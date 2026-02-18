using System.Threading.Tasks;

namespace TenMan.Web.Services
{
    public interface IViewRenderService
    {
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
