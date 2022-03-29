using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccessLayer;
using Services;
using HttpContext = System.Web.HttpContext;

namespace ManageUserAPI.Controllers
{
	[RoutePrefix("api/user")]
    public class UserController : ApiController
    {
		private IUserService service;
		//private IRepository service;

		public UserController( IUserService _service )
		{
			service = _service;
		}

		[HttpGet]
		[Route( "Get/{id}" )]
		[ResponseType( typeof( User ) )]
		public HttpResponseMessage Get( string id )
		{
			User user = service.Get( id );
			if( user == null )
			{
				return Request.CreateResponse( System.Net.HttpStatusCode.NotFound, "user doesn't exist" );
			}
			return Request.CreateResponse( System.Net.HttpStatusCode.Accepted, user );
		}

		[HttpGet]
		[Route( "GetAll" )]
		[ResponseType( typeof( List<User> ) )]
		public HttpResponseMessage GetAll()
		{
			IEnumerable<User> users = service.GetUsers();
			return Request.CreateResponse( System.Net.HttpStatusCode.Accepted, users );
		}

		[HttpPost]
		[Route( "Create" )]
		public HttpResponseMessage Create( User user )
		{
			string message = "user crreated successfully";
			var newUser = service.AddUser( user, out int res );
			if( newUser == null && res == 2 )
			{
				message = "NRID already present, Use different NRID";
				return Request.CreateResponse( System.Net.HttpStatusCode.BadRequest, message );
			}
			else if( newUser == null && res == 1 )
			{
				message = "Please enter unique first name, last name, middle name";
				return Request.CreateResponse( System.Net.HttpStatusCode.BadRequest, message );
			}
			return Request.CreateResponse( System.Net.HttpStatusCode.OK, newUser );
		}

		[HttpPost]
		[Route( "Update" )]
		public HttpResponseMessage Update( User user )
		{
			string message = "user updated successfully";
			if( !service.UpdateUser( user, out int result ) )
			{
				if( result == 1 )
				{
					message = "user not exists.";
				}
				else if( result == 2 )
				{
					message = "Duplicate NRIC.";
				}
				else if( result == 3 )
				{
					message = "Duplicate Names.";
				}
				return Request.CreateResponse( System.Net.HttpStatusCode.NotFound, message );
			}
			return Request.CreateResponse( System.Net.HttpStatusCode.OK, message );
		}

		[HttpPost]
		[Route( "Delete/{id}" )]
		public HttpResponseMessage Delete( string id )
		{
			if( !service.DeleteUser( id ) )
			{
				return Request.CreateResponse( System.Net.HttpStatusCode.NotFound, "User not found" );
			}
			return Request.CreateResponse( System.Net.HttpStatusCode.Accepted, "user deleted successfully" );
		}

		[HttpGet]
		[Route( "ToggleStatus/{id}" )]
		public HttpResponseMessage ToggleStatus( int id )
		{
			string newStatus = service.ToggleStatus( id );
			if( newStatus == null )
			{
				return Request.CreateResponse( System.Net.HttpStatusCode.NotFound, "User not found" );
			}
			return Request.CreateResponse( System.Net.HttpStatusCode.Accepted, newStatus );
		}


	}
}
