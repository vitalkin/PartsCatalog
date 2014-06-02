using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
{
    [Table("Makes")]
    public class Make
    {
        public const string ImagesPath = "~/Resources/Images/Makes/";

        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public virtual ICollection<Model> Models { get; set; }
    }
}