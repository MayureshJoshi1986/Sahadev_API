//***********************************************************************************************//
/*  Copy right issue :- This source file is property of Millicent Technologies.                 *
 *  --------------------------------------------------------------------------------------------*
 *  Class Name      :- Mail                                                                     *
 *  --------------------------------------------------------------------------------------------*
 *  Description     :- This class is use for shoot SMTP mail   
 *  --------------------------------------------------------------------------------------------*
 *  Created By      :- MS                                                                       *                                                                 
 *  --------------------------------------------------------------------------------------------*
 *  Created Date    :- 10/Apr/2014                                                              *
 *  --------------------------------------------------------------------------------------------* 
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 *  revised By      :-                                                                          *
 *  revised Details :-                                                                          *
 //**********************************************************************************************/
using System;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Collections.Generic;
using Serilog;

namespace SahadevUtilities.Email
{
    /// <summary>
    ///  this class get SMTP setting from config file and shoots mail
    /// </summary>
    public class Mail
    {
        #region Variables
        //private variables
        private string smtpHost = "";
        private bool isReturn = false;
        private int smtpPort;
        static string _className = "SahadevUtilities.Email.Mail";
        //public variables
        public bool isBodyTypeHtml { get; set; }

        #endregion

        #region Constructor to get SMTP Setting

        /// <summary>
        /// default construct set host and port with config file
        /// </summary>
        public Mail()
        {
            try
            {
                // validate SMTP setting in config file
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TEST_SmtpHost"]))
                    throw new Exception("Email host address cannot be empty");
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TEST_SmtpPort"]))
                    throw new Exception("Email port address cannot be empty");

                // read SMTP settings 
                smtpHost = ConfigurationManager.AppSettings["TEST_SmtpHost"].ToString();
                smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["TEST_SmtpPort"].ToString());
            }
            catch (ConfigurationException ex)
            {
                // writes error into log file
                Log.Error(ex, _className, "Mail");
                throw ex;
            }
        }

        /// <summary>
        ///  Set SMTP host and port 
        /// </summary>
        /// <param name="host">host name</param>
        /// <param name="port">port number</param>
        public Mail(string host, int port)
        {
            try
            {
                // assign values for host and port
                smtpHost = host;
                smtpPort = port;
            }
            catch (Exception ex)
            {   // writes error into log file
                Log.Error(ex, _className, "Mail(string host, int port)");
                throw ex;
            }

        }
        #endregion

        #region Shoot Mail

        /// <summary>
        /// shoot single mail 
        /// </summary>
        /// <param name="recipientName">To name</param>
        /// <param name="recipientMailAddress">To email address</param>
        /// <param name="senderName">From Name</param>
        /// <param name="senderMailAddress">From email address</param>
        /// <param name="mailSubject">Suject line</param>
        /// <param name="mailBody">mail body</param>
        /// <param name="priority">mail priority</param>
        /// <returns>Ture for sent successfully, False for sent fail</returns>
        public Boolean SendMail(string recipientName, string recipientMailAddress, string senderName,
            string senderMailAddress, string mailSubject, string mailBody, MailPriority priority)
        {

            try
            {
                // throw exception for no recipient found
                if (recipientMailAddress.Trim() == "")
                    throw new Exception("Recipient mail address cannot be empty");
                if (senderMailAddress.Trim() == "")
                    throw new Exception("Sender mail address cannot be empty");

                // set SMTP setting
                SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
                MailMessage mail = new MailMessage();

                // set mail headers 
                mail.From = new MailAddress(senderMailAddress, senderName);
                mail.To.Add(new MailAddress(recipientMailAddress, recipientName));
                mail.IsBodyHtml = true;

                // set mail content
                mail.Subject = mailSubject;
                mail.Body = mailBody;

                // set mail priority
                mail.Priority = priority;

                // check for authentication
                string IsMailUsingAuthentication = "";
                System.Net.NetworkCredential authentication;
                try
                {
                    // get authentication setting
                    IsMailUsingAuthentication = (ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"] != null) ?
                                    ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"].ToString() : "0";
                }
                catch (ConfigurationException exConfig)
                {
                    // log error for config read fail
                    Log.Error(exConfig, _className, "SendMail - ConfigurationException");
                }

                // passs credentials if authentication needed
                if (IsMailUsingAuthentication.Trim() == "1")
                {

                    string userid = "";
                    string pwd = "";
                    string domain = "";

                    try
                    {
                        // get authentication token for SMTP
                        userid = ConfigurationManager.AppSettings["TEST_userid"];
                        pwd = ConfigurationManager.AppSettings["TEST_pwd"];
                        domain = ConfigurationManager.AppSettings["TEST_domain"];
                    }
                    catch (ConfigurationException exConfig)
                    {
                        // log error for config read fail
                        Log.Error(exConfig, _className, "SendMail - ConfigurationException");
                    }

                    // create authentication object
                    authentication = new System.Net.NetworkCredential();

                    //check authentication token and assign to credentials
                    if (userid.Trim() != "" && pwd.Trim() != "")
                    {
                        authentication.UserName = userid;
                        authentication.Password = pwd;
                        if (domain.Trim() != "")
                        {
                            authentication.Domain = domain;
                        }
                        smtpClient.UseDefaultCredentials = false;
                        bool isMailUsingSSL = false;
                        try
                        {
                            // get enable ssl setting
                            isMailUsingSSL = (ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"] != null) ?
                                            Convert.ToBoolean(Convert.ToInt16(ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"].ToString())) : false;
                        }
                        catch (ConfigurationException exConfig)
                        {
                            // log error for config read fail
                            Log.Error(exConfig, _className, "SendMail - ConfigurationException");
                        }
                        smtpClient.EnableSsl = isMailUsingSSL;
                        smtpClient.Credentials = authentication;
                    }
                }

                // shoots mail
                smtpClient.Send(mail);
                isReturn = true;
            }
            catch (Exception ex)
            {
                isReturn = false;
                Log.Error(ex, _className, "SendMail");
            }
            return isReturn;
        }

        public Boolean SendMailWithCCBCC(string recipientName, string recipientMailAddress, string ccMailAddress, string bccMailAddress, string senderName,
           string senderMailAddress, string mailSubject, string mailBody, MailPriority priority)
        {

            try
            {
                // throw exception for no recipient found
                if (recipientMailAddress.Trim() == "")
                    throw new Exception("Recipient mail address cannot be empty");
                if (senderMailAddress.Trim() == "")
                    throw new Exception("Sender mail address cannot be empty");

                // set SMTP setting
                SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
                MailMessage mail = new MailMessage();

                // set mail headers 
                mail.From = new MailAddress(senderMailAddress, senderName);
                mail.To.Add(new MailAddress(recipientMailAddress, recipientName));

                if (!string.IsNullOrEmpty(ccMailAddress))
                    mail.CC.Add(new MailAddress(ccMailAddress));

                if (!string.IsNullOrEmpty(bccMailAddress))
                    mail.Bcc.Add(new MailAddress(bccMailAddress));

                mail.IsBodyHtml = true;

                // set mail content
                mail.Subject = mailSubject;
                mail.Body = mailBody;

                // set mail priority
                mail.Priority = priority;

                // check for authentication
                string IsMailUsingAuthentication = "";
                System.Net.NetworkCredential authentication;
                try
                {
                    // get authentication setting
                    IsMailUsingAuthentication = (ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"] != null) ?
                                    ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"].ToString() : "0";
                }
                catch (ConfigurationException exConfig)
                {
                    // log error for config read fail
                    Log.Error(exConfig, _className, "SendMailWithCCBCC - ConfigurationException");
                }

                // passs credentials if authentication needed
                if (IsMailUsingAuthentication.Trim() == "1")
                {

                    string userid = "";
                    string pwd = "";
                    string domain = "";

                    try
                    {
                        // get authentication token for SMTP
                        userid = ConfigurationManager.AppSettings["TEST_userid"];
                        pwd = ConfigurationManager.AppSettings["TEST_pwd"];
                        domain = ConfigurationManager.AppSettings["TEST_domain"];
                    }
                    catch (ConfigurationException exConfig)
                    {
                        // log error for config read fail
                        Log.Error(exConfig, _className, "SendMailWithCCBCC - ConfigurationException");
                    }

                    // create authentication object
                    authentication = new System.Net.NetworkCredential();

                    //check authentication token and assign to credentials
                    if (userid.Trim() != "" && pwd.Trim() != "")
                    {
                        authentication.UserName = userid;
                        authentication.Password = pwd;
                        if (domain.Trim() != "")
                        {
                            authentication.Domain = domain;
                        }
                        smtpClient.UseDefaultCredentials = false;
                        bool isMailUsingSSL = false;
                        try
                        {
                            // get enable ssl setting
                            isMailUsingSSL = (ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"] != null) ?
                                            Convert.ToBoolean(Convert.ToInt16(ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"].ToString())) : false;
                        }
                        catch (ConfigurationException exConfig)
                        {
                            // log error for config read fail
                            Log.Error(exConfig, _className, "SendMailWithCCBCC - ConfigurationException");
                        }
                        smtpClient.EnableSsl = isMailUsingSSL;
                        smtpClient.Credentials = authentication;
                    }
                }

                // shoots mail
                smtpClient.Send(mail);
                isReturn = true;
            }
            catch (Exception ex)
            {
                isReturn = false;
                Log.Error(ex, _className, "SendMailWithCCBCC");
            }
            return isReturn;
        }

        public Boolean SendMailToMany(List<string> recipientMailAddress, string senderName,
           string senderMailAddress, string mailSubject, string mailBody, MailPriority priority)
        {

            try
            {
                // throw exception for no recipient found
                if (recipientMailAddress.Count == 0)
                    throw new Exception("Recipient mail address cannot be empty");
                if (senderMailAddress.Trim() == "")
                    throw new Exception("Sender mail address cannot be empty");

                // set SMTP setting
                SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
                MailMessage mail = new MailMessage();

                // set mail headers 
                mail.From = new MailAddress(senderMailAddress, senderName);
                foreach (string mailAddress in recipientMailAddress)
                {
                    mail.To.Add(new MailAddress(mailAddress));
                }
                mail.IsBodyHtml = true;

                // set mail content
                mail.Subject = mailSubject;
                mail.Body = mailBody;

                // set mail priority
                mail.Priority = priority;

                // check for authentication
                string IsMailUsingAuthentication = "";
                System.Net.NetworkCredential authentication;
                try
                {
                    // get authentication setting
                    IsMailUsingAuthentication = (ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"] != null) ?
                                    ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"].ToString() : "0";
                }
                catch (ConfigurationException exConfig)
                {
                    // log error for config read fail
                    Log.Error(exConfig, _className, "SendMailToMany - ConfigurationException");
                }

                // passs credentials if authentication needed
                if (IsMailUsingAuthentication.Trim() == "1")
                {

                    string userid = "";
                    string pwd = "";
                    string domain = "";

                    try
                    {
                        // get authentication token for SMTP
                        userid = ConfigurationManager.AppSettings["TEST_userid"];
                        pwd = ConfigurationManager.AppSettings["TEST_pwd"];
                        domain = ConfigurationManager.AppSettings["TEST_domain"];
                    }
                    catch (ConfigurationException exConfig)
                    {
                        // log error for config read fail
                        Log.Error(exConfig, _className, "SendMailToMany - ConfigurationException");
                    }

                    // create authentication object
                    authentication = new System.Net.NetworkCredential();

                    //check authentication token and assign to credentials
                    if (userid.Trim() != "" && pwd.Trim() != "")
                    {
                        authentication.UserName = userid;
                        authentication.Password = pwd;
                        if (domain.Trim() != "")
                        {
                            authentication.Domain = domain;
                        }
                        smtpClient.UseDefaultCredentials = false;
                        bool isMailUsingSSL = false;
                        try
                        {
                            // get enable ssl setting
                            isMailUsingSSL = (ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"] != null) ?
                                            Convert.ToBoolean(Convert.ToInt16(ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"].ToString())) : false;
                        }
                        catch (ConfigurationException exConfig)
                        {
                            // log error for config read fail
                            Log.Error(exConfig, _className, "SendMailToMany - ConfigurationException");
                        }
                        smtpClient.EnableSsl = isMailUsingSSL;
                        smtpClient.Credentials = authentication;
                    }
                }

                // shoots mail
                smtpClient.Send(mail);
                isReturn = true;
            }
            catch (Exception ex)
            {
                isReturn = false;
                Log.Error(ex, _className, "SendMailToMany");
            }
            return isReturn;
        }

        public Boolean SendMailWithCCBCCToMany(List<string> recipientMailAddress, List<string> ccMailAddress, List<string> bccMailAddress, string senderName,
          string senderMailAddress, string mailSubject, string mailBody, MailPriority priority)
        {

            try
            {
                // throw exception for no recipient found
                if (recipientMailAddress.Count == 0)
                    throw new Exception("Recipient mail address cannot be empty");
                if (senderMailAddress.Trim() == "")
                    throw new Exception("Sender mail address cannot be empty");

                // set SMTP setting
                SmtpClient smtpClient = new SmtpClient(smtpHost, smtpPort);
                MailMessage mail = new MailMessage();

                // set mail headers 
                mail.From = new MailAddress(senderMailAddress, senderName);
                foreach (string mailAddress in recipientMailAddress)
                {
                    mail.To.Add(new MailAddress(mailAddress));
                }
                if (ccMailAddress.Count != 0)
                {
                    foreach (string ccMailAddres in ccMailAddress)
                    {
                        mail.CC.Add(ccMailAddres);
                    }
                }
                if (bccMailAddress.Count != 0)
                {
                    foreach (string bccMailAddres in bccMailAddress)
                    {
                        mail.Bcc.Add(bccMailAddres);
                    }

                }
                mail.IsBodyHtml = true;

                // set mail content
                mail.Subject = mailSubject;
                mail.Body = mailBody;

                // set mail priority
                mail.Priority = priority;

                // check for authentication
                string IsMailUsingAuthentication = "";
                System.Net.NetworkCredential authentication;
                try
                {
                    // get authentication setting
                    IsMailUsingAuthentication = (ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"] != null) ?
                                    ConfigurationManager.AppSettings["TEST_IsMailUsingAuthentication"].ToString() : "0";
                }
                catch (ConfigurationException exConfig)
                {
                    // log error for config read fail
                    Log.Error(exConfig, _className, "SendMailWithCCBCCToMany - ConfigurationException");
                }

                // passs credentials if authentication needed
                if (IsMailUsingAuthentication.Trim() == "1")
                {

                    string userid = "";
                    string pwd = "";
                    string domain = "";

                    try
                    {
                        // get authentication token for SMTP
                        userid = ConfigurationManager.AppSettings["TEST_userid"];
                        pwd = ConfigurationManager.AppSettings["TEST_pwd"];
                        domain = ConfigurationManager.AppSettings["TEST_domain"];
                    }
                    catch (ConfigurationException exConfig)
                    {
                        // log error for config read fail
                        Log.Error(exConfig, _className, "SendMailWithCCBCCToMany - ConfigurationException");
                    }

                    // create authentication object
                    authentication = new System.Net.NetworkCredential();

                    //check authentication token and assign to credentials
                    if (userid.Trim() != "" && pwd.Trim() != "")
                    {
                        authentication.UserName = userid;
                        authentication.Password = pwd;
                        if (domain.Trim() != "")
                        {
                            authentication.Domain = domain;
                        }
                        smtpClient.UseDefaultCredentials = false;
                        bool isMailUsingSSL = false;
                        try
                        {
                            // get enable ssl setting
                            isMailUsingSSL = (ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"] != null) ?
                                            Convert.ToBoolean(Convert.ToInt16(ConfigurationManager.AppSettings["TEST_IsMailUsingSSL"].ToString())) : false;
                        }
                        catch (ConfigurationException exConfig)
                        {
                            // log error for config read fail
                            Log.Error(exConfig, _className, "SendMailWithCCBCCToMany - ConfigurationException");
                        }
                        smtpClient.EnableSsl = isMailUsingSSL;
                        smtpClient.Credentials = authentication;
                    }
                }

                // shoots mail
                smtpClient.Send(mail);
                isReturn = true;
            }
            catch (Exception ex)
            {
                isReturn = false;
                Log.Error(ex, _className, "SendMailWithCCBCCToMany");
            }
            return isReturn;
        }

        #endregion

    }
}
