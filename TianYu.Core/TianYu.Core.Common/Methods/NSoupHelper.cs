using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// NSoup操作帮助类
    /// <para>已集成：</para>
    /// <para>1、根据HTML内容，获取所有imgUrl</para>
    /// <para>2、根据HTML内容，获取所有img标签</para>
    /// </summary>
    public class NSoupHelper
    {

        /// <summary>
        /// 根据HTML内容，获取所有img标签Url
        /// </summary>
        /// <param name="content">HTML内容</param>
        /// <param name="isUri">是否补充URL</param>
        /// <returns></returns>
        public static List<string> GetImageUrlList(string content, bool isUri = true)
        {
            List<string> imgList = null;
            //解析
            try
            {
                NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(content);
                imgList = doc.GetElementsByTag("img").Select(a => a.Attributes["src"]).ToList();
                if (isUri)
                {
                    imgList = imgList.Select(a => a.ToImageUrl()).ToList();
                }
            }
            catch
            {
                imgList = new List<string>();
            }
            return imgList;
        }
        /// <summary>
        /// 根据HTML内容，获取所有img标签
        /// </summary>
        /// <param name="content">HTML内容</param>
        /// <returns></returns>
        public static List<string> GetImageTagList(string content)
        {
            List<string> imgList = null;
            //解析
            try
            {
                NSoup.Nodes.Document doc = NSoup.NSoupClient.Parse(content);
                imgList = doc.GetElementsByTag("img").Select(a => a.ToString()).ToList();
            }
            catch
            {
                imgList = new List<string>();
            }
            return imgList;
        }

    }
}
