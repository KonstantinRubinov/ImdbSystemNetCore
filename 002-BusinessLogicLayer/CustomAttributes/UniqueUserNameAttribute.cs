using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace ImdbServerCore
{
	public class UniqueUserNameAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null || value.ToString() == "")
				return ValidationResult.Success;

			string name = value.ToString();



			IUsersRepository usersManager=null;
			//InterfaceUsersLogic logic = new EntityUsersLogic();



			if (GlobalVariable.logicType == 1)
			{
				usersManager = new SqlUsersManager();
			}
			else if (GlobalVariable.logicType == 2)
			{
				usersManager = new MySqlUsersManager();
			}
			else
			{
				usersManager = new MongoUsersManager();
			}

			if (usersManager.IsNameTaken(name))
			{
				Debug.WriteLine("User name " + name + " already taken!");
				return new ValidationResult("User name " + name + " already taken!");
			}

			Debug.WriteLine("User name " + name + " is ok!");
			return ValidationResult.Success;
		}
	}
}
