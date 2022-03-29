using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	public class UserRepository : IUserRepository
	{
		public User AddUser( User user, out int result )
		{
			return Database.CreateUser( user, out result );
		}

		public bool DeleteUser( string id )
		{
			return Database.DeleteUser( id );
		}

		public User Get( string id )
		{
			return Database.GetUser( id );
		}

		public IEnumerable<User> GetUsers()
		{
			return Database.GetUsers();
		}

		public string ToggleStatus( int id )
		{
			return Database.ToggleStatus( id );
		}

		public bool UpdateUser( User user, out int res )
		{
			return Database.UpdateUser( user, out res );
		}
	}
}
