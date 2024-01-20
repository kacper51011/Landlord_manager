using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Application.Helpers
{
    public static class ValueValidator
    {
        /// <summary>
        /// Checks if a value of T is included in one of the values of T type fields in class of type U, return true if not included in fields
        /// </summary>
        /// <typeparam name="T">Input value type</typeparam>
        /// <typeparam name="U">Class with const values</typeparam>
        /// <param name="value">input value</param>
        /// <returns></returns>
        public static bool IsNotProperValue<T, U>(T value)
        {

            Type type = typeof(U);

            var flags = BindingFlags.Static | BindingFlags.Public;
            var fields = type.GetFields(flags).OfType<T>().ToList();

            if (value is not null && fields.Count > 0)
            {

                return !fields.Any(x => x.Equals(value));
            }

            return true;
        }
    }
}
