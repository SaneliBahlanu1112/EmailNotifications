using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EmailNotifications;

namespace EmailNotifications.Models
{
    public class DeclarationOfInterest
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("CSDNumber")]
        [RegularExpression(@"^MAAA\d{7}$", ErrorMessage = "Please enter a valid 11-character MAAA number (e.g., MAAA0000001).")]
        public string CSDNumber { get; set; }

        [JsonPropertyName("tender_No")]
        public string TenderNo { get; set; }

        [JsonPropertyName("csdId")]
        public string CsdId { get; set; }

        [JsonPropertyName("nameOfBidder")]
        public string NameOfBidder { get; set; }

        [JsonPropertyName("idNumber")]
        public string IdNumber { get; set; }

        [JsonPropertyName("positionInCompany")]
        public string PositionInCompany { get; set; }

        [JsonPropertyName("companyRegistrationNumber")]
        public string CompanyRegistrationNumber { get; set; }

        [JsonPropertyName("taxRefNumber")]
        public string TaxRefNumber { get; set; }

        [JsonPropertyName("vatRegNumer")]
        public string VatRegNumer { get; set; }

        [JsonPropertyName("connectedToBidder")]
        public bool ConnectedToBidder { get; set; }

        [JsonPropertyName("conductBusiness")]
        public bool ConductBusiness { get; set; }

        [JsonPropertyName("relationshipWithBidder1")]
        public bool RelationshipWithBidder1 { get; set; }

        [JsonPropertyName("relationshipWithBidder2")]
        public bool RelationshipWithBidder2 { get; set; }

        [JsonPropertyName("relationshipWithBidder3")]
        public bool RelationshipWithBidder3 { get; set; }

        [JsonPropertyName("Directors")]
        public virtual List<EsubmissionSupplierDirectors> Directors { get; set; }
    }

    public class SupportingDocuments
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class EsubmissionSupplierDirectors
    {
        public int Id { get; set; }
        public string IDNumber { get; set; }
        public string TaxNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public int DeclarationId { get; set; }
        public string Name { get; set; }
    }

    public class Bidder
    {
        [Key]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("vatRegNo")]
        public string VatRegNo { get; set; }

        [JsonPropertyName("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonPropertyName("streetAddress")]
        public string StreetAddress { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("cellNumber")]
        public string CellNumber { get; set; }

        [JsonPropertyName("facsimileNumber")]
        public string FacsimileNumber { get; set; }

        [JsonPropertyName("taxCopmpliancePin")]
        public string TaxCopmpliancePin { get; set; }

        [JsonPropertyName("csdNumber")]
        public string CsdNumber { get; set; }

        [JsonPropertyName("tender_No")]
        public string Tender_No { get; set; }

        [JsonPropertyName("documentsAvailable")]
        public string DocumentsAvailable { get; set; }
    }

    public class Discussion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public List<Discussion> Discussions { get; set; }
        public string Question { get; set; }
        public string TenderNo { get; set; }
    }

    public class EsubmissionDocument
    {
        [Key]
        public Guid DocumentID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public int EsubmissionId { get; set; }
        public bool Active { get; set; }
        public string TypeOfDoc { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public virtual Esubmission Esubmission { get; set; }
    }

    public class Esubmission
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tender_No")]
        public string Tender_No { get; set; }

        [JsonPropertyName("tenderID")]
        public int TenderID { get; set; }



        [JsonPropertyName("CSDNumber")]
        [Required]
        public string CSDNumber { get; set; }

        [JsonPropertyName("bidderId")]
        public int? BidderId { get; set; }

        [JsonPropertyName("userEmail")]
        public string UserEmail { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("declarationOfInterest")]
        public DeclarationOfInterest DeclarationOfInterest { get; set; }

        [JsonPropertyName("supportingDocuments")]
        public ICollection<EsubmissionDocument> EsubmissionDocuments { get; set; }

        public List<EsubmissionRequirementsChecklist> EsubmissionRequirementsChecklist { get; set; }

        [JsonPropertyName("tender")]
        public Tenders Tender { get; set; }

        [JsonPropertyName("bidder")]
        public virtual Bidder Bidder { get; set; }

        [JsonPropertyName("formSection")]
        public string FormSection { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool? RequiredDocsUploaded { get; set; }

        [NotMapped]
        public List<SupplierDetailsViewModel> supplierDetails { get; set; }
        [NotMapped]
        public bool allChecklistsCompleted { get; set; }

    }
}
