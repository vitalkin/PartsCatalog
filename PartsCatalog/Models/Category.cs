using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
{
    [Table("Categories")]
    public class Category
    {
        public const string ImagesPath = "~/Resources/Images/Categories/";

        public int Id { get; set; }

        public string Name { get; set; }

        [Column("Parent_Id")]
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        public virtual Category Parent { get; set; }

        public virtual ICollection<Category> Children { get; set; }
    }
}