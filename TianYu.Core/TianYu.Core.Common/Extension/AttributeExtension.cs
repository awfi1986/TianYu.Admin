using System; 
using System.ComponentModel; 
using System.Reflection; 

namespace TianYu.Core.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class AttributeExtension
    {
        /// <summary>
        /// 获取对象的DescriptionAttribute特性的Description说明
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            Type t = obj.GetType();
            string fieldName = Enum.GetName(t, obj);
            if (fieldName.IsNullOrWhiteSpace())
            {
                return string.Empty;
            }
            FieldInfo field = t.GetField(fieldName);
            if (field == null)
            {
                return string.Empty;
            }
            DescriptionAttribute descAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            if (descAttr == null)
            {
                return string.Empty;
            }
            else
            {
                return descAttr.Description;
            }
        }
        /// <summary>
        /// 获取对象的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="obj">要查找的对象</param>
        /// <returns>返回 特性类型 实例</returns>
        public static T GetAttribute<T>(this object obj) where T : class
        {
            Type type = obj.GetType();
            var attrs = type.GetCustomAttributes(typeof(T), false);
            if (attrs != null && attrs.Length > 0)
            {

                return (T)attrs[0];
            }
            return null;
        }


        /// <summary>
        /// 获取对象的特性
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="type">要查找的对象</param>
        /// <returns>返回 特性类型 实例</returns>
        public static T GetAttribute<T>(this System.Type type) where T : class
        {
            var attrs = type.GetCustomAttributes(typeof(T), false);
            if (attrs != null && attrs.Length > 0)
            {

                return (T)attrs[0];
            }
            return null;
        }
    }
}
