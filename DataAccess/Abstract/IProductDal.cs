﻿using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable CommentTypo

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {//Temel Veriye erişim operasyonları olacak
    }
}
