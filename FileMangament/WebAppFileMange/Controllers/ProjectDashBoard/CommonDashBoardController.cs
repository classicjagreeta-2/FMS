

using System.Web.Mvc;

namespace WEB_PRO17.Controllers.ProjectDashBoard
{
    [Authorize]
    public class CommonDashBoardController : BaseCommonController
    {
        // GET: CommonDashBoard
        public ActionResult CommonDashBoard()
        {
            
            return View();
        }
    }
}