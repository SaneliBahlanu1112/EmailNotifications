using System;

namespace EmailNotifications.Models
{
    public class Email_Notifications
    {
        

        public int Id { get; set; }
        public Guid Universal_Id { get; set; }
        public string email_to { get; set; }
        public string email_message { get; set; }
        public int email_sent { get; set; }
        public DateTime? Email_sentdate { get; set; }
        public int emailsent_count { get; set; }
        public int email_retries { get; set; }
        public DateTime? email_retrydate { get; set; }
        public string email_error { get; set; }

        public string Tender_No { get; set; }
    }
}
