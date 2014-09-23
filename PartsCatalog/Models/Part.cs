using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
{
    [Table("Parts")]
    public class Part
    {
        public const string ImagesPath = "~/Resources/Images/Parts/";

        public int Id { get; set; }

        public string Manufacturer { get; set; }

        public string Country { get; set; }

        public string Name { get; set; }

        public string Images { get; set; }

        [Column(TypeName="money")]
        public decimal? Price { get; set; }

        [Column("Category_Id")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Column("Model_Id")]
        [ForeignKey("Model")]
        public int ModelId { get; set; }

        public virtual Model Model { get; set; }

        public string[] GetImages()
        {
            return Images == null ? null : Images.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}