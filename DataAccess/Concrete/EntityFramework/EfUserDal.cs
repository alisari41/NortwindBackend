using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {//Bir kullanıcının rollerini çekmek istiyorum.
         //Beni bunu yapmam için join işlemi yapmam lazım Yani 2 tabloyu birleştirmem lazım.

            using (var context = new NorthwindContext())
            {//Gelen User bilgilerinin join işlemleri ile rollerini listeledim

                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id//sınırlandırma yaptım
                             select new OperationClaim
                             {
                                 //Burdan bir operationclaim rol listesi döndürcem
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name

                             };
                return result.ToList();
            }
        }
    }
}
