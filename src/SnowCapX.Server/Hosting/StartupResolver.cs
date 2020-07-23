﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnowCapX.Server.Abstracts;
using SnowCapX.Server.Controlling;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnowCapX.Server.Hosting
{
    public static class StartupResolver
    {
        public static readonly string ConfigurePorcessChainMethodename = "ConfigureProcessChain";
        public static readonly string ConfigureServicesMethodename = "ConfigureServices";

        internal static object? CreateStartup(Type startuptype, HostBuilderContext context)
        {
            var type = startuptype;
            object? startup = null;

            if (!(type.GetConstructor(Type.EmptyTypes) is null))
            {
                startup = type.GetConstructor(Type.EmptyTypes).Invoke(Array.Empty<object>());
            }
            else if (!(type.GetConstructor(new Type[] { typeof(IConfiguration) }) is null))
            {
                startup = type.GetConstructor(new Type[] { typeof(IConfiguration) })
                    .Invoke(new object[] { context.Configuration });
            }
            else if (!(type.GetConstructor(new Type[] { typeof(IHostEnvironment) }) is null))
            {
                startup = type.GetConstructor(new Type[] { typeof(IHostEnvironment) })
                    .Invoke(new object[] { context.HostingEnvironment });
            }
            else if (!(type.GetConstructor(new Type[] { typeof(IConfiguration), typeof(IHostEnvironment) }) is null))
            {
                startup = type.GetConstructor(new Type[] { typeof(IConfiguration), typeof(IHostEnvironment) })
                    .Invoke(new object[] { context.Configuration, context.HostingEnvironment });
            }
            else if (!(type.GetConstructor(new Type[] { typeof(IHostEnvironment), typeof(IConfiguration) }) is null))
            {
                startup = type.GetConstructor(new Type[] { typeof(IHostEnvironment), typeof(IConfiguration) })
                    .Invoke(new object[] { context.HostingEnvironment, context.Configuration });
            }

            return startup;
        }

        internal static bool InvokeConfigureProcessChain(object startup, ProcessChainBuilder builder, HostBuilderContext context)
        {
            //Todo: Maybe later cover IServiceProvider for injecting every service.

            var startuptype = startup.GetType();
            var methode = startuptype.GetMethod(ConfigurePorcessChainMethodename, new[] { typeof(ProcessChainBuilder) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new[] { builder });
                return true;
            }
            methode = startuptype.GetMethod(ConfigurePorcessChainMethodename, new[] { typeof(ProcessChainBuilder), typeof(IConfiguration) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { builder, context.Configuration });
                return true;
            }
            methode = startuptype.GetMethod(ConfigurePorcessChainMethodename, new[] { typeof(ProcessChainBuilder), typeof(IConfiguration), typeof(IHostEnvironment) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { builder, context.Configuration, context.HostingEnvironment });
                return true;
            }
            methode = startuptype.GetMethod(ConfigurePorcessChainMethodename, new[] { typeof(ProcessChainBuilder), typeof(IHostEnvironment) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { builder, context.HostingEnvironment });
                return true;
            }
            methode = startuptype.GetMethod(ConfigurePorcessChainMethodename, new[] { typeof(ProcessChainBuilder), typeof(IHostEnvironment), typeof(IConfiguration) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { builder, context.HostingEnvironment, context.Configuration });
                return true;
            }
            return false;
        }

        internal static bool InvokeConfigureServices(object startup, IServiceCollection services, HostBuilderContext context)
        {
            var startuptype = startup.GetType();
            var methode = startuptype.GetMethod(ConfigureServicesMethodename, new[] { typeof(IServiceCollection) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new[] { services });
                return true;
            }
            methode = startuptype.GetMethod(ConfigureServicesMethodename, new[] { typeof(IServiceCollection), typeof(IConfiguration) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { services, context.Configuration });
                return true;
            }
            methode = startuptype.GetMethod(ConfigureServicesMethodename, new[] { typeof(IServiceCollection), typeof(IConfiguration), typeof(IHostEnvironment) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { services, context.Configuration, context.HostingEnvironment });
                return true;
            }
            methode = startuptype.GetMethod(ConfigureServicesMethodename, new[] { typeof(IServiceCollection), typeof(IHostEnvironment) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { services, context.HostingEnvironment });
                return true;
            }
            methode = startuptype.GetMethod(ConfigureServicesMethodename, new[] { typeof(IServiceCollection), typeof(IHostEnvironment), typeof(IConfiguration) });
            if (!(methode is null))
            {
                methode.Invoke(startup, new object[] { services, context.HostingEnvironment, context.Configuration });
                return true;
            }
            return false;
        }
    }
}
