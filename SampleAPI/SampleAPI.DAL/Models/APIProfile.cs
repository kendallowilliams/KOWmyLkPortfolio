using System;
using System.Collections.Generic;

namespace SampleAPI.DAL.Models
{
    public partial class APIProfile
    {
        public APIProfile()
        {
            APIProfileService = new HashSet<APIProfileService>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<APIProfileService> APIProfileService { get; set; }
    }
}
