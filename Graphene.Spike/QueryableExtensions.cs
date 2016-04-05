using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Graphene.Core.Model;

namespace Graphene.Spike
{
    public static class QueryableExtensions
    {
        public static IQueryable<dynamic> SelectPartially<T>(this IQueryable<T> source, IEnumerable<string> propertyNames)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source Object is NULL");
            }

            var sourceItem = Expression.Parameter(source.ElementType, "t");

            var sourceProperties = propertyNames.Where(name => source.ElementType.GetProperty(name) != null).ToDictionary(name => name, name => source.ElementType.GetProperty(name));
            var dynamicType = DynamicTypeBuilder.GetDynamicType(sourceProperties.Values.ToDictionary(f => f.Name, f => f.PropertyType), typeof(object), Type.EmptyTypes);

            var bindings = dynamicType.GetFields().Select(p => Expression.Bind(p, Expression.Property(sourceItem, sourceProperties[p.Name]))).OfType<MemberBinding>().ToList();
            var selector = Expression.Lambda<Func<T, dynamic>>(Expression.MemberInit(Expression.New(dynamicType.GetConstructor(Type.EmptyTypes)), bindings), sourceItem);

            return source.Select(selector);
        }

        public static IEnumerable<T> SelectIncluding<T>(this IQueryable<T> source, IEnumerable<Expression<Func<T, object>>> includeExpessions)
        {
            if (source == null) throw new ArgumentNullException("Source Object is NULL");

            var sourceItem = Expression.Parameter(source.ElementType, "t");
            var paramRewriter = new PredicateRewriterVisitor(sourceItem);

            Dictionary<string, Tuple<Expression, Type>> dynamicFields = new Dictionary<string, Tuple<Expression, Type>>();
            int dynamicFieldsCounter = 0;
            foreach (Expression<Func<T, object>> includeExpession in includeExpessions)
            {
                Type typeDetected;
                if ((includeExpession.Body.NodeType == ExpressionType.Convert) ||
                    (includeExpession.Body.NodeType == ExpressionType.ConvertChecked))
                {
                    var unary = includeExpession.Body as UnaryExpression;
                    if (unary != null)
                        typeDetected = unary.Operand.Type;
                }
                typeDetected = includeExpession.Body.Type;
                //Save into List
                dynamicFields.Add("f" + dynamicFieldsCounter, new Tuple<Expression, Type>(
                    paramRewriter.ReplaceParameter(includeExpession.Body, includeExpession.Parameters[0]),
                    typeDetected
                    ));
                //Count
                dynamicFieldsCounter++;
            }

            dynamicFields.Add("sourceObject", new Tuple<Expression, Type>(
                sourceItem,
                source.ElementType
                ));

            var dynamicType = DynamicTypeBuilder.GetDynamicType(dynamicFields.ToDictionary(x => x.Key, x => x.Value.Item2), typeof(object), Type.EmptyTypes);
            var bindings = dynamicType.GetFields().Select(p => Expression.Bind(p, dynamicFields[p.Name].Item1)).OfType<MemberBinding>().ToList();
            var selector = Expression.Lambda<Func<T, dynamic>>(Expression.MemberInit(Expression.New(dynamicType.GetConstructor(Type.EmptyTypes)), bindings), sourceItem);

            return source.Select(selector).AsEnumerable().Select(x => (T)x.sourceObject);
        }

        private class PredicateRewriterVisitor : ExpressionVisitor
        {
            private ParameterExpression _parameterExpression;
            private ParameterExpression _parameterExpressionToReplace;
            public PredicateRewriterVisitor(ParameterExpression parameterExpression)
            {
                _parameterExpression = parameterExpression;
            }
            public Expression ReplaceParameter(Expression node, ParameterExpression parameterExpressionToReplace)
            {
                _parameterExpressionToReplace = parameterExpressionToReplace;
                return base.Visit(node);
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                if (node == _parameterExpressionToReplace) return _parameterExpression;
                else return node;
            }
        }

        public static class DynamicTypeBuilder
        {
            private static AssemblyName assemblyName = new AssemblyName() { Name = "DynamicLinqTypes" };
            private static ModuleBuilder moduleBuilder = null;
            private static Dictionary<string, Tuple<string, Type>> builtTypes = new Dictionary<string, Tuple<string, Type>>();

            static DynamicTypeBuilder()
            {
                moduleBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).DefineDynamicModule(assemblyName.Name);
            }

            private static string GetTypeKey(Dictionary<string, Type> fields)
            {
                string key = string.Empty;
                foreach (var field in fields.OrderBy(v => v.Key).ThenBy(v => v.Value.Name))
                    key += field.Key + ";" + field.Value.Name + ";";
                return key;
            }

            public static Type GetDynamicType(Dictionary<string, Type> fields, Type basetype, Type[] interfaces)
            {
                if (null == fields)
                    throw new ArgumentNullException("fields");
                if (0 == fields.Count)
                    throw new ArgumentOutOfRangeException("fields", "fields must have at least 1 field definition");

                try
                {
                    Monitor.Enter(builtTypes);
                    string typeKey = GetTypeKey(fields);

                    if (builtTypes.ContainsKey(typeKey))
                        return builtTypes[typeKey].Item2;

                    string typeName = "DynamicLinqType" + builtTypes.Count.ToString();
                    TypeBuilder typeBuilder = moduleBuilder.DefineType(typeName, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable, null, Type.EmptyTypes);

                    foreach (var field in fields)
                        typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);

                    builtTypes[typeKey] = new Tuple<string, Type>(typeName, typeBuilder.CreateType());

                    return builtTypes[typeKey].Item2;
                }
                catch
                {
                    throw;
                }
                finally
                {
                    Monitor.Exit(builtTypes);
                }

            }

        }

        public static IQueryable<T> Filter<T>(this IQueryable<T> source, Argument[] arguments)
        {
            if (arguments == null)
            {
                return source;
            }

            return arguments.Aggregate(source, (current, argument) =>
                current.Where(SimpleComparison<T>(argument.Name, argument.Value)));
        }

        private static Expression<Func<TSource, bool>> SimpleComparison<TSource>
            (string property, object value)
        {
            var type = typeof(TSource);
            var pe = Expression.Parameter(type, "p");
            var propertyReference = Expression.Property(pe, property);
            var constantReference = Expression.Constant(value);
            return Expression.Lambda<Func<TSource, bool>>
                (Expression.Equal(propertyReference, constantReference), pe);
        }
    }
}
