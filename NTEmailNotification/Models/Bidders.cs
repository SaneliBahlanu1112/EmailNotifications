using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTendersAdmin.Models
{
    //Anything to do with the Bidders list
    public class Bidders
    {
        [Key]
        public int BiddersID { get; set; }
        [Required]
        [DisplayName("Bidder's Name")]
        public string BiddersName { get; set; }
        [Required]
        [DisplayName("Bidding Value")]
        public int BiddingValue { get; set; } = 0;
        [Required]
        [DisplayName("Contact Person")]
        public string ContactPerson { get; set; } = " ";
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; } = " ";
        [ForeignKey("ID")]
        public int TendersID { get; set; }
        public string ReasonForDelete { get; set; }
        public bool? IsDeleted { get; set; }
        //public virtual Tenders Tenders { get; set; }
        public string UpdatedBy { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = eTendersAdmin.Helper.CommonClass.DataFormatStringDate)]
        public DateTime? DateModified { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public string BidderListDocument { get; set; }
    }
}
