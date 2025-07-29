
using DotNetAPI.UserJobInfoModels;
using DotNetAPI.UserModels;
using DotNetAPI.UserSalaryModels;

namespace DotNetAPI.Data
{
    public class UserRepository : IUserRepository   // Connects the repository to the controller via interface using inheritance
    {
        // Access to entity framework context from this abstract class
        DataContextEF _entityFramework;
        public UserRepository(IConfiguration config)
        {
            _entityFramework = new DataContextEF(config);
        }


        // Similar to dapper. Basically a generic method wrapper around 
        // the entity framework context
        // Move methods from UserEFController to UserRepository
        // Aim is to have a generic and dynamic way to access the database
        #region Repository Generic Methods
        public bool SaveChanges()
        {
            return _entityFramework.SaveChanges() > 0;
        }

        // public bool AddEntity<T>(T entityToAdd)
        public void AddEntity<T>(T entityToAdd)
        {
            if (entityToAdd != null)
            {
                _entityFramework.Add(entityToAdd);
                //return true;
            }
            //return false;
        }

        public void RemoveEntity<T>(T entityToRemove)
        {
            if (entityToRemove != null)
            {
                _entityFramework.Remove(entityToRemove);
            }

        }

        #endregion

        #region Repository Peripheral Methods
        public IEnumerable<User> GetUsers()
        {
            IEnumerable<User> users = _entityFramework.Users.ToList<User>();

            return users;
        }

        public User GetSingleUser(int userId)
        {

            User? user = _entityFramework.Users
                .Where(u => u.UserId == userId)
                .FirstOrDefault<User>();
            if (user != null)
            {
                return user;
            }
            throw new Exception("Failed to Get Single User");
        }

        public UserSalary GetSingleUserSalary(int userId)
        {

            UserSalary? userSalary = _entityFramework.UserSalary
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserSalary>();
            if (userSalary != null)
            {
                return userSalary;
            }
            throw new Exception("Failed to Get Single User");
        }

        public UserJobInfo GetSingleUserJobInfo(int userId)
        {

            UserJobInfo? userJobInfo = _entityFramework.UserJobInfo
                .Where(u => u.UserId == userId)
                .FirstOrDefault<UserJobInfo>();
            if (userJobInfo != null)
            {
                return userJobInfo;
            }
            throw new Exception("Failed to Get Single User");
        }

        #endregion

    }
}