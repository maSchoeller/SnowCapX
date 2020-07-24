using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.ComponentModel;
using System.Text;

namespace SnowCapX.Core.Abstratcs
{
    public interface ISettingsManager
    {
        TValue GetValue<TValue>(string path) 
            where TValue : struct;
        TProxy GetBinding<TProxy>(string path) 
            where TProxy : INotifyPropertyChanged;
        TObject GetObject<TObject>(string path);
    }
}
