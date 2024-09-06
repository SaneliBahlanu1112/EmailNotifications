using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Models
{
    public class SupplierDetailsViewModel
    {
        [Key]
        public string MAAANumber { get; set; }
        public string TradingName { get; set; }
        public string LegalName { get; set; }
        public string DisplayName { get; set; }
    }
}
