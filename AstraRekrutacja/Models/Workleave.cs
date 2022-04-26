using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.Models
{
    public class Workleave
    {
        public int WorkleaveId { get; set; }
        public int WorkerId { get; set; }
        public int Days { get; set; }
        public DateTime Created { get; set; }
        public DateTime StartOfWorkleave { get; set; }
        public WorkleaveStatus Status { get; set; }
        public int WorkleaveTypeId { get; set; }


    }

    public enum WorkleaveStatus
    {
        Canceled = -10,
        Draft = 0,
        New = 10,
        Pending = 20,
        Accepted = 30,
        Rejected = 100
    }
}