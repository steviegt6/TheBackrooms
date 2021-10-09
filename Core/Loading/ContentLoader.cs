using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TheBackrooms.Core.Loading
{
    // TODO: Obsolete in 1.4.
    public class ContentLoader
    {
        public Assembly Assembly { get; }

        public ContentLoader(Assembly assembly)
        {
            Assembly = assembly;
        }

        public IEnumerable<Type> OfType<T>(bool ctorNoParams = true) => Assembly
            .GetTypes()
            .Where(x =>
                !x.IsAbstract
                && (
                    !ctorNoParams
                    || x.GetConstructor(Type.EmptyTypes) != null
                )
                && x == typeof(T)
            );

        public IEnumerable<T> OfInstances<T>(bool ctorNoParams = true) =>
            OfType<T>(ctorNoParams).Select(x => (T) Activator.CreateInstance(x));
    }
}