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

        public IEnumerable<T> Getenumablelist()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetIquerry()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQuery(Expression<Func<T, bool>> whereLambda)
        {
            return _unitofwork.GetDbContext().Set<T>().Where(whereLambda);
        }

        public async Task<List<T>> Select(Expression<Func<T, bool>> whereLambda)
        {
            return await _unitofwork.GetDbContext().Set<T>().Where(whereLambda).ToListAsync();
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


    }
}
