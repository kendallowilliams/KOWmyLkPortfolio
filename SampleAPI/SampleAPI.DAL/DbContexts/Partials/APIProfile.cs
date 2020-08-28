using SampleAPI.DAL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAPI.DAL.Models
{
    public partial class APIProfile : IDataModel
    {
        [NotMapped]
        public bool HasAllServices { get; set; }
    }
}
