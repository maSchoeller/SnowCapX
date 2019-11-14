﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Core.Utilities
{
    public class GlobalExceptionCatcher
    {
        public GlobalExceptionCatcher(ILogger<GlobalExceptionCatcher>? logger)
        {
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                logger?.LogError(e.Exception, $"Exception from unobserved Task.");
            };

            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
            {
                var ex = e.ExceptionObject as Exception;
                logger?.LogError(ex, $"Global uncatched Exception.");
            };
        }
    }
}
