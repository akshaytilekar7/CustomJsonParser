using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomJsonParser
{
    public static class DataTypeHepler
    {

        /// <summary>
        /// retrive value of property
        /// </summary>
        /// <param name="obj">Type</param>
        /// <param name="propertyInfo">Property Info</param>
        /// <returns></returns>
        private static object GetValue(Object obj, PropertyInfo propertyInfo)
        {
            return obj.GetType().GetProperty(propertyInfo.Name).GetValue(obj, null);
        }

        /// <summary>
        /// Generate Escape string 
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static string EscapeHelper(string value)
        {
            return "\"" + value + "\"";
        }

        /// <summary>
        /// retutn true of property is Custom or Inbuilt type
        /// </summary>
        /// <param name="propertyInfo">property of a class</param>
        /// <returns></returns>
        public static bool IsCustomDataType(PropertyInfo propertyInfo)
        {
            if (propertyInfo.GetType().Assembly.GetName().Name != "mscorlib")
                return false;
            return true;
        }

        /// <summary>
        /// Evaluate datatype of property
        /// </summary>
        /// <param name="obj">TYOE</param>
        /// <param name="propertyInfo">PropertyInfo</param>
        /// <returns></returns>
        public static Object EvaluateDataType(Object obj, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(string))
            {
                if (GetValue(obj, propertyInfo) == null)
                    return "null";
                return "\"" + GetValue(obj, propertyInfo) + "\"";
            }
            else if (propertyInfo.PropertyType == typeof(bool))
            {
                return GetValue(obj, propertyInfo).ToString().ToLower();
            }
            else if (propertyInfo.PropertyType == typeof(int))
            {
                return GetValue(obj, propertyInfo);
            }
            else if (propertyInfo.PropertyType == typeof(decimal))
            {
                decimal d = Convert.ToDecimal(GetValue(obj, propertyInfo));
                return "\"" + d.ToString("N8") + "\"";
            }
            else if (propertyInfo.PropertyType == typeof(DateTime))
            {
                var d = Convert.ToDateTime(GetValue(obj, propertyInfo));
                return "\"" + d.ToString("yyyy-MM-dd h:mm tt") + "\"";
            }
            else
            {
                throw new Exception("Developer Error in EvaluateDataType");
            }
        }

        /// <summary>
        /// Evaluate DataType for .NET type (like string, int, bool etc)
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns></returns>
        public static Object EvaluateDataType(Object obj)
        {
            if (obj.GetType() == typeof(string))
            {
                if (obj == null)
                    return "null";
                return "\"" + obj + "\"";
            }
            else if (obj.GetType() == typeof(bool))
            {
                return obj;
            }
            else if (obj.GetType() == typeof(int))
            {
                return obj;
            }
            else if (obj.GetType() == typeof(decimal))
            {
                decimal d = Convert.ToDecimal(obj);
                return "\"" + d.ToString("N8") + "\"";
            }
            else if (obj.GetType() == typeof(DateTime))
            {
                var d = Convert.ToDateTime(obj);
                return "\"" + d.ToString("yyyy-MM-dd h:mm tt") + "\"";
            }
            else
            {
                throw new Exception("Developer Error in EvaluateDataType");
            }
        }

    }
}
