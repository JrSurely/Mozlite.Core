﻿using System.Collections.Generic;
using Mozlite.Core;

namespace Mozlite.Mvc.AdminMenus
{
    /// <summary>
    /// 菜单提供者工厂接口。
    /// </summary>
    public interface IMenuProviderFactory : ISingletonService
    {
        /// <summary>
        /// 通过提供者名称获取菜单实例对象。
        /// </summary>
        /// <param name="provider">提供者名称。</param>
        /// <returns>返回当前提供者名称的菜单实例对象。</returns>
        IEnumerable<MenuItem> GetMenus(string provider);

        /// <summary>
        /// 获取当前选项。
        /// </summary>
        /// <param name="provider">提供者名称。</param>
        /// <param name="name">当前菜单唯一Id。</param>
        /// <returns>返回当前菜单项。</returns>
        MenuItem GetMenu(string provider, string name);
    }
}