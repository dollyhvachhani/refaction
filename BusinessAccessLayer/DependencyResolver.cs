using Resolver;
using System.ComponentModel.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    [Export(typeof(IComponent))]
    public class DependencyResolver : IComponent
    {
        #region Methods
        public void SetUp(IRegisterComponent registerComponent)
        {
            registerComponent.RegisterType<IProductService, ProductService>();
            registerComponent.RegisterType<IUserService, UserService>();
        }
        #endregion
    }
}
