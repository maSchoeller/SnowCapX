using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Lib.Abstracts.Networking.SignalR
{
    public interface IMetaReporterHubModel
    {
        Task ReportLog();

        Task ReportImu();

        Task ReportMotors();

    }
}
