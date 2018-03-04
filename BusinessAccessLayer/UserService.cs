using DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    /// <summary>
    /// Offers services for user specific operations
    /// </summary>
    public class UserService : IUserService
    {
        #region Private member variables
        private readonly UnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        /// <summary>
        /// Public constructor.
        /// </summary>
        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Services
        /// <summary>
        /// Public method to authenticate user by user name and password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Authenticate(string userName, string password)
        {
            var user = _unitOfWork.UserRepository.Get(u => u.UserName == userName && u.Password == password);
            if (user != null && user.UserId > 0)
            {
                return user.UserId;
            }
            return 0;
        }
        #endregion
    }
}
