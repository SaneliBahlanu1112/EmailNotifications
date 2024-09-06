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
    public class Contract
    {
        public int ContractID { get; set; }
        public string ContractNumber { get; set; }
        public string SourceMethod { get; set; }
        public string Status { get; set; }
        public decimal ContractValue { get; set; }
        public decimal ContractAmountPaid { get; set; }
        public decimal ContractBalance { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public int TendersID { get; set; }
        public int DepartmentsID { get; set; }
        public int ProvinceID { get; set; }
        public string Notes { get; set; }
        public string Description { get; set; }
        public decimal PercentageEscalation { get; set; }
        public string EarlyTerminationClause { get; set; }
        public string PaymentMilestones { get; set; }
        public string PaymentPenaltyClause { get; set; }
        public DateTime? ContractCommenceDate { get; set; }
        public DateTime? ContractExpiryDate { get; set; }
        public string ContractDocument { get; set; }
        public string ContractDocumentGuid { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ReleaseId { get; set; }
        public string OCID { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
        public bool IsDeleted { get; set; }
        public string ReasonForDelete { get; set; }
        public int ContractTypeId { get; set; }
    }
}