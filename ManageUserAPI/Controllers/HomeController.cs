using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;
using HttpContext = System.Web.HttpContext;

namespace ManageUserAPI.Controllers
{	
	public class HomeController : ApiController
	{
		

		public HttpResponseMessage Index()
		{
			return Request.CreateResponse( HttpStatusCode.Accepted );
		}

		

	}
}
