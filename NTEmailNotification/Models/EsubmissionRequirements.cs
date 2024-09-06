using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailNotifications.Models
{
    public class EsubmissionRequirements
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public int? ParentId { get; set; }
        public bool Required { get; set; }
        public bool Mandatory { get; set; }
        public string TenderNo { get; set; }
        public bool? Show { get; set; }
        public bool? Completed { get; set; }

        public virtual List<EsubmissionRequirements> requirements { get; set; }
    }
}
