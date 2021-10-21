using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parxlab.Common.Api;
using Parxlab.Common.Extensions;
using Parxlab.Controllers.Base;
using Parxlab.Data.Dtos.ParkUsage;
using Parxlab.Data.Dtos.Reserved;
using Parxlab.Entities;
using Parxlab.Entities.Enums;
using Parxlab.Repository;
using Parxlab.Service.Contracts;

namespace Parxlab.Controllers
{
    public class ParkUsageController:ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParkingCalculator _calculator;

        public ParkUsageController(IMapper mapper,IUnitOfWork unitOfWork,IParkingCalculator calculator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _calculator = calculator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateParkUsageDto dto)
        {
            var reserved = _mapper.Map<ParkUsage>(dto);
            reserved.ClientId = User.GetUserId();
            await _unitOfWork.ParkUsage.AddFast(reserved);
            return Ok(new ApiResult()
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CalculatePrice(Guid id)
        {
            var usage = await _unitOfWork.ParkUsage.Get(id);
            if (usage == null)
                return Ok(new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] { "Such park usage id doesn't exist" }
                });
            if (!usage.LeaveTime.HasValue)
                return Ok(new ApiResult()
                {
                    StatusCode = ApiResultStatusCode.BadRequest,
                    Errors = new[] { "Park usage must be ended first" }
                });
            var res = _calculator.Price((usage.LeaveTime - usage.ArrivalTime).Value, ParkType.Normal);
            return Ok(new ApiResult<string>
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success,
                Data = res + " TL"
            });
        }

    }
}
