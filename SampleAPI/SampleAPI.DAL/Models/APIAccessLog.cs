using System;
using System.Collections.Generic;

namespace SampleAPI.DAL.Models
{
    public partial class APIAccessLog
    {
        public int Id { get; set; }
        public int APIProfileServiceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual APIProfileService APIProfileService { get; set; }
    }
}
