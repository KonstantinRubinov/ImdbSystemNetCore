using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;

namespace ImdbServerCore
{
	public class MongoUsersManager : IUsersRepository
	{
		private readonly IMongoCollection<UserModel> _users;
		private readonly AppSettings _appSettings;

		public MongoUsersManager(IOptions<AppSettings> appSettings, ImdbDatabaseSettings settings)
		{
			_appSettings = appSettings.Value;

			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_users = database.GetCollection<UserModel>(settings.UsersCollectionName);
		}

		public MongoUsersManager()
		{
			var client = new MongoClient(ConnectionStrings.ConnectionString);
			var database = client.GetDatabase(ConnectionStrings.DatabaseName);

			_users = database.GetCollection<UserModel>(ConnectionStrings.UsersCollectionName);
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
			return _users.Find(user => true).Project(user => new UserModel
			{
				userID = user.userID,
				userFirstName = user.userFirstName,
				userLastName = user.userLastName,
				userNickName = user.userNickName,
				userBirthDate = user.userBirthDate,
				userGender = user.userGender,
				userEmail = user.userEmail,
				userPassword = user.userPassword,
				userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null,
				userImdbPass = user.userImdbPass
			}).ToList();
		}

		public UserModel GetOneUserById(string id)
		{
			return _users.Find(Builders<UserModel>.Filter.Eq(user => user.userID, id)).Project(user => new UserModel
			{
				userID = user.userID,
				userFirstName = user.userFirstName,
				userLastName = user.userLastName,
				userNickName = user.userNickName,
				userBirthDate = user.userBirthDate,
				userGender = user.userGender,
				userEmail = user.userEmail,
				userPassword = user.userPassword,
				userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null,
				userImdbPass = user.userImdbPass
			}).SingleOrDefault();
		}

		public UserModel GetOneUserByName(string name)
		{
			return _users.Find(Builders<UserModel>.Filter.Eq(user => user.userNickName, name)).Project(user => new UserModel
			{
				userID = user.userID,
				userFirstName = user.userFirstName,
				userLastName = user.userLastName,
				userNickName = user.userNickName,
				userBirthDate = user.userBirthDate,
				userGender = user.userGender,
				userEmail = user.userEmail,
				userPassword = user.userPassword,
				userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null,
				userImdbPass = user.userImdbPass
			}).SingleOrDefault();
		}

		public UserModel AddUser(UserModel userModel)
		{
			string orPass = userModel.userPassword;
			userModel.userPassword = ComputeHash.ComputeNewHash(userModel.userPassword);

			if (!_users.Find(Builders<UserModel>.Filter.Eq(user => user.userID, userModel.userID)).Any())
			{
				_users.InsertOne(userModel);
			}
			
			UserModel tmpUserModel = ReturnUserByNamePassword(userModel.userNickName, userModel.userPassword);

			if (ComputeHash.ComputeNewHash(orPass).Equals(tmpUserModel.userPassword))
			{
				tmpUserModel.userPassword = orPass;
			}

			return tmpUserModel;
		}
		
		public UserModel UpdateUser(UserModel userModel)
		{
			string orPass = userModel.userPassword;
			userModel.userPassword = ComputeHash.ComputeNewHash(userModel.userPassword);

			_users.ReplaceOne(user => user.userID.Equals(userModel.userID), userModel);
			UserModel tmpUserModel = ReturnUserByNamePassword(userModel.userNickName, userModel.userPassword);

			if (ComputeHash.ComputeNewHash(orPass).Equals(tmpUserModel.userPassword))
			{
				tmpUserModel.userPassword = orPass;
			}

			return tmpUserModel;
		}

		public int DeleteUser(string id)
		{
			_users.DeleteOne(user => user.userID.Equals(id));

			return 1;
		}

		public LoginModel ReturnUserByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);
			if (checkUser == null)
				throw new ArgumentOutOfRangeException();

			return (from user in _users.AsQueryable()
					where user.userNickName.Equals(checkUser.userNickName)
					where user.userPassword.Equals(checkUser.userPassword)
					select new LoginModel
					{
						userNickName = user.userNickName,
						userImdbPass = user.userImdbPass,
						userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null
					}).SingleOrDefault();
		}

		public UserModel ReturnUserByNamePassword(string userNickName, string userPassword)
		{
			if (userNickName == null || userNickName.Equals(""))
				throw new ArgumentOutOfRangeException();
			if (userPassword == null || userPassword.Equals(""))
				throw new ArgumentOutOfRangeException();

			if (!CheckStringFormat.IsBase64String(userPassword))
			{
				userPassword = ComputeHash.ComputeNewHash(userPassword);
			}

			return (from user in _users.AsQueryable()
					where user.userNickName.Equals(userNickName)
					where user.userPassword.Equals(userPassword)
					select new UserModel
					{
						userID = user.userID,
						userFirstName = user.userFirstName,
						userLastName = user.userLastName,
						userNickName = user.userNickName,
						userBirthDate = user.userBirthDate,
						userGender = user.userGender,
						userEmail = user.userEmail,
						userPassword = user.userPassword,
						userPicture = user.userPicture != null ? "/assets/images/users/" + user.userPicture : null,
						userImdbPass = user.userImdbPass
					}).SingleOrDefault();
		}

		public string ReturnImdbPassByNamePassword(LoginModel checkUser)
		{
			checkUser.userPassword = ComputeHash.ComputeNewHash(checkUser.userPassword);
			if (checkUser == null)
				throw new ArgumentOutOfRangeException();
			string userImdbPass = "";

			UserModel userModel = _users.Find<UserModel>(user => user.userNickName.Equals(checkUser.userNickName) && user.userPassword.Equals(checkUser.userPassword)).FirstOrDefault();
			userImdbPass = userModel.userImdbPass;

			return userImdbPass;
		}

		public string ReturnUserIdByUserPass(string userPass)
		{
			string userId = "";
			if (userPass.Equals(string.Empty) || userPass.Equals(""))
				throw new ArgumentOutOfRangeException();
			userPass = ComputeHash.ComputeNewHash(userPass);

			UserModel userModel = _users.Find<UserModel>(user => user.userPassword.Equals(userPass)).FirstOrDefault();
			userId = userModel.userID;
			return userId;
		}

		public bool IsNameTaken(string name)
		{
			if (name.Equals(string.Empty) || name.Equals(""))
				throw new ArgumentOutOfRangeException();

			return _users.Find<UserModel>(user => user.userNickName.Equals(name)).Any();
		}
		
		public UserModel UploadUserImage(string id, string img)
		{
			UserModel tmpUserModel = _users.Find<UserModel>(user => user.userID.Equals(id)).FirstOrDefault();
			tmpUserModel.userImage = img;

			_users.ReplaceOne(user => user.userID.Equals(id), tmpUserModel);
			tmpUserModel = _users.Find<UserModel>(user => user.userID.Equals(id)).FirstOrDefault();

			return tmpUserModel;
		}

		public string ReturnUserIdByImdbPass(string imdbPass)
		{
			string userId = "";
			if (imdbPass.Equals(string.Empty) || imdbPass.Equals(""))
				throw new ArgumentOutOfRangeException();

			UserModel tmpUserModel = _users.Find<UserModel>(user => user.userImdbPass.Equals(imdbPass)).FirstOrDefault();
			userId = tmpUserModel.userID;

			return userId;
		}
	}
}
