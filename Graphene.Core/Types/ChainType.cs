using System.Collections.Generic;
using System.Linq;
using Graphene.Core.FieldTypes;
using Graphene.Core.Types.Scalar;

namespace Graphene.Core.Types
{
    public class ChainType : IGraphQLChainType
    {
        private readonly string[] _types;
        private readonly ITypeList _typeList;

        public ChainType(ITypeList typeList, params string[] types)
        {
            _typeList = typeList;
            _types = types;
        }

        public string Kind
        {
            get { return GetCurrentType().Kind; }
        }

        public string Name
        {
            get { return GetCurrentType().Name; }
        }

        public string Description
        {
            get { return GetCurrentType().Description; }
        }

        public IGraphQLType OfType
        {
            get
            {
                if (_types.Length >= 2)
                {
                    return new ChainType(_typeList, _types.Skip(1).ToArray());
                }
                return null;
            }
        }

        public IEnumerable<IGraphQLFieldType> Fields
        {
            get
            {
                var type = this;
                while (type != null &&
                    type.GetCurrentType() != null)
                {
                    var fields = type.GetCurrentType().GetFields().ToArray();

                    if (fields.Any())
                    {
                        return fields;
                    }

                    type = (ChainType)type.OfType;
                }
                return new IGraphQLFieldType[0];
            }
        }

        public IGraphQLType GetCurrentType()
        {
            return _typeList.LookUpType(_types.First());
        }

    }
}