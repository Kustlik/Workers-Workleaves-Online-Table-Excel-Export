using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.Models
{
    public class Manager : Person
    {
        public int ManagerId { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
    }
}