using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

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

        //当前事务guid
        private Guid _currenttransactionid => _trans==null?default(Guid): _trans.TransactionId;

        /// <summary>
        /// 当前事务ID
        /// </summary>
        public string CurrentTransactionId=> _currenttransactionid.ToString();

        /// <summary>
        /// 指示数据库是否支持事务保存点
        /// </summary>
        public bool SupportsPoints => _trans!=null? _trans.SupportsSavepoints:false;


        /// <summary>
        ///开启事务后的单元 
        /// </summary>
        public IDbContextTransaction ContextTransaction => _trans;


        /// <summary>
        /// DI
        /// </summary>
        /// <param name="dbTContext"></param>
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
        /// 异步开启事务
        /// </summary>
        /// <returns></returns>
        public async Task BeginTransactionAsync()
        {
            _trans =await dbContext.Database.BeginTransactionAsync();
        }
        

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit() => _trans?.Commit();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            await _trans?.CommitAsync();
        }


        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback() => _trans?.Rollback();

        /// <summary>
        /// 异步回滚事务
        /// </summary>
        /// <returns></returns>
        public async Task RollbackAsync() {
           await _trans?.RollbackAsync();
        }


        /// <summary>
        /// 执行原始sql示例
        /// </summary>
        //public void exectcomd() {
        //    dbContext.Set<SysAdmin>().FromSqlInterpolated();
        //    dbContext.Database.ExecuteSqlInterpolated()
        //}


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
