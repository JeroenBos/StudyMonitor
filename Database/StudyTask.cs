namespace StudyMonitor.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StudyTask
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        public long Estimate { get; set; } // in units of ticks

        public virtual AspNetUser AspNetUser { get; set; }
    }
}