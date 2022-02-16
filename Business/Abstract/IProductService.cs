using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

// ReSharper disable CommentTypo

namespace Business.Abstract
{
    public interface IProductService
    {
        //Arayüz tarafında metod paremetresi sade olmalı
        IDataResult<Product> GetById(int productId);//Data başarlımı oldu başarısız mı onlarada bakıcam IDataResult ile
        IDataResult<List<Product>> GetList();
        IDataResult<List<Product>> GetListByCategory(int categoryId);//Categoriye göre ürünleri getir
        IResult Add(Product product);// Data döndürmek istemiyorum.Başarılı mı oldum başarısız mı onlara bakmak istiyorum.
        IResult Delete(Product product);
        IResult Update(Product product);

        IResult TransactionalOperation(Product product);//Satış yapmak için işlemleri kontrol edicem

    }
}
