using Parxlab.Controllers.Base;
using Parxlab.Repository;

namespace Parxlab.Controllers
{
    public class InvoiceController:ApiBaseController
    {
        private readonly IUnitOfWork unitOfWork;
        public InvoiceController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

    }
}
