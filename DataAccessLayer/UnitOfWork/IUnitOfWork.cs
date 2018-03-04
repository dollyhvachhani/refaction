using DataAccessLayer.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        #region Properties
        GenericRepository<Product> ProductRepository { get; }
        GenericRepository<ProductOption> ProductOptionRepository { get; }
        GenericRepository<User> UserRepository { get; }
        #endregion

        #region Public methods
        /// <summary>
        /// Save method.
        /// </summary>
        void Save();
        #endregion
    }
}
