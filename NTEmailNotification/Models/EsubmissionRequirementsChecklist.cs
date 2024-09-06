using System.ComponentModel.DataAnnotations;

namespace EmailNotifications.Models
{
    public class EsubmissionRequirementsChecklist
    {
        [Key]
        public int Id { get; set; }
        public int EsubmissionRequirementsId { get; set; }
        public int EsubmissionId { get; set; }
        public string TenderNo { get; set; }
        public string Name { get; set; }
        public string Form { get; set; }
        public bool Completed { get; set; }
    }
}
