using System;
using System.Collections.Generic;

namespace SampleAPI.DAL.Models
{
    public partial class APIService
    {
        public APIService()
        {
            APIProfileService = new HashSet<APIProfileService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public int? DisabledResponseCode { get; set; }
        public string DisabledResponseMessage { get; set; }
        public string ServiceDefinedFields { get; set; }
        public string ConnectionInfo { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<APIProfileService> APIProfileService { get; set; }
    }
}
