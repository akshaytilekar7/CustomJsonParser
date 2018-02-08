using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomJsonParser
{
    //public partial class JsonParser
    //{
    //    public MyType ConvertToJsonObject<MyType>(string JsonString)
    //    {
    //        var obj = Activator.CreateInstance<MyType>();
    //        StringBuilder temp = new StringBuilder();

    //        JsonString= JsonString.Replace(": [", ":[");
    //        JsonString = JsonString.Replace(": {", ":{");

    //        var index =  JsonString.IndexOf(':');
    //        char c =JsonString[index + 1];
    //        if (c == 'a')
    //        {

    //        }

    //        return obj;
    //    }
    //}

    public partial class JsonParser
    {
        public string ConvertToJsonString(Object obj)
        {
            StringBuilder str = new StringBuilder();
            Type type = obj.GetType();

            //all Inbuilt type from obj class
            IEnumerable<PropertyInfo> normalPropertise = type.GetProperties().Where(x => x.PropertyType.Assembly.FullName != type.Assembly.FullName && (typeof(IEnumerable).IsAssignableFrom(x.PropertyType) == typeof(String).Equals(x.PropertyType)));
            //all Custom type from obj class like [public class Manager Manager {get;set;}]
            IEnumerable<PropertyInfo> customPropertise = type.GetProperties().Where(x => x.PropertyType.Assembly.FullName == type.Assembly.FullName && x.PropertyType.IsClass);
            //all Custom and Inbuilt type from obj class like 
            //[public class List<Manager> Lstmanager {get;set;}]
            //[public class List<string> LstNames {get;set;}]
            IEnumerable<PropertyInfo> IenumerablePropertise = type.GetProperties().Where(x => !typeof(String).Equals(x.PropertyType) && typeof(IEnumerable).IsAssignableFrom(x.PropertyType));

            //return key value pair and append to string
            str.Append(DoWorkForNormalPropertise(normalPropertise, obj));

            if (IenumerablePropertise != null)
            {
                foreach (var propertyInfo in IenumerablePropertise)
                {
                    //Check List property contains data or not
                    var iePropertyHasData = obj.GetType().GetProperty(propertyInfo.Name).GetValue(obj, null);


                    if (iePropertyHasData == null)
                    {
                        str.Append(",\"" + propertyInfo.Name + "\":null");
                    }
                    else
                    {
                        str.Append(",\"" + propertyInfo.Name + "\":[");

                        var ie = iePropertyHasData as IEnumerable;

                        //actualObject can be string or class ie, Inbuilt or Custom
                        foreach (var actualObject in ie)
                        {
                            //Evaluate type of actualObject
                            var isCustom = actualObject.GetType().Assembly.FullName == type.Assembly.FullName;

                            if (isCustom)
                            {
                                //if Custom 
                                //Then it may contains List of Custom type or Ibuilt type
                                // AGAIN WE HAVE TO ITERATE SAME LOGIC
                                str.Append(ConvertToJsonString(actualObject));
                                str.Append(",");
                            }
                            else
                            {
                                // if it is Ibuilt type so directly bind [string, bool]
                                str.Append(DataTypeHepler.EvaluateDataType(actualObject));
                                str.Append(",");
                            }
                        }
                        str = str.Length == 0 ? str : str.Remove(str.Length - 1, 1);
                        str.Append("],");
                    }
                }
            }
            if (customPropertise == null)
            {
                return Convert.ToString(str);
            }
            else
            {
                // Custom type Like Address, Manager
                // we have to bind Like
                //NameOfType : value,value,value,..
                str.Append(DoWorkForCustomPropertise(customPropertise, obj));
            }
            var res = "{" + Convert.ToString(str) + "}";
            res = res.Replace(",,", ",");
            res = res.Replace("],}", "]}");
            res = res.Replace("},]", "}]");
            return res;
        }
        /// <summary>
        /// ganarate string for Custom Type
        /// Ex : Manager : v1,v2,
        /// </summary>
        /// <param name="customPropertise"></param>
        /// <param name="obj"></param>
        /// <returns>NameOfType : value,value,value,..</returns>
        private String DoWorkForCustomPropertise(IEnumerable<PropertyInfo> customPropertise, Object obj)
        {
            StringBuilder str1 = new StringBuilder();

            foreach (var item in customPropertise)
            {
                //custom type can AGAIN contain InBuilt and Custom type
                //ITS LIKE WHOLE COMPLTE NEW TYPE
                //AGAIN SAME STEPS
                var CTypes = obj.GetType().GetProperty(item.Name).GetValue(obj, null);
                if (CTypes == null)
                {
                    str1.Append(",\"" + item.Name + "\" : null");
                }
                else
                {
                    str1.Append(",\"" + item.Name + "\":");
                    if (CTypes != null)
                        str1.Append(ConvertToJsonString(CTypes));
                    //str1.Append(" ");
                }
            }
            return str1.ToString();
        }

        /// <summary>
        /// iterate thr all list 
        /// and generate key value pair
        /// </summary>
        /// <param name="Propertise"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        private String DoWorkForNormalPropertise(IEnumerable<PropertyInfo> Propertise, Object obj)
        {
            StringBuilder str = new StringBuilder();
            foreach (PropertyInfo propertyInfo in Propertise)
            {
                Type type = obj.GetType();
                str.Append(GenerateKeyValuePair(obj, propertyInfo));
            }
            str = str.Length == 0 ? str : str.Remove(str.Length - 1, 1);
            return str.ToString();
        }

        /// <summary>
        /// Generate key value pair of property Like  EmpName = "Akshay"
        /// "EmpName" : "Akshay"  // User EscapeHelper and Evaluate DataType for ",',
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        private StringBuilder GenerateKeyValuePair(Object obj, PropertyInfo propertyInfo)
        {
            StringBuilder str2 = new StringBuilder();
            var name = DataTypeHepler.EscapeHelper(propertyInfo.Name);
            var value = DataTypeHepler.EvaluateDataType(obj, propertyInfo);
            str2.Append(name + ":" + value);
            str2.Append(",");
            return str2;
        }

    }
}

