using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ParkingSystem;
using Parxlab.Common.Api;
using Parxlab.Controllers.Base;
using Parxlab.Data.Dtos.Sensor;
using Parxlab.IocConfig.Hubs;
using Parxlab.Repository;
using Parxlab.Service.Contracts;
using Serilog;

namespace Parxlab.Controllers
{
    public class SensorController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<SensorHub> _sensorHub;
        private readonly ISensorManager _sensorManager;
        public SensorController(IUnitOfWork unitOfWork, IHubContext<SensorHub> sensorHub,ISensorManager sensorManager)
        {
            _unitOfWork = unitOfWork;
            _sensorHub = sensorHub;
            _sensorManager = sensorManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sensors = await _unitOfWork.Sensor.GetAllDto();
            return Ok(new ApiResult<IEnumerable<SensorDto>>()
            {
                IsSuccess = true,
                Data = sensors
            });
        }

        [HttpPost("{sensorId}")]
        public async Task<IActionResult> Start(string sensorId)
        {
            var res = await _unitOfWork.Sensor.StartListening(sensorId);
            if (res == 0)
                return Ok(new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Sensor status couldn't be updated,call system administrator"}
                });
            await _sensorHub.Clients.All.SendAsync("UpdateStatus", sensorId, 1);
            var sensor = await _unitOfWork.Sensor.Get(s => s.WPSDId == sensorId);
          //  BackgroundJob.Enqueue(() => _sensorManager.StartListener(sensor.Ip, sensor.Port));
            return Ok(new ApiResult()
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success
            });
        }

        [HttpPost("{sensorId}")]
        public async Task<IActionResult> Stop(string sensorId)
        {
            var res = await _unitOfWork.Sensor.StopListening(sensorId);
            if (res == 0)
                return Ok(new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] {"Sensor status couldn't be updated,call system administrator"}
                });
            await _sensorHub.Clients.All.SendAsync("UpdateStatus", sensorId, 0);
            RecurringJob.RemoveIfExists(sensorId);
            return Ok(new ApiResult
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success
            });
        }
    }
}