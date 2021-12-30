using Microsoft.EntityFrameworkCore.Storage;
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
        private bool disposedValue;
        //事务单元
        private IDbContextTransaction _trans = null;


        public UnitOfWork(DbTContext dbTContext)
        {
            dbContext = dbTContext;
        }

        public DbTContext GetDbContext()
        {
            return dbContext;
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction() {
            _trans = dbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit() => _trans?.Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback() => _trans?.Rollback();


        /// <summary>
        /// 异步保存
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges() {
            return dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)
                }
                dbContext.Dispose();
                // TODO: 释放未托管的资源(未托管的对象)并重写终结器
                // TODO: 将大型字段设置为 null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
