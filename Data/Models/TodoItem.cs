using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TodoApi.Data.Models
{
    using Abstract;

    #region snippet
    [Table("TodoItems")]
    public class TodoItem : IEntityBase
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [DefaultValue(false)]
        public bool IsComplete { get; set; }

        public string Secret { get; set; }
    }
    #endregion
}