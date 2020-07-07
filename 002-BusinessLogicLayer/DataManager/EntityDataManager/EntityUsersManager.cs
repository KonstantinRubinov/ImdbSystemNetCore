using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ImdbServerCore
{
	public class EntityUsersManager : EntityBaseManager, IUsersRepository
	{
		private readonly AppSettings _appSettings;

		public EntityUsersManager(ImdbFavoritesEntities context, IOptions<AppSettings> appSettings):base(context)
		{
			_appSettings = appSettings.Value;
		}

		public UserModel Authenticate(string username, string password)
		{
			var user = ReturnUserByNamePassword(username, password);

			// return null if user not found
			if (user == null || user.userID == null || user.userImdbPass == null || user.userID.Equals("") || user.userImdbPass.Equals(""))
				return null;

			// authentication successful so generate jwt token
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.userID),
					new Claim(ClaimTypes.SerialNumber, user.userImdbPass)
				}),
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);

			// remove additiona data before returning
			UserModel userModel = new UserModel();
			userModel.usertoken = tokenHandler.WriteToken(token);
			userModel.userNickName = user.userNickName;
			userModel.userPicture = user.userPicture;
			return userModel;
		}

		public List<UserModel> GetAllUsers()
		{
			var resultQuary = imdbFavoritesEntities.USERS.Select(u => new UserModel
			{
				userID = u.userID,
				userFirstName = u.userFirstName,
				userLastName = u.userLastName,
				userNickName = u.userNickName,
				userBirthDate = u.userBirthDate,
				userGender = u.userGender,
				userEmail = u.userEmail,
				userPassword = u.userPassword,
				userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				userImdbPass = u.userImdbPass
			});

			//var resultSP = imdbFavoritesEntities.GetAllUsers().Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.ToList();
			//else
			//	return resultSP.ToList();
		}

		public UserModel GetOneUserById(string id)
		{
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userID.Equals(id)).Select(u => new UserModel
			{
				userID = u.userID,
				userFirstName = u.userFirstName,
				userLastName = u.userLastName,
				userNickName = u.userNickName,
				userBirthDate = u.userBirthDate,
				userGender = u.userGender,
				userEmail = u.userEmail,
				userPassword = u.userPassword,
				userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				userImdbPass = u.userImdbPass
			});

			//var resultSP = imdbFavoritesEntities.GetOneUserById(id).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}



		public UserModel GetOneUserByName(string name)
		{
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userNickName.Equals(name)).Select(u => new UserModel
			{
				userID = u.userID,
				userFirstName = u.userFirstName,
				userLastName = u.userLastName,
				userNickName = u.userNickName,
				userBirthDate = u.userBirthDate,
				userGender = u.userGender,
				userEmail = u.userEmail,
				userPassword = u.userPassword,
				userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				userImdbPass = u.userImdbPass
			});

			//var resultSP = imdbFavoritesEntities.GetOneUserByName(name).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}

		public UserModel AddUser(UserModel userModel)
		{
			//var resultSP = imdbFavoritesEntities.AddUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userImdbPass).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			UserModel user;
			//if (GlobalVariable.queryType == 0)
				user = AddUserQuery(userModel);
			//else
			//	user = resultSP.SingleOrDefault();

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}

			return user;
		}

		public UserModel UpdateUser(UserModel userModel)
		{
			//var resultSP = imdbFavoritesEntities.UpdateUser(userModel.userID, userModel.userFirstName, userModel.userLastName, userModel.userNickName, userModel.userBirthDate, userModel.userGender, userModel.userEmail, ComputeHash.ComputeNewHash(userModel.userPassword), userModel.userPicture, userModel.userImdbPass).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			UserModel user;
			//if (GlobalVariable.queryType == 0)
				user = UpdateUserQuery(userModel);
			//else
			//	user = resultSP.SingleOrDefault();

			if (ComputeHash.ComputeNewHash(userModel.userPassword).Equals(user.userPassword))
			{
				user.userPassword = userModel.userPassword;
			}
			return user;
		}

		public int DeleteUser(string id)
		{
			//var resultSP = imdbFavoritesEntities.DeleteUser(id);

			//if (GlobalVariable.queryType == 0)
				return DeleteUserQuery(id);
			//else
			//	return resultSP;
		}


		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userNickName.Equals(checkUser.userNickName)).Where(u => u.userPassword.Equals(checkUser.userPassword)).Select(u => new LoginModel
			{
				userNickName = u.userNickName,
				userImdbPass = u.userImdbPass,
				userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null
			});

			//var resultSP = imdbFavoritesEntities.ReturnUserByNamePassword(checkUser.userNickName, checkUser.userPassword).Select(u => new LoginModel
			//{
			//	userNickName = u.userNickName,
			//	userImdbPass = u.userImdbPass,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}

		public UserModel ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}

			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userNickName.Equals(userNickName)).Where(u => u.userPassword.Equals(userPassword)).Select(u => new UserModel
			{
				userID = u.userID,
				userFirstName = u.userFirstName,
				userLastName = u.userLastName,
				userNickName = u.userNickName,
				userBirthDate = u.userBirthDate,
				userGender = u.userGender,
				userEmail = u.userEmail,
				userPassword = u.userPassword,
				userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
				userImdbPass = u.userImdbPass
			});

			//var resultSP = imdbFavoritesEntities.ReturnUserByNameLogin(userNickName, userPassword).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			//if (GlobalVariable.queryType == 0)
				return resultQuary.SingleOrDefault();
			//else
			//	return resultSP.SingleOrDefault();
		}


		public string ReturnImdbPassByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userNickName.Equals(checkUser.userNickName)).Where(u => u.userPassword.Equals(checkUser.userPassword)).Select(u => new UserModel
			{
				userImdbPass = u.userImdbPass
			});

			//var resultSP = imdbFavoritesEntities.ReturnImdbPassByNamePassword(checkUser.userNickName, checkUser.userPassword).Select(u => new UserModel
			//{
			//	userImdbPass = u.userImdbPass
			//});

			UserModel userModel;
			//if (GlobalVariable.queryType == 0)
				userModel = resultQuary.SingleOrDefault();
			//else
			//	userModel = resultSP.SingleOrDefault();


			return userModel.userImdbPass;
		}

		public string ReturnUserIdByUserPass(string userPass)
		{
			userPass = ComputeHash.ComputeNewHash(userPass);
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userPassword.Equals(userPass)).Select(u => new UserModel
			{
				userID = u.userID
			});

			//var resultSP = imdbFavoritesEntities.ReturnUserIdByUserPass(userPass).Select(userID => new UserModel
			//{
			//	userID = userID
			//});

			UserModel userModel;
			//if (GlobalVariable.queryType == 0)
				userModel = resultQuary.SingleOrDefault();
			//else
			//	userModel = resultSP.SingleOrDefault();

			return userModel.userID;
		}



		public bool IsNameTaken(string name)
		{
			//if (GlobalVariable.queryType == 0)
				return imdbFavoritesEntities.USERS.Any(u => u.userNickName.ToLower().Equals(name.ToLower()));
			//else
			//{
			//	if (imdbFavoritesEntities.IsNameTaken(name).Equals(1))
			//		return true;
			//	else
			//		return false;
			//}
		}

		public UserModel UploadUserImage(string id, string img)
		{
			//var resultSP = imdbFavoritesEntities.UploadUserImage(id, img).Select(u => new UserModel
			//{
			//	userID = u.userID,
			//	userFirstName = u.userFirstName,
			//	userLastName = u.userLastName,
			//	userNickName = u.userNickName,
			//	userBirthDate = u.userBirthDate,
			//	userGender = u.userGender,
			//	userEmail = u.userEmail,
			//	userPassword = u.userPassword,
			//	userPicture = u.userPicture != null ? "/assets/images/users/" + u.userPicture : null,
			//	userImdbPass = u.userImdbPass
			//});

			//if (GlobalVariable.queryType == 0)
				return UploadUserImageQuery(id, img);
			//else
			//	return resultSP.SingleOrDefault();
		}




		public string ReturnUserIdByImdbPass(string imdbPass)
		{
			var resultQuary = imdbFavoritesEntities.USERS.Where(u => u.userImdbPass.Equals(imdbPass)).Select(u => new UserModel
			{
				userID = u.userID
			});

			//var resultSP = imdbFavoritesEntities.ReturnUserIdByImdbPass(imdbPass).Select(userID => new UserModel
			//{
			//	userID = userID
			//});

			UserModel userModel;
			//if (GlobalVariable.queryType == 0)
				userModel = resultQuary.SingleOrDefault();
			//else
			//	userModel = resultSP.SingleOrDefault();

			return userModel.userID;
		}












		private UserModel AddUserQuery(UserModel userModel)
		{
			string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
			USER user = new USER
			{
				userID = userModel.userID,
				userFirstName = userModel.userFirstName,
				userLastName = userModel.userLastName,
				userNickName = userModel.userNickName,
				userPassword = userPassword2,
				userEmail = userModel.userEmail,
				userGender = userModel.userGender,
				userBirthDate = userModel.userBirthDate,
				userPicture = userModel.userPicture,
				userImdbPass = userModel.userImdbPass
			};
			imdbFavoritesEntities.USERS.Add(user);
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
			return GetOneUserById(user.userID);
		}

		private UserModel UpdateUserQuery(UserModel userModel)
		{
			string userPassword2 = ComputeHash.ComputeNewHash(userModel.userPassword);
			USER user = imdbFavoritesEntities.USERS.Where(u => u.userID.Equals(userModel.userID)).SingleOrDefault();
			if (user == null)
				return null;
			user.userID = userModel.userID;
			user.userFirstName = userModel.userFirstName;
			user.userLastName = userModel.userLastName;
			user.userNickName = userModel.userNickName;
			user.userPassword = userPassword2;
			user.userEmail = userModel.userEmail;
			user.userGender = userModel.userGender;
			user.userBirthDate = userModel.userBirthDate;
			user.userPicture = userModel.userPicture;
			user.userImdbPass = userModel.userImdbPass;
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
			return GetOneUserById(user.userID);
		}

		private int DeleteUserQuery(string id)
		{
			USER user = imdbFavoritesEntities.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
			imdbFavoritesEntities.USERS.Attach(user);
			if (user == null)
				return 0;
			imdbFavoritesEntities.USERS.Remove(user);
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return 0;
			}
			return 1;
		}



		private UserModel UploadUserImageQuery(string id, string img)
		{
			USER user = imdbFavoritesEntities.USERS.Where(u => u.userID.Equals(id)).SingleOrDefault();
			if (user == null)
				return null;
			user.userPicture = img;
			try
			{
				imdbFavoritesEntities.SaveChanges();
			}
			catch (ValidationException ex)
			{
				Debug.WriteLine(ex.Message);
				return null;
			}
			return GetOneUserById(user.userID);
		}
	}
}
