﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    #region snippet
    [Table("TodoItems")]
    public class TodoItem
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; }
    }
    #endregion
}