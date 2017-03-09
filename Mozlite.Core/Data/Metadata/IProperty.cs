﻿using System;
using Mozlite.Core;

namespace Mozlite.Data.Metadata
{
    /// <summary>
    /// 属性接口。
    /// </summary>
    public interface IProperty : IAnnotatable
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 类型。
        /// </summary>
        Type ClrType { get; }

        /// <summary>
        /// 声明此属性的类型。
        /// </summary>
        IEntityType DeclaringType { get; }

        /// <summary>
        /// 当前属性是否可以承载null值。
        /// </summary>
        bool IsNullable { get; }

        /// <summary>
        /// 是否为自增长属性。
        /// </summary>
        bool IsIdentity { get; }

        /// <summary>
        /// 获取当前属性值。
        /// </summary>
        /// <param name="instance">当前对象实例。</param>
        /// <returns>获取当前属性值。</returns>
        object Get(object instance);

        /// <summary>
        /// 设置当前属性。
        /// </summary>
        /// <param name="instance">当前对象实例。</param>
        /// <param name="value">属性值。</param>
        void Set(object instance, object value);
    }
}