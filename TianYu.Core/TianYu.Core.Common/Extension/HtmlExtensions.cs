using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace TianYu.Core.Common
{
    /// <summary>
    /// MVC-HTML扩展类
    /// </summary>
    public static class HtmlExtensions
    {
        #region RadioButtonList
        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="selectList">集合</param>
        /// <param name="expression">模型字段</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, string defValues = "")
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return RadioButtonList(htmlHelper, name, selectList, htmlAttributes, defValues);
        }

        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="expression">模型字段</param>
        /// <param name="selectList">集合</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string defValues = "")
        {
            string name = ExpressionHelper.GetExpressionText(expression);
            return RadioButtonList(htmlHelper, name, selectList, new { }, defValues);
        }

        /// <summary>
        /// Radio列表
        /// </summary>
        /// <param name="htmlHelper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <returns></returns>
        public static MvcHtmlString RadioButtonList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, string defValues = "")
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            string defaultValue = string.Empty;
            if (htmlHelper.ViewData.Model != null)
            {
                if (!string.IsNullOrWhiteSpace(name))
                {
                    defaultValue = htmlHelper.ViewData.Eval(name) + "";
                }
            }
            
            if (!string.IsNullOrWhiteSpace(defValues))
            {
                defaultValue = defValues;
            }
            StringBuilder str = new StringBuilder();
            foreach (SelectListItem item in selectList)
            {
                //str.AppendLine("<label>");
                str.Append("<input ");
                if (item.Value == defaultValue)
                {
                    str.Append("checked='checked' ");
                }
                foreach (var bute in HtmlAttributes)
                {
                    str.Append(bute.Key + "=\"" + bute.Value);

                }

                str.AppendFormat("\" type=\"radio\"  value=\"{0}\" name=\"{1}\" title=\"{2}\" />", item.Value, name, item.Text);
                str.AppendLine("");
                //str.AppendLine("</label>");
            }


            return MvcHtmlString.Create(str.ToString());
        }
        #endregion

        #region CheckBoxList
        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="helper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(helper, name, selectList, new { });
        }
        /// <summary>
        /// CheckBox列表
        /// </summary>
        /// <param name="helper">辅助类</param>
        /// <param name="name">字段名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">html标签</param>
        /// <returns></returns>
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            HashSet<string> set = new HashSet<string>();
            List<SelectListItem> list = new List<SelectListItem>();
            string selectedValues = Convert.ToString((selectList as SelectList).SelectedValue);

            if (!string.IsNullOrEmpty(selectedValues))
            {
                if (selectedValues.Contains(","))
                {

                    string[] tempStr = selectedValues.Split(',');

                    for (int i = 0; i < tempStr.Length; i++)
                    {
                        set.Add(tempStr[i]);
                    }
                }
                else
                {
                    set.Add(selectedValues);
                }
            }
            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? set.Contains(item.Value) : set.Contains(item.Text);
                list.Add(item);
            }
            selectList = list;
            HtmlAttributes.Add("type", "checkbox");
            HtmlAttributes.Add("id", name);
            HtmlAttributes.Add("name", name);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (SelectListItem selectItem in selectList)
            {

                IDictionary<string, object> newHtmlAttributes = HtmlAttributes.DeepCopy();
                newHtmlAttributes.Add("value", selectItem.Value);
                if (selectItem.Selected)
                {
                    newHtmlAttributes.Add("checked", "checked");
                }
                TagBuilder tagBuilder = new TagBuilder("input");
                tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);
                string inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                stringBuilder.AppendFormat("<label> {0}  {1}</label>", inputAllHtml, selectItem.Text);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        private static IDictionary<string, object> DeepCopy(this IDictionary<string, object> ht)
        {
            Dictionary<string, object> _ht = new Dictionary<string, object>();
            foreach (var p in ht)
            {
                _ht.Add(p.Key, p.Value);
            }
            return _ht;
        }
        #endregion

    }
}