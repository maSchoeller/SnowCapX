using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SnowCapX.Lib.Abstracts.Networking.Grpc;
using SnowCapX.Lib.Abstracts.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SnowCapX.Server.Networking
{
    internal class GrpcServerSettingsService : SettingsService.SettingsServiceBase
    {
        private readonly GrpcSettingsProvider _provider;
        private readonly GrpcSettingsSychronisationTrigger _trigger;
        private List<IServerStreamWriter<SettingMessage>> _updateReceiver;
        private List<IServerStreamWriter<Empty>> _triggerReceiver;
        public GrpcServerSettingsService(
            GrpcSettingsProvider provider,
            GrpcSettingsSychronisationTrigger trigger)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _trigger = trigger ?? throw new ArgumentNullException(nameof(trigger));
            _updateReceiver = new List<IServerStreamWriter<SettingMessage>>();
            _provider.SettingChanged += async (s, e) =>
            {
                foreach (var receiver in _updateReceiver)
                {
                    await receiver.WriteAsync(new SettingMessage 
                    { 
                        Key = e.Key, 
                        Value = e.Value, 
                        RaiseFromSynchronization = e.RaiseFromSynchronization 
                    });
                }
            };
            _provider.SynchronizationRequested += async (s, e) =>
            {
                foreach (var receiver in _triggerReceiver)
                {
                    await receiver.WriteAsync(new Empty());
                }
            };
        }


        public override Task ReceiveUpdate(Empty request, IServerStreamWriter<SettingMessage> responseStream, ServerCallContext context)
        {
            _updateReceiver.Add(responseStream);
            return Task.CompletedTask;
        }

        public override Task<Empty> UpdateSetting(SettingMessage request, ServerCallContext context)
        {
            _provider.Set(request.Key, request.Value, request.RaiseFromSynchronization);
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> InvokeSynchronization(Empty request, ServerCallContext context)
        {
            _trigger.InvokeRequest();
            return Task.FromResult(new Empty());
        }

        public override Task GetSynchronizationNotificator(Empty request, IServerStreamWriter<Empty> responseStream, ServerCallContext context)
        {
            _triggerReceiver.Add(responseStream);
            return Task.CompletedTask;
        }


    }
}
