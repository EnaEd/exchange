using Exchange.Web.DataAccess.ApplicationContext;
using Exchange.Web.DataAccess.Entities;
using Exchange.Web.DataAccess.Models;
using Exchange.Web.DataAccess.Repositories.Base;
using Exchange.Web.DataAccess.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exchange.Web.DataAccess.Repositories
{
    public class PhotoRepository : BaseEFRepository<PhotoEntity>, IPhotoRepository<PhotoEntity>
    {

        public PhotoRepository(AppContextDb appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<PhotoEntity>> GetOneByFilter(FilterModel filterModel)
        {
            var UserId = new SqlParameter("@UserId", filterModel?.UserId ?? (object)DBNull.Value);
            var CategoryId = new SqlParameter("@CategoryId", filterModel?.CategoryId ?? (object)DBNull.Value);
            var City = new SqlParameter("@City", filterModel?.City ?? (object)DBNull.Value);
            var Country = new SqlParameter("@Country", filterModel?.Country ?? (object)DBNull.Value);


            var result = await Task.Run(() => DbSet.FromSqlRaw<PhotoEntity>(
                 $"spGetFilteredPhoto @UserId,@CategoryId,@City,@Country",
                 UserId, CategoryId, City, Country).ToListAsync());

            return result.Skip((int)filterModel.SkippedCount).Take(10);

        }
    }
}
