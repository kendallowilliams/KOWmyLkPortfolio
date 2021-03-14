using System;
using System.Collections.Generic;

#nullable disable

namespace DevTools.DAL.Models
{
    public partial class DevToolsProcessorItem
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string RequestType { get; set; }
        public string RequestJson { get; set; }
        public string Message { get; set; }
        public bool IsError { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? EndedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
