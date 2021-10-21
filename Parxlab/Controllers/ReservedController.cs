using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parxlab.Common.Api;
using Parxlab.Common.Extensions;
using Parxlab.Controllers.Base;
using Parxlab.Data.Dtos.Reserved;
using Parxlab.Entities;
using Parxlab.Repository;

namespace Parxlab.Controllers
{

    public class ReservedController:ApiBaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        public ReservedController(IMapper mapper,IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservedDto dto)
        {
            var reserved = _mapper.Map<Reserved>(dto);
            reserved.UserId = User.GetUserId();
            await unitOfWork.Reserved.AddFast(reserved);
            return Ok(new ApiResult()
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success
            });
        }

        [HttpPost("{sensorId}")]
        public async Task<IActionResult> Finish(string sensorId)
        {
           
            return Ok(new ApiResult()
            {
                IsSuccess = true,
                StatusCode = ApiResultStatusCode.Success
            });
        }
    }
}
