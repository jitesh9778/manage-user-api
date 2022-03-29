using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DataAccessLayer
{
	public static class Database
	{
		private static string DatabaseConnection = string.Empty;
		private static List<User> _users;		
		private static string path = HostingEnvironment.ApplicationPhysicalPath + "UserList.txt";

		static Database()
		{
			_users = FileWriter.LoadJson( path );
		}

		public static int GetMaxId()
		{
			if( _users != null && _users.Count > 0 )
			{
				return _users.Max( x => x.id ) + 1;
			}
			return 1;
		}

		public static User CreateUser( User user, out int result )
		{
			result = -1;
			try
			{
				// Check for duplicate names
				if( IsDuplicateNames( user, _users ) )
				{
					result = 1;
					return null;
				}
				// Check for duplicate nric
				if( _users.FindIndex( x => x.nric == user.nric ) != -1 )
				{
					result = 2;
					return null;
				}
				user.id = GetMaxId();
				_users.Add( user );
				result = 0;
				FileWriter.WriteJSON( _users, path );
			}
			catch( Exception )
			{

			}
			return user;
		}

		public static bool UpdateUser( User user, out int res )
		{
			res = -1;
			try
			{
				int index = _users.FindIndex( x => x.id == user.id );
				List<User> usrs = new List<User>( _users );
				usrs.RemoveAll( x => x.id == user.id );

				if( _users.FindIndex( x => x.id == user.id ) == -1 )
				{
					res = 1;
					return false;
				}
				if( usrs.FindIndex( x => x.nric == user.nric ) != -1 )
				{
					res = 2;
					return false;
				}
				if( IsDuplicateNames( user, usrs ) )
				{
					res = 3;
					return false;
				}

				res = 0;
				_users[index] = user;
				FileWriter.WriteJSON( _users, path );
			}
			catch( Exception )
			{

			}
			return true;
		}

		private static bool IsDuplicateNames( User user, List<User> users )
		{
			if( users.FindIndex( x => x.firstName == user.firstName && x.middleName == user.middleName && x.lastName == user.lastName ) != -1 )
			{
				return true;
			}
			return false;
		}

		public static bool DeleteUser( string nric )
		{
			try
			{
				User user = _users.Find( x => x.nric == nric );
				if( user == null )
				{
					return false;
				}

				_users.Remove( user );
				FileWriter.WriteJSON( _users, path );
			}
			catch( Exception )
			{

			}
			return true;
		}

		public static string ToggleStatus( int id )
		{
			User user = _users.Find( x => x.id == id );
			try
			{
				if( user == null )
				{
					return null;
				}

				if( user.status == UserStatus.Inactive || user.status == UserStatus.New )
				{
					user.status = UserStatus.Active;
					user.isActive = true;
				}
				else
				{
					user.status = UserStatus.Inactive;
					user.isActive = false;
				}
				FileWriter.WriteJSON( _users, path );
			}
			catch( Exception )
			{

			}
			return user.status.ToString();
		}

		public static User GetUser( string NRCID )
		{
			return _users.Find( x => x.nric == NRCID );
		}
		public static List<User> GetUsers()
		{
			return _users;
		}
	}
}
