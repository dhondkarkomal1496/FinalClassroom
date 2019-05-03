using DHTMLX.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinalClassroom.Models;
namespace DatabaseFistScheduler.Models
{
    public class CalendarModel
    {
        public IEnumerable<ClassRoom> Rooms { get; set; }
        public Dictionary<string, string> Colors { get; set; }
        public string CurrentID { get; set; }
        public DHXScheduler Scheduler { get; set; }
    }
}