using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IShopify.Core.Helpers
{
    public static class ObjectExtension
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return (source == null || !source.Any());
        }

        public static bool IsNull<T>(this T source)
        {
            return source == null;
        }

        public static bool IsNotNull<T>(this T source)
        {
            return source != null;
        }

        public static bool IsDefault<T>(this T source)
            where T : struct
        {
            return source.Equals(default(T));
        }


        public static bool IsAnyOf<T>(this T source, params T[] values)
        {
            if (values.IsNullOrEmpty())
            {
                return false;
            }

            return values.Contains(source);
        }

        public static bool HasOneItem<T>(this IEnumerable<T> source)
        {
            return (source.Count() == 1);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty())
            {
                return;
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool IsCollection(this Type type)
        {
            if (type == null)
            {
                return false;
            }

            switch (type.Name)
            {
                case "IEnumerable`1":
                case "IList`1":
                case "List`1":
                case "ICollection`1":
                    return true;
                default:
                    return type.IsArray || type.Name.EndsWith("[]");
            }
        }

        public static IList<TResult> SelectIfNotNull<T, TResult>(this IEnumerable<T> source, Func<T, TResult> condition)
        {
            if (source == null)
            {
                return new List<TResult>();
            }

            return source.Select(condition).ToList();
        }

        public static bool ToBool(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return false;
            }

            return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }

        public static int ToSafeInt(this string value)
        {
            if (int.TryParse(value, out var intVale))
            {
                return intVale;
            }

            return default(int);
        }

        public static T FromJson<T>(this string source)
        {
            return JsonConvert.DeserializeObject<T>(source);
        }

        public static object FromJson(this string value, Type type)
        {
            return JsonConvert.DeserializeObject(value, type);
        }

        public static string ToJson<T>(this T data, bool camelCasing = false)
        {
            if (!camelCasing)
            {
                return JsonConvert.SerializeObject(data);
            }

            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            return JsonConvert.SerializeObject(data, settings);
        }

        public static V GetValue<K, V>(this IDictionary<K, V> source, K key, V defaultValue = default(V))
        {
            if (source.IsNullOrEmpty())
            {
                return defaultValue;
            }

            if (source.TryGetValue(key, out var value))
            {
                return value;
            }

            return defaultValue;
        }

        public static void Shuffle<T>(this IList<T> source)
        {
            var random = new Random();

            for (int i = 0; i < source.Count; i++)
            {
                var currentItem = source[i];
                var indexToSwap = random.Next(source.Count);

                source[i] = source[indexToSwap];
                source[indexToSwap] = currentItem;
            }
        }
    }
}
