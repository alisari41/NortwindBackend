using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable CommentTypo

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        //Veri tabanı nesnesi olduğunu anlatmak için IEntity kulladım. Sınıfımı çıplak bırakmamak için
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
    }
}
