using System.Collections.Generic;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(
            DataContext context,
            ICombosHelper combosHelper
            )
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        //TODO: Revisar los comentarios de este método.
        public async Task<Request> ToRequestAsync(RequestViewModel model, bool isNew)
        {
            return new Request
            {
                Id = isNew ? 0 : model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Speciality = await _context.Specialties.FindAsync(model.SpecialtyId),
                Status = model.Status,
                Worker = model.Worker, // Poner WorkerID?
                Images = isNew ? new List<RequestImage>() : model.Images,
                Tenant = await _context.Tenants.FindAsync(model.TenantId)
            };
        }

        public RequestViewModel ToRequestViewModel(Request request)
        {
                return new RequestViewModel
                {
                    Id = request.Id,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    //Speciality = request.Speciality,
                    Status = request.Status,
                    Worker = request.Worker, // Poner WorkerID?
                    Images = request.Images,
                    Tenant = request.Tenant,
                    TenantId = request.Tenant.Id,
                    SpecialtyId = request.Speciality.Id,
                    Specialties = _combosHelper.GetComboSpecialties()
                };
        }
    }
}
