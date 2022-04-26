using AstraRekrutacja.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AstraRekrutacja.JsonObjects
{
    public class WorkleavesJson
    {
        public string WorkerName { get; set; }
        public string ManagerName { get; set; }
        public DateTime WorkleaveStartDate { get; set; }
        public DateTime WorkleaveEndDate { get; set; }
        public string WorkleaveName { get; set; }
        public WorkleaveStatus Status { get; set; }
        public int DaysWithWeekends { get; set; }
        public char Span { get; set; }
    }
}