using System;
using System.Collections.Generic;

#nullable disable

namespace DevTools.DAL.Models
{
    public partial class DevToolsObject
    {
        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectJson { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
