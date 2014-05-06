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
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
    }
}