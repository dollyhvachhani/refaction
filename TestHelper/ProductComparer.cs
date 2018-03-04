using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHelper
{
    public class ProductComparer : IComparer, IComparer<Product>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Product;
            var rhs = actual as Product;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(Product expected, Product actual)
        {
            int temp;
            return (temp = expected.Id.CompareTo(actual.Id)) != 0 ? temp : expected.Name.CompareTo(actual.Name) ;
        }
    }
    public class ProductOptionsComparer : IComparer, IComparer<ProductOption>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as ProductOption;
            var rhs = actual as ProductOption;
            if (lhs == null || rhs == null) throw new InvalidOperationException();
            return Compare(lhs, rhs);
        }

        public int Compare(ProductOption expected, ProductOption actual)
        {
            int temp;
            return (temp = expected.Id.CompareTo(actual.Id)) != 0 ? temp : expected.Name.CompareTo(actual.Name);
        }
    }
    }
