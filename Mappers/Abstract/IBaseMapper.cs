using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Data.Abstract;

namespace TodoApi.Mappers.Abstract
{
    public interface IBaseMapper<TData, TModel> 
        where TData : IEntityBase
        where TModel : class
    {
        /// <summary>
        /// From Db to View model
        /// </summary>
        /// <param name="dbData"></param>
        /// <returns></returns>
        public TModel FromDb(TData dbData);

        /// <summary>
        /// From View to Db model
        /// </summary>
        /// <param name="viewModelData"></param>
        /// <returns></returns>
        public TData ToDb(TModel viewModelData);

        /// <summary>
        /// Update db model
        /// </summary>
        /// <param name="dbData"></param>
        /// <param name="viewModel"></param>
        public void UpdateDb(TData dbData, TModel viewModel);

    }
}
