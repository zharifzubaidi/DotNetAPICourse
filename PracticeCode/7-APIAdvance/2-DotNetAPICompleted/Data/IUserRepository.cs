
// Interface for User Repository
// This interface defines methods for interacting with User data in the database
// It is implemented by the UserRepository class, which uses Entity Framework for data access.
// This allows for a clean separation of concerns and makes it easier to test and maintain the code.

using DotNetAPI.UserJobInfoModels;
using DotNetAPI.UserModels;
using DotNetAPI.UserSalaryModels;

namespace DotNetAPI.Data
{
    public interface IUserRepository
    {
        #region Repository Dynamic Methods

        public bool SaveChanges();
        public void AddEntity<T>(T entityToAdd);
        public void RemoveEntity<T>(T entityToRemove);

        #endregion

        #region Repository Peripheral Methods

        public IEnumerable<User> GetUsers();
        public User GetSingleUser(int userId);
        public UserSalary GetSingleUserSalary(int userId);
        public UserJobInfo GetSingleUserJobInfo(int userId);
        
        #endregion
    }
}