using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
	public interface IUserRepository
	{
		IEnumerable<User> GetUsers();
		User Get( string NRCId );
		User AddUser( User user, out int result );
		bool UpdateUser( User user, out int res );
		bool DeleteUser( string id );
		string ToggleStatus( int id );
	}
}
