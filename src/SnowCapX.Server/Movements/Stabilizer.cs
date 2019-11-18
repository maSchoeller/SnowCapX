using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SnowCapX.Lib.Abstracts.Regulations;
using SnowCapX.Lib.Abstracts.Settings;
using SnowCapX.Lib.Abstracts.Utilities;
using SnowCapX.Lib.Core.Regulations;
using SnowCapX.Server.Abstracts;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnowCapX.Server.Movements
{
    internal class Stabilizer : IHostedService
    {
        private readonly ILoopWorker _worker;
        private readonly IMovementTarget _target;
        private readonly IPidRegulator _xAxisRegulator;
        private readonly IPidRegulator _yAxisRegulator;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Stabilizer>? _logger;
        //private IMotorController? _motorController;
        //private IImuSensor? _imuSensor;

        public Stabilizer(
            ILoopWorkerFactory workerFactory,
            IPidRegulatorFactory regulatorFactory,
            ISettings setting,
            IMovementTarget target,
            IHostApplicationLifetime lifetime,
            ILogger<Stabilizer>? logger)
        {
            if (workerFactory is null)
            {
                throw new ArgumentNullException(nameof(workerFactory));
            }
            if (regulatorFactory is null)
            {
                throw new ArgumentNullException(nameof(regulatorFactory));
            }
            if (setting is null)
            {
                throw new ArgumentNullException(nameof(setting));
            }
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            _logger = logger;


            //hardwareManager.ConnectAsync().ContinueWith(result =>
            //{
            //    if (result.Result)
            //    {
            //        _motorController = hardwareManager.GetHardware<IMotorController>()
            //            ?? throw new ArgumentException($"Can't resolve object of type{nameof(IMotorController)}");
            //        _imuSensor = hardwareManager.GetHardware<IImuSensor>()
            //            ?? throw new ArgumentException($"Can't resolve object of type{nameof(IImuSensor)}");
            //    }
            //}, TaskScheduler.Default);
            _worker = workerFactory.Create("StabilizerLoop", Callback);
            var opt = setting.GetBinding<PidRegulatorSetting>("yAxisRegulator");
            opt.DGain = 4;
            _xAxisRegulator = regulatorFactory.Create("xAxisRegulator");
            _yAxisRegulator = regulatorFactory.Create("yAxisRegulator");

        }


        private void Callback(CancellationToken token)
        {
            //var TargetPos = Vector3.Normalize(_movement.Position);
            //var ActuelPos = Vector3.Normalize(new Vector3(_imuSensor!.GravityVector.X, _imuSensor.GravityVector.Y, -1 * _imuSensor.GravityVector.Y));
            //var movementX = (byte)_xAxisRegulator.Calculate(ActuelPos.X, TargetPos.X);
            //var movementY = (byte)_yAxisRegulator.Calculate(ActuelPos.Y, TargetPos.Y);
            //_motorController![0].Speed = movementX;
            //_motorController![2].Speed = (byte)(byte.MaxValue - movementX);
            //_motorController![1].Speed = movementY;
            //_motorController![3].Speed = (byte)(byte.MaxValue - movementY);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger?.LogInformation("Stabilizer loop is starting.");
            //Todo: Check if Hardware is connected AND server is started, then start control loop.
            _lifetime.ApplicationStarted.Register(() =>
            {
                _logger?.LogInformation("Stabilizer loop is running.");
                _worker.StartWorker();

            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (_worker.IsRunning)
            {
                _worker.StopWorker();
            }
            _logger?.LogInformation("Stabilizer loop is stopping.");
            return Task.CompletedTask;
        }
    }
}
