using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PartsCatalog.Models
{
    [Table("Models")]
    public class Model
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual Make Make { get; set; }
    }
}