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
                Statuses = isNew ? new List<Status>() : model.Statuses,
                ActualStatus = isNew ? "Generada" : model.ActualStatus,
                Remarks = model.Remarks,
                Worker = await _context.Workers.FindAsync(model.WorkerId),
                Images = isNew ? new List<RequestImage>() : model.Images,
                Unit = await _context.Units.FindAsync(model.UnitId)
            };
        }

        public RequestViewModel ToRequestViewModel(Request request)
        {
                return new RequestViewModel
                {
                    Id = request.Id,
                    Remarks = request.Remarks,
                    ActualStatus = request.ActualStatus,
                    StatusTypes = _combosHelper.GetComboStatusTypes(),
                    StatusTypeId = 0,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    Speciality = request.Speciality,
                    Statuses = request.Statuses,
                    Worker = request.Worker,
                    WorkerId = request.Worker.Id,
                    Images = request.Images,
                    Unit = request.Unit,
                    UnitId = request.Unit.Id,
                    SpecialtyId = request.Speciality.Id,
                    Specialties = _combosHelper.GetComboSpecialties()
                };
        }
    }
}
