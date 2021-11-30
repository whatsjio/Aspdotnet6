using DateModel;
using PlatData.SysTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OperateService.Iservice
{
    /// <summary>
    /// 数据库基础操作接口
    /// </summary>
    public interface IBaseOperateDbService<T>: IBaseService where T : SysObject
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Task<Message> CreateNew(T table);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        Message Update(T entity);


        /// <summary>
        /// 获取内存中数据列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> Getenumablelist();

        /// <summary>
        /// 获取数据列表查询
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetIquerry();

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        Message DeleteTable(T table);

        /// <summary>
        /// 不跟踪查询列表
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<List<T>> Select(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<T> GetQuery(Expression<Func<T, bool>> whereLambda);

    }
}
