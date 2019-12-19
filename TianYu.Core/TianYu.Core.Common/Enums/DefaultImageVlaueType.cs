using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 默认图片枚举类型
    /// </summary>
    public enum DefaultImageVlaueType
    {
        /// <summary>
        /// 不使用默认图片
        /// </summary>
        [Description("")]
        None = -1,
        /// <summary>
        /// 使用默认图片（地址：Default/Icon/default_img.png）
        /// </summary>
        [Description("Default/Icon/default_img.png")]
        DefaultImage = 0,
        /// <summary>
        /// 游客头像默认图片（地址：Default/Icon/default_head.png）
        /// </summary>
        [Description("Default/Icon/default_head.png")]
        VisitorHeadImg = 1,
        /// <summary>
        /// 商品默认图片（地址：Default/Icon/default_img.png）
        /// </summary>
        [Description("Default/Icon/default_img.png")]
        GoodsImage = 2,
        /// <summary>
        /// 门店默认图片（地址：Default/Icon/default_shop.png）
        /// </summary>
        [Description("Default/Icon/default_shop.png")]
        StoreImage = 3,
    }
}
