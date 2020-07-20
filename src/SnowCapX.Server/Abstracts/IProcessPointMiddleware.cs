using SnowCapX.Server.Enviroment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Abstracts
{
    public interface IProcessPointMiddleware
    {
        Task InvokeAsync(SnowCapContext context, ProcessPoint next);
    }
}
