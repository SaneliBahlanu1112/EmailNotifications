using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Models
{
    public class Tenders
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Tender No")]
        [Required]
        public string Tender_No { get; set; }
        [Required]
        public string Description { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public string Organ_of_State { get; set; }
        public string Status { get; set; }
        public int ClustersID { get; set; }
        public DateTime? Closing_Date { get; set; }
        public DateTime? Date_Published { get; set; }

        public DateTime? Compulsory_briefing_session { get; set; }
        public string BriefingVenue { get; set; }
        public string Streetname { get; set; }
        public string Surburb { get; set; }
        public string Town { get; set; }
        public string Code { get; set; }
        public string Conditions { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Username { get; set; }

        public bool? BriefingSession { get; set; }
        public bool? BriefingCompulsory { get; set; }
        public int? Validity { get; set; }

        [ForeignKey("ProvincesID")]
        public int ProvincesID { get; set; }

        public virtual Provinces Provinces { get; set; }

        [ForeignKey("CategoriesID")]
        public int CategoriesID { get; set; }
        public int DepartmentsID { get; set; }
        public virtual Departments Departments { get; set; }
        public virtual Categories Categories { get; set; }
        public string BiddersListDocument { get; set; }
        public string BiddersListDocumentGuid { get; set; }

        //new additions
        public bool? ESubmission { get; set; }
        public bool? TwoEnvelopeSubmission { get; set; }

        public ICollection<SupportDocument> SupportDocument { get; set; }
        public ICollection<BiddersListDocuments> BiddersListDocuments { get; set; }
        public virtual List<QuestionsAndAnswer> QuestionsAndAnswers { get; set; }
        public virtual List<EsubmissionRequirements> EsubRequiredDocuments { get; set; }
        //public virtual List<Discussion> Discussions { get; set; }
        //public virtual Discussion Discussion { get; set; }

        public ICollection<Awards> Awards { get; set; }

        public ICollection<Bidders> Bidders { get; set; }
        public virtual Cancelled Cancelled { get; set; }
        public virtual Closed Closed { get; set; }
        public int ReleaseId { get; set; }
        public string OCID { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
    }
    public class SupportDocument
    {
        public Guid SupportDocumentID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int TendersID { get; set; }
        public bool Active { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public virtual Tenders Tenders { get; set; }

    }
    public class BiddersListDocuments
    {

        [Key]
        public int BidderListDocId { get; set; }

        public int TendersID { get; set; }
        public string BiddersListDocument { get; set; }
        public string BiddersListDocumentGuid { get; set; }
        public string UpdatedBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DateModified { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? CreateDate { get; set; }
    }

    public class Bidders
    {
        [Key]
        public int BiddersID { get; set; }
        [Required]
        [DisplayName("Company")]
        public string BiddersName { get; set; }
        [ForeignKey("ID")]
        public int TendersID { get; set; }
        //public virtual Tenders Tenders { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }

    public class Cancelled
    {
        [Key]
        public int CancellationID { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime? DateModified { get; set; }
        public int TendersID { get; set; }
    }
    public class Closed
    {
        [Key]
        public int ClosingID { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string UpdatedBy { get; set; }
        [Required]
        public DateTime? DateModified { get; set; }
        [Required]
        public DateTime? Closing_Date { get; set; }
        public int TendersID { get; set; }
    }

    public class AdditionalActions
    {
        [Key]
        public int ActionID { get; set; }
        public bool Notifiication { get; set; }
        public bool Bookmark { get; set; }
        public int TendersID { get; set; }
        public string userId { get; set; }
    }

    public class QuestionsAndAnswer
    {
        public int Id { get; set; }
        public string Tender_No { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Order_No { get; set; }
    }

    public class RequiredDocuments
    {
        public int Id { get; set; }
        public bool SBD1Required { get; set; }
        public bool SBD4Required { get; set; }
        public bool SBD6Required { get; set; }
        public bool SBD1 { get; set; }
        public bool SBD4 { get; set; }
        public bool SBD6 { get; set; }
    }

    public class Provinces
    {
        public Provinces()
        {
            Tenders = new HashSet<Tenders>();
        }
        [Key]
        public int ProvincesID { get; set; }
        public string Name { get; set; }
        public ICollection<Tenders> Tenders { get; set; }
    }

    public class Categories
    {
        public Categories()
        {
            Tenders = new HashSet<Tenders>();
        }
        [Key]
        public int CategoriesID { get; set; }
        public string Name { get; set; }
        public ICollection<Tenders> Tenders { get; set; }
    }

    public class Departments
    {
        public Departments()
        {
            Tenders = new HashSet<Tenders>();
        }
        [Key]
        public int DepartmentsID { get; set; }
        public string Name { get; set; }
        public ICollection<Tenders> Tenders { get; set; }
    }

    public class Clusters
    {
        public Clusters()
        {
            Tenders = new HashSet<Tenders>();
        }
        [Key]
        public int ClustersID { get; set; }
        public string Name { get; set; }
        public ICollection<Tenders> Tenders { get; set; }
    }
}
