using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DataAccessLayer
{
	public static class FileWriter
	{
		static ReaderWriterLock locker = new ReaderWriterLock();
		public static List<User> LoadJson( string path )
		{
			List<User> users = new List<User>();
			if( File.Exists( path ) )
			{
				try
				{
					
					using( StreamReader r = new StreamReader( path ) )
					{
						string json = r.ReadToEnd();
						users = JsonConvert.DeserializeObject<List<User>>( json );
					}
				}
				catch
				{

				}
			}

			return users;
		}

		public static void WriteJSON( List<User> users, string path )
		{
			string json = JsonConvert.SerializeObject( users.ToArray() );

			try
			{
				locker.AcquireWriterLock( int.MaxValue );
				//write string to file
				System.IO.File.WriteAllText( path, json );
			}
			finally
			{
				locker.ReleaseWriterLock();
			}
		}
	}
}
