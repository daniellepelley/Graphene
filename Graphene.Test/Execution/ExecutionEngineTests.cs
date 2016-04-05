using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Graphene.Core.Model;
using Graphene.Core.Parsers;
using Graphene.Execution;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Compatibility;

namespace Graphene.Test.Execution
{
    public class ExecutionEngineTests
    {
        [Test]
        public void RunsExecute()
        {
            var sut = new ExecutionEngine();

            var document = CreateDocument();

            var result = sut.Execute(document);
            Assert.AreEqual("json", result);
        }

        [Test]
        public void QueryUserNames()
        {
            var query = "{user{Name}}";
             
            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Name"":""Dan_Smith""},{""Name"":""Lee_Smith""},{""Name"":""Nick_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void QueryUserNamesAndIds()
        {
            var query = "{user {Id, Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":""1"",""Name"":""Dan_Smith""},{""Id"":""2"",""Name"":""Lee_Smith""},{""Id"":""3"",""Name"":""Nick_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void IsFast()
        {
            var query = "{ user(id: 1) {Id, Name, birthday}}";

            var document = new DocumentParser().Parse(query);
            ExecuteQuery(document);

            var documentStopWatch = new Stopwatch();
            documentStopWatch.Start();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                new DocumentParser().Parse(query);
            }
            documentStopWatch.Stop();

            var executeStopWatch = new Stopwatch();
            executeStopWatch.Start();
            foreach (var i in Enumerable.Range(0, 1000))
            {
                ExecuteQuery(document);
            }
            executeStopWatch.Stop();
              
            Assert.Less(executeStopWatch.ElapsedMilliseconds, 1000);
            Assert.Less(documentStopWatch.ElapsedMilliseconds, 50);
        }

        [Test]
        public void QueryUserNamesAndIdsFiltedById()
        {
            var query = "{user(Id: 1){Id,Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":""1"",""Name"":""Dan_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void QueryUserNamesAndIdsFiltedByNameAndId()
        {
            var query = @"{user(Id: 1, Name: Dan_Smith){Id,Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":""1"",""Name"":""Dan_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void QueryUserNamesAndIdsFiltedByNameAndIdReturnsNone()
        {
            var query = @"{user(Id: 2, Name: Dan_Smith){Id,Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[]";

            Assert.AreEqual(expected, json);
        }

        [Test]
        public void QueryUserNamesAndIdsFiltedByName()
        {
            var query = @"{user(Name: Dan_Smith){Id,Name}}";

            var document = new DocumentParser().Parse(query);
            var json = ExecuteQuery(document);
            var expected = @"[{""Id"":""1"",""Name"":""Dan_Smith""}]";

            Assert.AreEqual(expected, json);
        }

        private string ExecuteQuery(Document document)
        {
            return ExecuteQuery(GetData(), document);
        }

        private string ExecuteQuery<T>(IQueryable<T> source, Document document)
        {
            var operation = document.Operations.First();
            var fields = operation.Selections.Select(x => x.Field.Name);
            var arguments = operation.Directives.First().Arguments;
            var result = source
                .Filter(arguments)
                .SelectPartially(fields)
                .ToArray();

            var json = JsonConvert.SerializeObject(result);
            return json;
        }

        private static Document CreateDocument()
        {
            var document = new Document
            {
                Operations = new[]
                {
                    new Operation
                    {
                        Selections = new[]
                        {
                            new Selection
                            {
                                Field = new Field
                                {
                                    Name = "name"
                                }
                            }
                        }
                    }
                }
            };
            return document;
        }

        private IQueryable<TestUser> GetData()
        {
            return GetTestUsersFromDatabase().Select(x =>
                new TestUser
                {
                    Id = x.Id,
                    Name = x.Firstname + "_" + x.Lastname
                });
        }

        private IQueryable<TestUserDatabase> GetTestUsersFromDatabase()
        {
            return new[]
            {
                new TestUserDatabase
                {
                    Id = "1",
                    Firstname= "Dan",
                    Lastname = "Smith"
                },
                new TestUserDatabase
                {
                    Id = "2",
                    Firstname= "Lee",
                    Lastname = "Smith"
                },
                new TestUserDatabase
                {
                    Id = "3",
                    Firstname= "Nick",
                    Lastname = "Smith"
                }
            }.AsQueryable();

        }
    }

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