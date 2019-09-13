//verifyType：1=只校验到第1级，2=只校验到第2级，3=只校验到第3级（默认）
var defaults = {
    s1: 'Province',
    s2: 'City',
    s3: 'Area',
    v1: '',
    v2: '',
    v3: '',
    verifyType: 3,
    sc1: null,
    sc2: null,
    sc3: null
};
layui.define(['jquery', 'form'], function (exports) {
    var $ = layui.jquery
        , form = layui.form
        , $form = $('.layui-form');

    var render = function (config) {
        //此处处理默认值
        config = initTreeSelect(config);

        $form.find('select[name=' + config.s1 + ']').html("");
        $.each(threeSelectData, function (k, v) {
            appendOptionTo($form.find('select[name=' + config.s1 + ']'), v.Item.Text, v.Item.Value, config.v1);
        });
        form.render();
        cityEvent(config);
        if (config.verifyType >= 2) {
            $form.find('select[name=' + config.s2 + ']').removeAttr("disabled");
            $form.find('select[name=' + config.s2 + ']').parent().show();
        }
        else {
            //禁用
            $form.find('select[name=' + config.s2 + ']').attr("disabled", "disabled");
            $form.find('select[name=' + config.s2 + ']').parent().hide();
        }
        areaEvent(config);
        if (config.verifyType >= 3) {
            $form.find('select[name=' + config.s3 + ']').removeAttr("disabled");
            $form.find('select[name=' + config.s3 + ']').parent().show();
        }
        else {
            //禁用
            $form.find('select[name=' + config.s3 + ']').attr("disabled", "disabled");
            $form.find('select[name=' + config.s3 + ']').parent().hide();
        }
        form.on('select(' + config.s1 + ')', function (data) {
            cityEvent(data, true);
            if (config.sc1) {
                config.sc1(this);
            }
        });
        form.on('select(' + config.s2 + ')', function (data) {
            areaEvent(data, true);
            if (config.sc2) {
                config.sc2(this);
            }
        });
        form.on('select(' + config.s3 + ')', function (data) {
            if (config.sc3) {
                config.sc3(this);
            }
        });
        form.render();

        function cityEvent(data, isSelected) {
            $form.find('select[name=' + config.s2 + ']').html("");
            if (isSelected) {
                config.v1 = data.value;
            }
            else {
                config.v1 = data.value ? data.value : config.v1;
            }
            $.each(threeSelectData, function (k, v) {
                if (v.Item.Value == config.v1) {
                    if (v.Children) {
                        $.each(v.Children, function (kt, vt) {
                            appendOptionTo($form.find('select[name=' + config.s2 + ']'), vt.Item.Text, vt.Item.Value, config.v2);
                        });
                    }
                }
            });
            form.render();
            config.v2 = $('select[name=' + config.s2 + ']').val();

            areaEvent(config);
            if (config.verifyType >= 3) {
                $form.find('select[name=' + config.s3 + ']').removeAttr("disabled");
                $form.find('select[name=' + config.s3 + ']').parent().show();
            }
            else {
                //禁用
                $form.find('select[name=' + config.s3 + ']').attr("disabled", "disabled");
                $form.find('select[name=' + config.s3 + ']').parent().hide();
            }
        }
        function areaEvent(data, isSelected) {
            $form.find('select[name=' + config.s3 + ']').html("");
            if (isSelected) {
                config.v2 = data.value;
            }
            else {
                config.v2 = data.value ? data.value : config.v2;
            }
            $.each(threeSelectData, function (k, v) {
                if (v.Item.Value == config.v1) {
                    if (v.Children) {
                        $.each(v.Children, function (kt, vt) {
                            if (vt.Item.Value == config.v2) {
                                if (vt.Children && vt.Children.length) {
                                    //解除禁用
                                    $form.find('select[name=' + config.s3 + ']').removeAttr("disabled");

                                    $.each(vt.Children, function (ka, va) {
                                        appendOptionTo($form.find('select[name=' + config.s3 + ']'), va.Item.Text, va.Item.Value, config.v3);
                                    });
                                }
                                else {
                                    //处理禁用
                                    $form.find('select[name=' + config.s3 + ']').attr("disabled", "disabled");
                                }
                            }
                        });
                    }
                }
            });
            form.render();
            form.on('select(' + config.s3 + ')', function (data) { });
        }
        function appendOptionTo($o, k, v, d) {
            var $opt = $("<option>").text(k).val(v);
            if (v == d) { $opt.attr("selected", "selected") }
            $opt.appendTo($o);
        }

        return this;
    };
    var initTreeSelect = function (config) {
        config = $.extend(true, defaults, config);

        //此处处理默认值
        config.v1 = $('select[name=' + config.s1 + ']').attr("value");
        config.v2 = $('select[name=' + config.s2 + ']').attr("value");
        config.v3 = $('select[name=' + config.s3 + ']').attr("value");
        //设置layui标记
        $('select[name=' + config.s1 + ']').attr("lay-filter", config.s1);
        $('select[name=' + config.s2 + ']').attr("lay-filter", config.s2);
        $('select[name=' + config.s3 + ']').attr("lay-filter", config.s3);

        treeSelect.config = config;
        return config;
    };

    var setVerifyType = function (verifyType) {
        treeSelect.config.verifyType = verifyType;

        if (verifyType >= 2) {
            $form.find('select[name=' + treeSelect.config.s2 + ']').removeAttr("disabled");
            $form.find('select[name=' + treeSelect.config.s2 + ']').parent().show();
        }
        else {
            $form.find('select[name=' + treeSelect.config.s2 + ']').attr("disabled", "disabled");
            $form.find('select[name=' + treeSelect.config.s2 + ']').parent().hide();
        }
        if (verifyType >= 3) {
            $form.find('select[name=' + treeSelect.config.s3 + ']').removeAttr("disabled");
            $form.find('select[name=' + treeSelect.config.s3 + ']').parent().show();
        }
        else {
            $form.find('select[name=' + treeSelect.config.s3 + ']').attr("disabled", "disabled");
            $form.find('select[name=' + treeSelect.config.s3 + ']').parent().hide();
        }
        form.render();
    };

    var treeSelect = {
        render: render,
        verify: function () {
            var config = treeSelect.config;
            var verifyType = treeSelect.config.verifyType;
            if (verifyType >= 3) {
                if (!$('select[name=' + config.s3 + ']').attr("disabled") && !$('select[name=' + config.s3 + ']').val()) {
                    return false;
                }
            }
            if (verifyType >= 2) {
                if (!$('select[name=' + config.s2 + ']').val()) {
                    return false;
                }
            }
            if (verifyType >= 1) {
                if (!$('select[name=' + config.s1 + ']').val()) {
                    return false;
                }
            }

            return true;
        },
        config: {},
        initTreeSelect: initTreeSelect,
        setVerifyType: setVerifyType,
    };


    exports('treeSelect', treeSelect);
});