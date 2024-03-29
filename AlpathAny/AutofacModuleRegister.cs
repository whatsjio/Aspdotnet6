﻿using Autofac;
using NLog;
using OperateService.Iservice;
using OperateService.Service;
using System.Reflection;
using ILogger = NLog.ILogger;

namespace AlpathAny
{
    /// <summary>
    /// Autofac注册类
    /// </summary>
    public class AutofacModuleRegister: Autofac.Module
    {
        //注册容器
        private static IContainer _container;

        public static IContainer GetContainer()
        {
            return _container;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.Register(c=>LogManager.GetCurrentClassLogger()).As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterAssemblyTypes(GetAssemblyByName("OperateService")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(GetAssemblyByName("MiddlewareService")).Where(a => a.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterBuildCallback(container => _container = (IContainer)container);
        }

        /// <summary>
        /// 根据程序集名称获取程序集
        /// </summary>
        /// <param name="AssemblyName">程序集名称</param>
        /// <returns></returns>
        public static Assembly GetAssemblyByName(String AssemblyName)
        {
            return Assembly.Load(AssemblyName);
        }
    }
}
