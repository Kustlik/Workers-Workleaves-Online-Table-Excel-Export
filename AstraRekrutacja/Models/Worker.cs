using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.Models
{
    public class Worker : Person
    {
        [Key]
        public int WorkerId { get; set; }
        public int ManagerId { get; set; }
        public virtual ICollection<Workleave> Workleaves { get; set; }

    }
}