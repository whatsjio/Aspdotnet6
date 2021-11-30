using OperateService.Iservice;
using PlatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Service
{
    public class UnitOfWork: IUnitOfWork
    {
        /// <summary>
        /// Db实体
        /// </summary>
        private readonly DbTContext dbContext;
        public UnitOfWork(DbTContext dbTContext)
        {
            dbContext = dbTContext;
        }

        public DbTContext GetDbContext()
        {
            return dbContext;
        }

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
