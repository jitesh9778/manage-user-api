using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace Services
{
	public class UserService : IUserService
	{
		private IUserRepository _userRepo;


		public UserService( IUserRepository _IuserRepository)
		{
			_userRepo = _IuserRepository;
		}

		public User AddUser( User user, out int result )
		{
			return _userRepo.AddUser( user, out result );
		}

		public bool DeleteUser( string id )
		{
			return _userRepo.DeleteUser( id );
		}

		public User Get( string NRCId )
		{
			return _userRepo.Get( NRCId );
		}

		public IEnumerable<User> GetUsers()
		{
			return _userRepo.GetUsers();
		}

		public string ToggleStatus( int id )
		{
			return _userRepo.ToggleStatus( id );
		}

		public bool UpdateUser( User user, out int res )
		{
			return _userRepo.UpdateUser( user, out res );
		}
	}
}
