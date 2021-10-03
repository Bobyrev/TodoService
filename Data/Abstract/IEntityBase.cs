using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApi.Data.Abstract
{
    public interface IEntityBase
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        long Id { get; set; }
    }
}
