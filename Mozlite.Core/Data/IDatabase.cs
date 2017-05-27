﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Mozlite.Data.Internal;

namespace Mozlite.Data
{
    /// <summary>
    /// 数据库接口。
    /// </summary>
    public interface IDatabase : IExecutor
    {
        /// <summary>
        /// 日志接口。
        /// </summary>
        ILogger Logger { get; set; }

        /// <summary>
        /// 开启一个事务并执行。
        /// </summary>
        /// <param name="executor">事务执行的方法。</param>
        /// <param name="timeout">等待命令执行所需的时间（以秒为单位）。默认值为 30 秒。</param>
        /// <param name="cancellationToken">取消标识。</param>
        /// <returns>返回事务实例对象。</returns>
        Task<bool> BeginTransactionAsync(Func<ITransaction, Task<bool>> executor, int timeout = 30, CancellationToken cancellationToken = new CancellationToken());

        /// <summary>
        /// 开启一个事务并执行。
        /// </summary>
        /// <param name="executor">事务执行的方法。</param>
        /// <param name="timeout">等待命令执行所需的时间（以秒为单位）。默认值为 30 秒。</param>
        /// <returns>返回事务实例对象。</returns>
        bool BeginTransaction(Func<ITransaction, bool> executor, int timeout = 30);
    }
}