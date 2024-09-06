using EmailNotifications.DataLayer;
using EmailNotifications.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NTEmailNotifications;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Mail;
//using System.Diagnostics.Contracts;

namespace NTEmailNotification
{
    public class NTEmailNotification
    {
        private readonly EmailNotificationsDBContext _dbContext;
        private readonly ILogger<NTEmailNotification> _logger;
        private readonly SmtpSettings _smtpSettings;

        public NTEmailNotification(EmailNotificationsDBContext dbContext, ILogger<NTEmailNotification> logger, IOptions<SmtpSettings> smtpSettings)
        {
            _dbContext = dbContext;
            _logger = logger;
            _smtpSettings = smtpSettings.Value;
        }
        [Function("NTEmailNotification")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)

        {
            _logger.LogInformation($"Function executed at: {DateTime.Now}");

            // Trigger both types of notifications
            NTBiddersNotificationToUser();
            ValidityEmailToSend();
            EmailToSendDraftNotifications();
            AwardEmailToSend();
            ContractExpiryEmailToSend();


        }

        private void NTBiddersNotificationToUser()
        {
            try
            {
                var targetDateForTenders = DateTime.UtcNow.Date;

                var tendersToNotify = _dbContext.Tenders
       .Where(t => t.Closing_Date.HasValue
                   && t.Closing_Date.Value.Date == targetDateForTenders.AddDays(-8).Date
                   && t.Status == "Closed")
       .Select(t => new
       {
           t.Tender_No,
           t.Username,
           t.Email
       })
       .ToList();


                foreach (var tender in tendersToNotify)
                {
                    if (string.IsNullOrWhiteSpace(tender.Username))
                    {
                        _logger.LogWarning($"No valid username found for Tender No: {tender.Tender_No}. Skipping notification.");
                        continue;
                    }

                    var message = $@"
                    <html>
                    <body>
                        Good day
                        <br/>
                        <br/>
                        Please note that as per Instruction No 9 of 2022 / 2023, the bidder's list must be published within 10 days of the closing date. <br/>
                        This deadline will be reached in 2 days for the tender below. Please proceed to load the bidder's list: <br/>
                        <strong>{tender.Tender_No}</strong> <br/>
                        For more information, please visit the National Treasury eTenders website. <br/>
                        <br/>
                        Kind regards,
                        <br />
                        National Treasury - eTenders

                    </body>
                    </html>";

                    Guid universalId = Guid.NewGuid();
                    //var emailSent = SendEmail(tender.Email, tender.Tender_No, message, "Tender Validity Period", universalId);
                    SendEmailAndSaveNotification(tender.Tender_No, tender.Email, "Bidders List(s)", message);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in NTEmailNotificationToUser: {ex.Message}");
            }
        }

        private void ValidityEmailToSend()
        {
            try
            {
                var targetDateForTenders = DateTime.UtcNow.Date;

                var tendersToNotify = _dbContext.Tenders
                    .Where(t => t.Status == "Closed"
                                && t.Closing_Date.HasValue
                                && t.Closing_Date.Value.Date == targetDateForTenders.AddDays(-7))
                    .Select(t => new
                    {
                        t.Tender_No,
                        t.Username,
                        t.Email
                    })
                    .ToList();

                foreach (var tender in tendersToNotify)
                {
                    if (string.IsNullOrWhiteSpace(tender.Email))
                    {
                        _logger.LogWarning($"No valid email found for Tender No: {tender.Tender_No}. Skipping notification.");
                        continue;
                    }

                    var message = $@"
                    <html>
                    <body>
                         Good Day
                    <br />
                    <br />
                    Kindly note that the validity period for for the tender below will expire in 7 days.<br/>
                    <strong>{tender.Tender_No}</strong><br/>
                    For more information please visit National Treasury eTenders website.<br/>

                    <br />
                    <br />

                    Kind regards,
                    <br />
                    National Treasury - eTenders
                    </body>
                    </html>";

                    Guid universalId = Guid.NewGuid();
                    //var emailSent = SendEmail(tender.Email, tender.Tender_No, message, "Tender Validity Period", universalId);
                    SendEmailAndSaveNotification(tender.Tender_No, tender.Email, "Tender Validity Period", message);




                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ValidityEmailToSend: {ex.Message}");
            }
        }

        private void EmailToSendDraftNotifications()
        {
            try
            {
                var targetDateForTenders = DateTime.UtcNow.Date;

                var tendersToNotify = _dbContext.Tenders
                    .Join(_dbContext.Esubmission,
                        tender => tender.ID,
                        submission => submission.TenderID,
                        (tender, submission) => new { tender, submission })
                    .Where(t => t.tender.Closing_Date.HasValue
                                && t.tender.Closing_Date.Value.Date == targetDateForTenders.AddDays(2)
                                && t.submission.Status == "Draft")
                    .Select(t => new
                    {
                        t.submission.UserEmail,
                        t.tender.Tender_No,
                        t.submission.BidderId
                    })
                    .ToList();

                foreach (var tender in tendersToNotify)
                {
                    if (string.IsNullOrWhiteSpace(tender.UserEmail))
                    {
                        _logger.LogWarning($"No valid email found for Tender No: {tender.Tender_No}. Skipping notification.");
                        continue;
                    }

                    var message = $@"
            <html>
            <body>
                Good day Supplier
            <br />
            <br />

            Kindly note that your pending submission as per details below will be closing in 2 days.  <br/>        
            <strong>{tender.Tender_No}</strong> <br/>
            For more information please visit National Treasury eTenders website. <br/>
            <br />
            <br />


            Kind regards,
            <br />
            National Treasury - eTenders
            <br />
            </body>
            </html>";



                    Guid universalId = Guid.NewGuid();
                    //var emailSent = SendEmail(tender.Email, tender.Tender_No, message, "Tender Validity Period", universalId);
                    SendEmailAndSaveNotification(tender.Tender_No, tender.UserEmail, "Pending Submission", message);


                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in ValidityEmailToSend: {ex.Message}");
            }
        }


        private void ContractExpiryEmailToSend()
        {
            try
            {
                DateTime targetDate = DateTime.UtcNow.Date;

                // Query the database to filter contracts expiring from today onwards
                var contractsToNotify = _dbContext.Contract
                    .Where(c => c.ContractExpiryDate.HasValue &&
                                c.ContractExpiryDate.Value.Date >= targetDate)
                    .Select(c => new
                    {
                        c.ContractNumber,
                        c.ContractExpiryDate,
                        c.CreatedBy
                    })
                    .ToList();

                // Define the ranges in days
                var notifyDays = new[] { 30, 60, 90, 120, 150, 180 };

                var contractsToNotifyFiltered = contractsToNotify
                    .Where(c =>
                    {
                        var daysUntilExpiry = (c.ContractExpiryDate.Value - targetDate).Days;
                        return notifyDays.Contains(daysUntilExpiry);
                    })
                    .ToList();

                foreach (var contract in contractsToNotifyFiltered)
                {
                    string emailTo = contract.CreatedBy; // Assuming a default email address
                    //string universalId = contract.CreatedBy;

                    // Construct the email message
                    int daysUntilExpiry = (contract.ContractExpiryDate.Value - targetDate).Days;

                    var message = $@"
                 <html>
                <body>
                     Good Day,<br />
                     <br/>
                     Kindly note that the contract listed below will expire.<br />
                     <strong>{contract.ContractNumber}</strong><br />
                     For more information, please visit the National Treasury eTenders website.<br />
                     <br/>
                     Kind regards,<br />
                     National Treasury - eTenders
               </body>
              </html>


                    ";

                    Guid universalId = Guid.NewGuid();
                    //var emailSent = SendEmail(tender.Email, tender.Tender_No, message, "Tender Validity Period", universalId);
                    SendEmailAndSaveNotification(contract.CreatedBy, contract.CreatedBy, "Contract Expiry", message);
                }
            }




            catch (Exception ex)
            {
                _logger.LogError($"Error in ContractExpiryEmailToSend: {ex.Message}");
            }
        }


        private void AwardEmailToSend()
        {
            try
            {
                var targetDateForTenders = DateTime.UtcNow.Date;
                var notifyDays = new[] { 60 };

                // First, retrieve relevant tenders into memory
                var tenders = _dbContext.Tenders
                    .Where(t => t.Closing_Date.HasValue
                                && t.Status == "closed"
                                && t.Closing_Date.Value.Year >= targetDateForTenders.Year
                                && !new[] { "awarded", "cancelled" }.Contains(t.Status))
                    .Select(t => new
                    {
                        t.Tender_No,
                        t.Email,
                        t.Closing_Date
                    })
                    .ToList(); // Bring the data into memory

                // Perform additional filtering and processing in memory
                var tendersToNotify = tenders
                    .Where(t => notifyDays.Contains((targetDateForTenders - t.Closing_Date.Value.Date).Days))
                    .ToList();

                foreach (var tender in tendersToNotify)
                {
                    if (string.IsNullOrWhiteSpace(tender.Email))
                    {
                        _logger.LogWarning($"No valid email found for Tender No: {tender.Tender_No}. Skipping notification.");
                        continue;
                    }

                    var message = $@"
                 <html>
                 <body>
                    Good day
                    <br/>
                    <br/>
                    Kindly note that as per Instruction No 9 of 2022/2023, tenders must be awarded 10 days after the successful bidder accepts the award.  <br/>
                    The tender below shows that it has not been awarded. Please remember to publish the award. <br/>
                    <strong>{tender.Tender_No}</strong> <br/>
                    For more information, please visit the National Treasury eTenders website.
                    <br />
                    <br/>
                    Kind regards,<br />
                    <br/>
                    National Treasury - eTenders <br/>
                 </body>
                 </html>";

                    Guid universalId = Guid.NewGuid();

                    SendEmailAndSaveNotification(tender.Tender_No, tender.Email, "Tender Awards", message);

                }
            }




            catch (Exception ex)
            {
                _logger.LogError($"Error in ContractExpiryEmailToSend: {ex.Message}");
            }
        }

        private bool SendEmail(string recipientEmail, string tenderNo, string htmlBody, string subject, Guid universalId)
        {
            try
            {
                // Define SMTP settings
                var smtpHost = "164.151.136.185";
                var smtpPort = 25;
                var senderEmail = "etenders@treasury.gov.za";
                var smtpUsername = "etenders@treasury.gov.za";
                var smtpPassword = "Anonymous"; // Consider securely handling this password

                using (var client = new SmtpClient(smtpHost, smtpPort)
                {
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = false // Set to true if SSL is required
                })
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = subject,
                        Body = htmlBody,
                        IsBodyHtml = true
                    };

                    mailMessage.To.Add(recipientEmail);

                    client.Send(mailMessage);
                    _logger.LogInformation($"Email sent to {recipientEmail} for Tender No: {tenderNo}");

                    return true;
                }
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError($"SMTP error sending email for Tender No: {tenderNo}. Exception: {smtpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error sending email for Tender No: {tenderNo}. Exception: {ex.Message}");
                return false;
            }
        }

        private void SendEmailAndSaveNotification(string tenderNo, string recipientEmail, string subject, string htmlBody)
        {
            // Start a database transaction
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Get the current date
                    DateTime today = DateTime.UtcNow.Date;
                    Guid universalId = Guid.NewGuid();
                    // Check if an email has already been sent today for this Tender_No
                    bool emailAlreadySent = _dbContext.Email_Notifications
                        .Any(e => e.Tender_No == tenderNo &&
                         e.email_to == recipientEmail &&
                         e.Email_sentdate.HasValue &&
                         e.Email_sentdate.Value.Date == today);
                    // Log the result of the date check
                    _logger.LogInformation($"Checking if email already sent today for Tender_No {tenderNo}. Result: {emailAlreadySent}");

                    if (emailAlreadySent)
                    {
                        _logger.LogInformation($"Email for Tender_No: {tenderNo} has already been sent today.");
                        return; // Exit the method without sending the email
                    }

                    // Send the email
                    bool emailSent = SendEmail(recipientEmail, tenderNo, htmlBody, subject, universalId);

                    if (emailSent)
                    {
                        // Log before saving to the database
                        _logger.LogInformation($"Email sent successfully. Saving notification for Tender_No {tenderNo}.");

                        var emailNotification = new Email_Notifications
                        {
                            Tender_No = tenderNo,
                            email_to = recipientEmail,
                            email_message = subject,
                            email_error = null,
                            Email_sentdate = DateTime.UtcNow, // Assign the current UTC time
                            Universal_Id = Guid.NewGuid(), // Generate a new Universal_Id if needed
                            emailsent_count = 1,
                            email_retries = 0,
                            email_retrydate = null
                        };

                        _dbContext.Email_Notifications.Add(emailNotification);
                        _dbContext.SaveChanges();

                        // Commit the transaction if everything was successful
                        transaction.Commit();
                    }
                    else
                    {
                        _logger.LogError($"Failed to send email for Tender No: {tenderNo}. No record will be saved.");
                        transaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogError("An error occurred while sending the email or saving the notification.");
                    _logger.LogError($"Error Message: {ex.Message}");
                    _logger.LogError($"Inner Exception: {ex.InnerException?.Message}");
                }
            }
        }

    }
}








