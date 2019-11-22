using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IMetaReporterHub
    {
        static readonly string Path = "meta";
        static readonly string ReportLogMethodename = nameof(ReportLog);
        static readonly string ReportImuMethodename = nameof(ReportImu);
        static readonly string ReportMotorsMethodename = nameof(ReportMotors);

        Task ReportLog();

        Task ReportImu();

        Task ReportMotors();

    }
}
