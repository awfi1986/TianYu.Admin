
layui.define(['upload', 'jquery'], function (exports) {
    var $ = layui.jquery
        , upload = layui.upload;
    /**
    * TianYu文件上传
    * @@param {any} upload LayUI上传对象
    * @@param {any} options 初始化参数
    */
    var TianYuFileUpload = function (options) {
        /**
        * 文件上传设置属性
        */
        var setting_options = {
            url: '/FileUpload/ReportUploadFile',//layui控件上传地址
            size: 5120,//上传文件大小
            accept: [], //上传控件接受文件类型集合（如：image/jpg）
            multiple: false,//是否多文件上传
            number: 1,//最多可上传几个文件
        };

        //合并当前传递的参数
        setting_options = $.extend({}, setting_options, options);

        //配置LayUI上传
        setting_options.multiple = true;//此处请保持true，这样可触发allDone事件
        //全部完成事件
        setting_options.allDone = function (obj) {
            //success_call_back(getResult());
            success_call_back.call(this, getResult());
        }
        //完成事件
        setting_options.done = function (res) {
            if (res.Status == 200) {
                res = res.Data;
                if (res.Status == 1) {
                    //当前已上传集合，layui多文件上传则是单个返回，此处特殊处理
                    if (res.BusinessData && res.BusinessData.UploadResult) {
                        hasUploadList.push(res.BusinessData.UploadResult[0]);
                    }
                }
            }
        };
        //错误事件
        setting_options.error = function (index, upload) {
            error_call_back("上传失败", { index, upload });
        };
        //上传之前事件
        setting_options.before = function (obj) {
            before_call_back(obj);
        };

        /**
         * 当前已上传集合（因：layui多文件上传则是单个返回）
         */
        var hasUploadList = [];

        //初始化
        var UploadObj = upload.render(setting_options);

        /**
        * 获取结果
        */
        var getResult = function () {
            var res = { Status: 1, BusinessData: [], ErrorMessage: '上传文件成功' };
            if (hasUploadList.length == 0) {
                res.Status = 0;
                res.ErrorMessage = '上传文件失败';
            }
            else {
                res.BusinessData = hasUploadList;
            }
            return res;
        }

        /**
		* 上传之前回调事件
		* @@param obj
		*/
        var before_call_back = function (obj) {
            hasUploadList = [];

            if ("function" == typeof (options.before)) {
                options.before(obj);
            }
        }
        /**
		* 错误回调事件
		* @@param msg
		* @@param res
		*/
        var error_call_back = function (msg, res) {
            if ("function" == typeof (options.error)) {
                options.error(msg, res);
            }
            else {
                layer.msg(msg);
            }
        }

        /**
        * 成功回调事件
        * @@param res
        */
        var success_call_back = function (res) {
            //处理图片库
            if (setting_options.isAlbums) {
                if (!setting_options.shopId || !setting_options.storeId) {
                    console.error('图片库保存必须设置商家Id、店铺Id，请确认设置');
                }
                else {
                    //处理上传
                    saveAlbums(setting_options.shopId, setting_options.storeId, setting_options.remark, res.BusinessData);
                }
            }

            if ("function" == typeof (options.done)) {
                options.done.call(this, res);
                //options.done(res);
            }
        }

        /**
         * 保存相册图片集
         * @param {any} shopId 商家Id
         * @param {any} storeId 店铺Id
         * @param {any} remark 图片描述
         * @param {any} data 图片集合
         */
        var saveAlbums = function (shopId, storeId, remark, data) {
            $.ajax({
                url: '/PictureLibrary/AddPictureLibraryPhotosByTianYuUpload',
                data: {
                    ShopId: shopId,
                    StoreId: storeId,
                    Items: data,
                    Remark: remark,
                },
                type: 'post',
                dataType: 'json',
                success: function (res) {
                    if (res.Status == 200) {
                        res = res.Data;
                        if (res.Status == 1) {

                        }
                        else {
                            layer.msg(res.ErrorMessage);
                        }
                    }
                    else {
                        layer.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                    }
                },
                error: function (res) {
                    console.log(res);
                }
            });
        };

        return UploadObj;
    }

    exports('TianYuFileUpload', TianYuFileUpload);

});