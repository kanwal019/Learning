using Common.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Generics
{
    public class Calculator<TProduct> where TProduct : Product
    {
        public decimal CalculateDiscount(TProduct product)
        {
            return (product.Price / 10);
        }
    }
}
