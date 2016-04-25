using System;

namespace Graphene.Example.Data.EntityFramework
{
    public static class Extensions
    {
        public static void Times(this int source, Action action)
        {
            for (int i = 0; i < source; i++)
            {
                action();
            }

        }

    }
}