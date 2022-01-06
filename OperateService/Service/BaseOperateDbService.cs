using DateModel;
using Microsoft.EntityFrameworkCore;
using OperateService.Iservice;
using PlatData;
using PlatData.SysTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Service
{
    /// <summary>
    /// 数据库基础操作类
    /// </summary>
    public class BaseOperateDbService<T> : BaseService, IBaseOperateDbService<T> where T : SysObject
    {


        private readonly IUnitOfWork _unitofwork;

        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseOperateDbService(IUnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
        }

        /// <summary>
        /// 数据表
        /// </summary>
        public virtual DbSet<T> Table => _unitofwork.GetDbContext().Set<T>();


        public async Task<Message> CreateNew(T table)
        {
            try
            {
                var addinsult= await _unitofwork.GetDbContext().Set<T>().AddAsync(table);
            }
            catch (Exception e)
            {
                base.MessageDate.IsSucess = false;
                base.MessageDate.MessageStr = e.Message;
            }
           return base.MessageDate;
        }

        public Message DeleteTable(T table)
        {
            try
            {
                var reletresult = _unitofwork.GetDbContext().Set<T>().Remove(table);
            }
            catch (Exception e)
            {
                base.MessageDate.IsSucess = false;
                base.MessageDate.MessageStr = e.Message;
            }
            return base.MessageDate;
        }

        public IEnumerable<T> Getenumablelist(Expression<Func<T, bool>> whereLambda)
        {
            return _unitofwork.GetDbContext().Set<T>().Where(whereLambda).ToList();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> whereLambda)
        {
            return _unitofwork.GetDbContext().Set<T>().Where(whereLambda);
        }

        public async Task<List<T>> Select(Expression<Func<T, bool>> whereLambda)
        {
            return await _unitofwork.GetDbContext().Set<T>().Where(whereLambda).AsNoTracking().ToListAsync();
        }

        public T First(Expression<Func<T, bool>> whereLambda)
        {
            return _unitofwork.GetDbContext().Set<T>().FirstOrDefault(whereLambda);
        }

        public T FirstNoTrack(Expression<Func<T, bool>> whereLambda)
        {
            return _unitofwork.GetDbContext().Set<T>().AsNoTracking().FirstOrDefault(whereLambda);
        }

        public Message Update(T entity)
        {
            try
            {
                var upresult = _unitofwork.GetDbContext().Set<T>().Update(entity);
            }
            catch (Exception e)
            {
                base.MessageDate.IsSucess = false;
                base.MessageDate.MessageStr = e.Message;
            }
            return base.MessageDate;
        }

        /// <summary>
        /// 保存所有track修改
        /// </summary>
        /// <returns></returns>
        public async Task<Message<int>> SaveTrackAsync()
        {
            try
            {
                var savecount = await _unitofwork.SaveChangesAsync();
                return new Message<int>(savecount);
            }
            catch (Exception e)
            {
                return new Message<int>(false, e.Message);
            }
        }


    }
}
