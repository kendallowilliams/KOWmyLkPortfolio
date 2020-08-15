using System;
using System.Collections.Generic;

namespace SampleAPI.DAL.Models
{
    public partial class APIProfileService
    {
        public APIProfileService()
        {
            APIAccessLog = new HashSet<APIAccessLog>();
        }

        public int Id { get; set; }
        public int APIProfileId { get; set; }
        public int APIServiceId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual APIProfile APIProfile { get; set; }
        public virtual APIService APIService { get; set; }
        public virtual ICollection<APIAccessLog> APIAccessLog { get; set; }
    }
}
