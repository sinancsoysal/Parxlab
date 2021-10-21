using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Parxlab.Common.Api;
using Parxlab.Controllers.Base;
using Parxlab.Entities.Enums;
using Parxlab.Repository;
using Parxlab.Service.Contracts;

namespace Parxlab.Controllers
{
    public class ParkController:ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        public ParkController(IUnitOfWork unitOfWork,IParkingCalculator calculator)
        {
            this.unitOfWork = unitOfWork;
        }

        

    }
}
