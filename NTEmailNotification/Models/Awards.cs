using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmailNotifications.Models;
//using CloseExpiredTenders.Models;

namespace EmailNotifications.Models
{
    public class Awards
    {
        public Awards()
        {
            this.SubContractor = new List<SubContractor>() { new SubContractor() };
        }
        [Key]
        public int AwardID { get; set; }
        [Required]
        [DisplayName("Successful Bidder")]
        public string Company { get; set; }
        [Required]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [DisplayName("Enterprise Type")]
        public string EnterpriseType { get; set; }
        [DisplayName("Points Awarded")]
        public int? PointsAwarded { get; set; }
        [DisplayName("Award Date")]

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? AwardDate { get; set; }
        [DataType(DataType.Currency)]
        [DisplayName("Tender Amount(R)")]
        public string TenderAmount { get; set; }
        public string BEEScoring { get; set; }
        public int TendersID { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
        public string OCID { get; set; }
        public int ReleaseId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string ReasonForDelete { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }

        public virtual Tenders Tenders { get; set; }
        public List<SubContractor> SubContractor { get; set; }
    }

    public class SubContractor
    {
        [Key]
        public int SubContractorID { get; set; }
        [ForeignKey("AwardID")]
        public virtual Awards Awards { get; set; }
        public string SubContractorName { get; set; }
        public string BEEScoring { get; set; }
        public string SubContractingPercentage { get; set; }
        public bool BlackYouthownership { get; set; }
        public bool BlackWomanOwnership { get; set; }
        public bool BlackMilitaryVeteran { get; set; }
        public bool BlackDisabled { get; set; }
        public bool BlackCooperatives { get; set; }
        public bool BlackRural { get; set; }
    }
}
