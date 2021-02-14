using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DevTools.DAL.Models
{
    [Table("DevToolsObject")]
    public partial class DevToolsObject
    {
        [Key]
        public int Id { get; set; }
        public Guid ObjectId { get; set; }
        [Required]
        [StringLength(128)]
        public string ObjectType { get; set; }
        [Required]
        public string ObjectJson { get; set; }
        [Required]
        [StringLength(128)]
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        [Required]
        [StringLength(128)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
