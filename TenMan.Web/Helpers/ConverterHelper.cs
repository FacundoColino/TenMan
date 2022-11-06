using System.Collections.Generic;
using System.Threading.Tasks;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TenMan.Web.Data;
using TenMan.Web.Data.Entities;
using TenMan.Web.Helpers;
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
            var request = new Request
            {
                Id = isNew ? 0 : model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Speciality = await _context.Specialties.FindAsync(model.SpecialtyId),
                Statuses = isNew ? new List<Status>() : model.Statuses,
                ActualStatus = isNew ? "Generada" : model.ActualStatus,
                Remarks = model.Remarks,
                Worker = await _context.Workers.Include(w => w.User).FirstOrDefaultAsync(w => w.Id == model.WorkerId),
                //FindAsync(model.WorkerId),
                Images = isNew ? new List<RequestImage>() : model.Images,
                Unit = await _context.Units.FindAsync(model.UnitId)
            };
            return request;
        }

        public RequestViewModel ToRequestViewModel(Request request)
        {
            RequestViewModel requestViewModel = new RequestViewModel
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
                Images = request.Images,
                Unit = request.Unit,
                UnitId = request.Unit.Id,
                SpecialtyId = request.Speciality.Id,
                Specialties = _combosHelper.GetComboSpecialties(),
                Workers = _combosHelper.GetComboWorkers()
            };
            if (request.Worker != null)
            {
                requestViewModel.Worker = request.Worker;
                requestViewModel.WorkerId = request.Worker.Id;
            }
            return requestViewModel;
        }
    }
}
