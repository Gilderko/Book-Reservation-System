using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVCProject.Controllers 
{
    public class BaseController : Controller
    {
        private int _pageSize = 3;

        protected int PageSize
        {
            get
            {                
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }
}