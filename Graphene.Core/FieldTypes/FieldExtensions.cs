using Graphene.Core.Model;

namespace Graphene.Core.FieldTypes
{
    public static class FieldExtensions
    {
        public static string GetFieldOrAliasName(this Field source)
        {
            return  string.IsNullOrEmpty(source.Alias)  
                ? source.Name
                : source.Alias;
        }

    }
}