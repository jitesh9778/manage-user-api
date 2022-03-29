using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataAccessLayer
{
    public class User
    {
		public User()
		{

		}

		public User( int id, string NRCID, string firstName, string lastName, string middleName )
		{
			this.id = id;
			this.nric = NRCID;
			this.firstName = firstName;
			this.lastName = lastName;
			this.middleName = middleName;
			this.status = UserStatus.New;
			this.isActive = false;
		}

		public int id { get; set; }
		public string firstName { get; set; }
		public string middleName { get; set; }
		public string lastName { get; set; }
		public string nric { get; set; }

		[JsonConverter( typeof( StringEnumConverter ) )]
		public UserStatus status { get; set; }
		public bool isActive { get; set; }

		public string GetName()
		{
			return $"{this.firstName} {this.middleName} {this.lastName}";
		}
	}

	public enum UserStatus
	{
		New,
		Active,
		Inactive
	}
}
