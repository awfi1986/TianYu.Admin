using TianYu.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TianYu.Admin.Infrastructure.Constant
{
    public class SystemMenuHelper
    {
        /// <summary>
        /// 获取系统菜单项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> GetSytemMenuList<T>() where T : SystemMenuItem, new()
        {
            SystemMenuAttribute systemMenuAttribute = new SystemMenuAttribute();
            IList<T> list = new List<T>();
            List<Assembly> listAssembly = new List<Assembly>();

            var path = ConfigHelper.GetAppsettingValue("dllPath");

            Assembly assembly1 = Assembly.LoadFrom(path);
            listAssembly.Add(assembly1);

            bool sysFlag = false;

            //模块编号
            int moduleNum = 0;
            //菜单编号
            int menuNum = 0;
            //功能编号
            int actionNum = 0;

            foreach (var assem in listAssembly)
            {
                var moduleList = assem.Modules;
                foreach (var module in moduleList)
                {
                    var typeList = module.Assembly.ExportedTypes.Where(x => x.Name.Contains("Controller")).Where(x => x.GetCustomAttribute(systemMenuAttribute.GetType()) != null).ToList();

                    moduleNum = 0;
                    menuNum = 0;

                    foreach (var type in typeList)
                    {
                        var attribute = (SystemMenuAttribute)type.GetCustomAttribute(systemMenuAttribute.GetType());
                        if (type != null)
                        {
                            if (!sysFlag)
                            {
                                //一级  系统名称
                                T firstMenuItem = new T
                                {
                                    ParentCode = "",
                                    PowerCode = attribute.SystemCode,
                                    PowerName = attribute.SystemName,
                                    BussionModule = attribute.SystemName,
                                    Status = 0,
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    Level = 1
                                };
                                list.Add(firstMenuItem);
                                sysFlag = true;
                            }


                            if (!list.Where(x => x.BussionModule == attribute.ModuleName && x.Level == 2).Any())
                            {
                                moduleNum++;
                                //二级  模块名称
                                T secondMenuItem = new T
                                {
                                    ParentCode = attribute.SystemCode,
                                    PowerCode = attribute.SystemCode + moduleNum.ToString().PadLeft(2, '0'),
                                    PowerName = attribute.ModuleName,
                                    BussionModule = attribute.ModuleName,
                                    Status = 0,
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    Level = 2
                                };

                                list.Add(secondMenuItem);
                            }


                            //三级  菜单名称
                            if (!list.Where(x => x.PowerName == attribute.MenuName && x.Level == 3).Any())
                            {

                                var moudle = list.Where(x => x.BussionModule == attribute.ModuleName && x.Level == 2).FirstOrDefault();

                                menuNum++;
                                T secondMenuItem = new T
                                {
                                    ParentCode = moudle != null ? moudle.PowerCode : attribute.SystemCode + moduleNum.ToString().PadLeft(2, '0'),
                                    PowerCode = moudle != null ? moudle.PowerCode + menuNum.ToString().PadLeft(2, '0') : attribute.SystemCode + moduleNum.ToString().PadLeft(2, '0') + menuNum.ToString().PadLeft(2, '0'),
                                    PowerName = attribute.MenuName,
                                    BussionModule = attribute.ModuleName,
                                    Status = 0,
                                    CreateTime = DateTime.Now,
                                    UpdateTime = DateTime.Now,
                                    Level = 3
                                };
                                list.Add(secondMenuItem);
                            }

                            //四级  功能
                            var actionInfo = type.GetMethods().Where(x => x.GetCustomAttribute(systemMenuAttribute.GetType()) != null).ToList();
                            var colltrollerName = type.Name.Split('.').LastOrDefault().Replace("Controller", "");

                            bool isMenu = false;
                            bool isModule = false;
                            actionNum = 0;
                            foreach (var action in actionInfo)
                            {
                                var actionName = action.Name;
                                actionNum++;

                                var menuAttribute = (SystemMenuAttribute)action.GetCustomAttribute(systemMenuAttribute.GetType());

                                //多个列表
                                if (!string.IsNullOrWhiteSpace(menuAttribute.MenuName) /*&& menuAttribute.ActionName == "浏览"*/)
                                {
                                    if (!list.Where(x => x.BussionModule == menuAttribute.ModuleName && x.Level == 2).Any())
                                    {
                                        moduleNum++;
                                        //二级  模块名称
                                        T secondmoduleItem = new T
                                        {
                                            ParentCode = attribute.SystemCode,
                                            PowerCode = attribute.SystemCode + moduleNum.ToString().PadLeft(2, '0'),
                                            PowerName = menuAttribute.ModuleName,
                                            BussionModule = menuAttribute.ModuleName,
                                            Status = 0,
                                            CreateTime = DateTime.Now,
                                            UpdateTime = DateTime.Now,
                                            Level = 2
                                        };

                                        isModule = true;
                                        list.Add(secondmoduleItem);
                                    }

                                    if (!list.Where(x => x.BussionModule == menuAttribute.ModuleName && x.PowerName == menuAttribute.MenuName && x.Level == 3).Any())
                                    {

                                        var sed = list.Where(x => x.BussionModule == menuAttribute.ModuleName && x.Level == 2).FirstOrDefault();
                                        //创建菜单名称
                                        menuNum++;

                                        T secondMenuItem = new T
                                        {
                                            ParentCode = sed.PowerCode,
                                            PowerCode = sed.PowerCode + menuNum.ToString().PadLeft(2, '0'),
                                            PowerName = menuAttribute.MenuName,             //菜单名称
                                            BussionModule = menuAttribute.ModuleName,     //模块名称
                                            Status = 0,
                                            ActionUrl = colltrollerName + '/' + actionName,
                                            CreateTime = DateTime.Now,
                                            UpdateTime = DateTime.Now,
                                            Level = 3
                                        };
                                        list.Add(secondMenuItem);
                                    }
                                    var subMenu = list.Where(x => x.PowerName == menuAttribute.MenuName && x.Level == 3).FirstOrDefault();
                                    //功能点
                                    T subThirdMenuitem = new T
                                    {
                                        ParentCode = subMenu.PowerCode,
                                        PowerCode = subMenu.PowerCode + actionNum.ToString().PadLeft(2, '0'),
                                        PowerName = menuAttribute.ActionName,
                                        BussionModule = attribute.MenuName,
                                        ActionUrl = colltrollerName + '/' + actionName,
                                        Status = 0,
                                        CreateTime = DateTime.Now,
                                        UpdateTime = DateTime.Now,
                                        Level = 4
                                    };

                                    list.Add(subThirdMenuitem);
                                }
                                else
                                {
                                    var subMenu = list.Where(x => x.PowerName == attribute.MenuName && x.Level == 3).FirstOrDefault();

                                    T thirdMenuitem = new T
                                    {
                                        //                  ParentCode = subMenu !=null? subMenu.PowerCode : attribute.SystemCode + moduleNum.ToString().PadLeft(2, '0') + menuNum1.ToString().PadLeft(2, '0'),
                                        //                  PowerCode = subMenu != null ? subMenu.PowerCode + actionNum.ToString().PadLeft(2, '0') :  attribute.SystemCode + moduleNum.ToString().PadLeft(3, '0') + menuNum1.ToString().PadLeft(2, '0') + actionNum.ToString().PadLeft(2, '0'),
                                        ParentCode = subMenu.PowerCode,
                                        PowerCode = subMenu.PowerCode + actionNum.ToString().PadLeft(2, '0'),
                                        PowerName = menuAttribute.ActionName,
                                        BussionModule = attribute.MenuName,
                                        ActionUrl = colltrollerName + '/' + actionName,
                                        Status = 0,
                                        CreateTime = DateTime.Now,
                                        UpdateTime = DateTime.Now,
                                        Level = 4
                                    };


                                    if (menuAttribute.ActionName == "浏览")
                                    {
                                        var menuitem = list.Where(x => x.PowerName == attribute.MenuName && x.Level == 3).FirstOrDefault();
                                        menuitem.ActionUrl = colltrollerName + '/' + actionName;
                                    }

                                    list.Add(thirdMenuitem);
                                }
                            }
                        }
                        continue;
                    }
                }
            }


            return list;
        }
    }
}
