using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
{
    [Table("Models")]
    public class Model
    {
        public const string ImagesPath = "~/Resources/Images/Models/";

        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Make")]
        [Column("Make_Id")]
        public int MakeId { get; set; }

        public virtual Make Make { get; set; }

        public string Images { get; set; }

        public short ProductionStart { get; set; }

        public short ProductionEnd { get; set; }

        public string[] GetImages()
        {
            return Images == null ? null : Images.Split(new [] {';'}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}