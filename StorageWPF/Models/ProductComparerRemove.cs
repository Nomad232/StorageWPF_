using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.Models
{
    public class ProductComparerRemove : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x.Name == y.Name && x.UM == y.UM)
            {
                x.Count -= y.Count;
            }
            return x.Name == y.Name && x.UM == y.UM;
        }

        public int GetHashCode(Product obj)
        {
            return HashCode.Combine(obj.Name, obj.UM);
        }
    }
}
