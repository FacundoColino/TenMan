using System.Threading.Tasks;
using TenMan.Web.Data.Entities;
using TenMan.Web.Models;

namespace TenMan.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Request> ToRequestAsync(RequestViewModel model, bool isNew);
        RequestViewModel ToRequestViewModel(Request request);
    }
}