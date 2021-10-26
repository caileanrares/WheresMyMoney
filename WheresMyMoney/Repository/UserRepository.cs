using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheresMyMoney.Models;
using WheresMyMoney.Models.DBObjects;

namespace WheresMyMoney.Repository
{
    public class UserRepository
    {
        private Models.DBObjects.WheresMyMoneyModelsDataContext dbContext;

        public UserRepository()
        {
            this.dbContext = new Models.DBObjects.WheresMyMoneyModelsDataContext();
        }

        public UserRepository(Models.DBObjects.WheresMyMoneyModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public UserModel GetUserById(Guid Id)
        {
            return MapObjectToModel(dbContext.Users.FirstOrDefault(x => x.UserId == Id));
        }

        public UserModel GetUserByEmail(string email)
        {

            return MapObjectToModel(dbContext.Users.FirstOrDefault(x => x.Email == email));
        }



        public void CreateUser(UserModel userModel)
        {
            userModel.UserId = Guid.NewGuid();

            
            
            if (dbContext.Users.FirstOrDefault(x=>x.Email==userModel.Email)==null)
            {
                dbContext.Users.InsertOnSubmit(MapModelToObject(userModel));
                dbContext.SubmitChanges();
            }

            
        }

        public void DeleteUser (Guid id)
        {
            var userToDelete = dbContext.Users.FirstOrDefault(x => x.UserId == id);

            if (userToDelete != null)
            {
                dbContext.Users.DeleteOnSubmit(userToDelete);
                dbContext.SubmitChanges();
            }
        }

        private User MapModelToObject(UserModel userModel)
        {
            var dbUser = new Models.DBObjects.User();

            if (userModel!=null)
            {
                dbUser.UserId = userModel.UserId;
                dbUser.FirstName = userModel.FirstName;
                dbUser.LastName = userModel.LastName;
                dbUser.Email = userModel.Email;
                return dbUser;
            }
            return null;
        }

        private UserModel MapObjectToModel(User dbUser)
        {
            var userModel = new Models.UserModel();
            if (dbUser != null)
            {
                userModel.UserId = dbUser.UserId;
                userModel.FirstName = dbUser.FirstName;
                userModel.LastName = dbUser.LastName;
                userModel.Email = dbUser.Email;

                return userModel;
            }
            return null;
        }
    }
}