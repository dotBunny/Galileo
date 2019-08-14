using System;
using System.Collections.Generic;
using System.Linq;

namespace Galileo.Core
{
    /// <summary>
    /// Galileo Custom Types
    /// </summary>
    public static class Types
    {
        public class Vector2<T>
        {
            public T X;
            public T Y;

            public Vector2()
            {
                
            }
            public Vector2(T x, T y)
            {
                X = x;
                Y = y;
            }

            public void FromString(string input)
            {
                var split = input.Split(',');

                if (split.Length > 0)
                {
                    X = (T)Convert.ChangeType(split[0], typeof(T));
                }
           
                if (split.Length > 1)
                {
                    Y = (T)Convert.ChangeType(split[1], typeof(T));
                }
            }
            public override string ToString()
            {
                return X + "," + Y;
            }
        }


        public static IEnumerable<T> GetUniqueFlags<T>(this Enum flags)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("The generic type parameter must be an Enum.");

            if (flags.GetType() != typeof(T))
                throw new ArgumentException("The generic type parameter does not match the target type.");

            ulong flag = 1;
            foreach (var value in Enum.GetValues(flags.GetType()).Cast<T>())
            {
                ulong bits = Convert.ToUInt64(value);
                while (flag < bits)
                {
                    flag <<= 1;
                }

                if (flag == bits && flags.HasFlag(value as Enum))
                {
                    yield return value;
                }
            }
        }

        public static string GetString(this string[] strings)
        {
            string builder = "";
            foreach (string s in strings)
            {
                builder += s + ", ";
            }
            if (builder.EndsWith(", ", StringComparison.Ordinal))
            {
                builder = builder.Substring(0, builder.Length - 2);
            }
            return builder;
        }
    }
}
