using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageWPF.Models
{
    public class ProductComparerCopy : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x.Name == y.Name && x.UM == y.UM)
            {
                x.Count = y.Count;
                x.Dt = y.Dt;
                x.Price = y.Price;
            }
            return x.Name == y.Name && x.UM == y.UM;
        }

        public int GetHashCode(Product obj)
        {
            return HashCode.Combine(obj.Name, obj.UM);
        }
    }
}
