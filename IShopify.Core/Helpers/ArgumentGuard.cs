using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IShopify.Core.Helpers
{
    public static class ArgumentGuard
    {
        public static void NotNull<T>(T argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException($"'{argumentName}' cannot be null");
            }
        }

        public static void NotDefault<T>(T value, string argumentName)
            where T : struct
        {
            if (value.Equals(default(T)))
            {
                throw new ArgumentNullException($"'{argumentName}' needs to be set.");
            }
        }

        public static void NotNullOrEmpty<T>(IEnumerable<T> argument, string argumentName)
        {
            if (argument == null || !argument.Any())
            {
                throw new ArgumentException($"'{argumentName}' cannot be null or empty");
            }
        }

        public static void NotNullOrWhiteSpace(string argument, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException($"'{argumentName}' cannot be null or empty");
            }
        }

        public static void NotEmpty(Guid argument, string argumentName)
        {
            if (argument == Guid.Empty)
            {
                throw new ArgumentException($"'{argumentName}' cannot be an empty Guid");
            }
        }


        /// <summary>
        /// Checks that the collection does not contain null or empty elements, provided that the collection is not null or empty
        /// </summary>
        /// <param name="source"></param>
        /// <param name="name"></param>
        public static void NotContainNullOrEmptyElements(this IEnumerable<string> source, string name)
        {
            if (source.IsNotNull() && source.Any(x => x.IsNullOrEmpty()))
            {
                throw new ArgumentOutOfRangeException(name, $"'{name}' cannot contain elements that are null or empty");
            }
        }

        /// <summary>
        /// Checks that the collection does not contain null elements, provided that the collection is not null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        public static void NotContainNullElements<T>(this IEnumerable<T> source, string name)
            where T : class
        {
            if (source.IsNotNull() && source.Any(x => x == null))
            {
                throw new ArgumentOutOfRangeException(name, $"'{name}' cannot contain elements that are null");
            }
        }

        /// <summary>
        /// Checks that the collection does not contain default elements, provided that the collection is not null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="name"></param>
        public static void NotContainDefaultElements<T>(this IEnumerable<T> source, string name)
            where T : struct
        {
            if (source.IsNotNull() && source.Any(x => x.Equals(default(T))))
            {
                throw new ArgumentOutOfRangeException(name, $"'{name}' cannot contain elements that have default values");
            }
        }
    }
}
