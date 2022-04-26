using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.Models
{
    public class WorkleaveType
    {
        public int WorkleaveTypeId { get; set; }
        public string WorkleaveName { get; set; }
        public string WorkleaveTemplate { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Workleave> Workleaves { get; set; }
    }
}