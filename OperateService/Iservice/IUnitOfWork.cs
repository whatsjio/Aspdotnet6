using PlatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Iservice
{
    public interface IUnitOfWork:IDisposable
    {
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        DbTContext GetDbContext();

        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// 同步保存
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

    }
}
