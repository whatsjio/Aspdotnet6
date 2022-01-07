using Microsoft.EntityFrameworkCore.Storage;
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

        /// <summary>
        /// 当前事务ID
        /// </summary>
        string CurrentTransactionId { get; }

        /// <summary>
        /// 开启事务后的单元 
        /// </summary>
        IDbContextTransaction ContextTransaction { get; }

        /// <summary>
        /// 异步开启事务
        /// </summary>
        /// <returns></returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// 异步回滚事务
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();
    }
}
