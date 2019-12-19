using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace TianYu.Core.Common
{
    public static class AutoMapperExt
    {
        /// <summary>
        ///  类型映射
        /// </summary>
        public static TDestination MapTo<TDestination>(this object source)
        {
            if (source == null) return default(TDestination);

            var mapper = new MapperConfiguration(a => a.CreateMap(source.GetType(), typeof(TDestination))).CreateMapper();
            return mapper.Map<TDestination>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TDestination>(this IEnumerable source)
        {
            IMapper mapper = null;

            foreach (var first in source)
            {
                mapper = new MapperConfiguration(a => a.CreateMap(first.GetType(), typeof(TDestination))).CreateMapper();
                break;
            }
            if (mapper == null)
            {
                return new List<TDestination>();
            }

            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 集合列表类型映射
        /// </summary>
        public static List<TDestination> MapToList<TSource, TDestination>(this IEnumerable<TSource> source)
        {
            var mapper = new MapperConfiguration(a => a.CreateMap(typeof(TSource), typeof(TDestination))).CreateMapper();
            return mapper.Map<List<TDestination>>(source);
        }

        /// <summary>
        /// 类型映射
        /// </summary>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
            where TSource : class
            where TDestination : class
        {
            if (source == null) return destination;

            var mapper = new MapperConfiguration(a => a.CreateMap(typeof(TSource), typeof(TDestination))).CreateMapper();
            return mapper.Map(source, destination);
        }

        /// <summary>
        /// DataReader映射
        /// </summary>
        public static IEnumerable<T> DataReaderMapTo<T>(this IDataReader reader)
        {
            var mapper = new MapperConfiguration(a => a.CreateMap(typeof(IDataReader), typeof(IEnumerable<T>))).CreateMapper();
            return mapper.Map<IDataReader, IEnumerable<T>>(reader);
        }
        /// <summary>
        ///  类型映射字段
        /// </summary>
        public static Dictionary<string, object> MapToDic(this object source)
        {
            if (source == null) return default(Dictionary<string, object>);
            var mapper = new MapperConfiguration(a => a.CreateMap(source.GetType(), typeof(Dictionary<string, object>)).ConstructUsing(b =>
                JsonHelper.DeserializeObject<Dictionary<string, object>>(JsonHelper.SerializeObject(source))
            )).CreateMapper();
            return mapper.Map<Dictionary<string, object>>(source);
        }
    }
}
