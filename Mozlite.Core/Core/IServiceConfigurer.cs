﻿using Microsoft.Extensions.DependencyInjection;

namespace Mozlite.Core
{
    /// <summary>
    /// 服务配置接口。
    /// </summary>
    public interface IServiceConfigurer : IService
    {
        /// <summary>
        /// 配置服务方法。
        /// </summary>
        /// <param name="services">服务集合实例。</param>
        void ConfigureServices(IServiceCollection services);
    }
}