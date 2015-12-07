using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using log4net;
using S22.Imap;

namespace TestomaticSupport
{
    public class EmailHelper
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string IncomingServer { get; set; }
        public string OutboundServer { get; set; }
        private readonly ILog logger = LogManager.GetLogger("root");
        private const long threadSleepTimeInMillis = 500;

        public string DefaultIncomingImapServer
        {
            get { return "devmail.msrbtest.org"; }
        }

        public string DefaultOutboundSmtpServer
        {
            get { return "192.168.30.11"; }
        }

        public EmailHelper(string _user = "", string _pass = "", string _incomingServer = null,
            string _outgoingServer = null)
        {
            IncomingServer = _incomingServer ?? DefaultIncomingImapServer;
            OutboundServer = _outgoingServer ?? DefaultOutboundSmtpServer;
            User = _user;
            Password = _pass;
        }

        public bool CanConnectToIncoming()
        {
            try
            {
                using (new ImapClient(IncomingServer, 993,
                    User, Password, AuthMethod.Login, true))
                {
                    logger.DebugFormat("EmailHelper: Connection to email server '{0}' succeeded.", IncomingServer);
                    return true;
                }
            }
            catch (Exception)
            {
                logger.WarnFormat("EmailHelper: Unable to connect to email server '{0}'.", IncomingServer);
            }
            return false;
        }

        public Dictionary<uint, MailMessage> AllMessages(long waitInMillis = 0)
        {
            return SearchMessages(SearchCondition.All(), waitInMillis);
        }

        public bool DeleteAllMessages()
        {
            return DeleteMessages(AllMessages());
        }

        public bool MessageExists(string subject = null, string body = null, int waitInMillis = 10000)
        {
            Dictionary<uint, MailMessage> result = null;
            if (subject != null)
            {
                result = SearchMessages(SearchCondition.Subject(subject));
                
            }
            if (body != null)
            {
                result = SearchMessages(SearchCondition.Body(body));
            }

            return result != null && result.Count > 0;
        }

        public Dictionary<uint, MailMessage> SearchMessages(SearchCondition search, long waitInMillis = 10000)
        {
            try
            {
                using (var Client = new ImapClient(IncomingServer, 993,
                    User, Password, AuthMethod.Login, true))
                {
                    var stopWatch = new Stopwatch();
                    stopWatch.Start();
                    var messageMap = new Dictionary<uint, MailMessage>();
                    bool lookAgain = false;

                    do
                    {
                        IEnumerable<uint> uids = Client.Search(search);

                        uids.ToList().ForEach(id => { messageMap[id] = Client.GetMessage(id); });

                        logger.DebugFormat("EmailHelper: SearchMessages read ({0}) messages.  Search condition(s): {1}.",
                            messageMap.Keys.Count, search);

                        lookAgain = (messageMap.Keys.Count == 0 &&
                                     stopWatch.ElapsedMilliseconds < waitInMillis);
                        
                        if (lookAgain)
                        {
                            Thread.Sleep(TimeSpan.FromMilliseconds(threadSleepTimeInMillis));
                        }
                    } while (lookAgain);

                    return messageMap;
                }
            }
            catch (Exception)
            {
                logger.WarnFormat("EmailHelper: Unable to connect to email server {0}.", IncomingServer);
            }
            return null;
        }

        public bool DeleteMessages(Dictionary<uint, MailMessage> messagesToDelete)
        {
            try
            {
                using (var Client = new ImapClient(IncomingServer, 993,
                    User, Password, AuthMethod.Login, true))
                {
                    messagesToDelete.Keys.ToList().ForEach(uid => Client.DeleteMessage(uid));
                    Client.Expunge();
                    logger.DebugFormat("EmailHelper: Removed ({0}) messages.", messagesToDelete.Keys.Count);
                    return true;
                }
            }
            catch (Exception)
            {
                logger.WarnFormat("EmailHelper: Unable to remove mail messages ({0}) from server.", messagesToDelete.Keys.Count);
            }
            return false;
        }

        public void SendEmail(MailAddress from, IEnumerable<string> to, IEnumerable<string> bcc, string subject,
            string body,
            Stream attachmentContentStream, string attachmentFileName, string attachmentMediaType)
        {
            try
            {
                using (var client = new SmtpClient(OutboundServer))
                {
                    using (var message = new MailMessage())
                    {
                        message.IsBodyHtml = true;
                        message.From = from;
                        foreach (string address in to)
                        {
                            message.To.Add(address);
                        }
                        message.Subject = subject;
                        message.Body = body;
                        foreach (string address in bcc)
                        {
                            message.Bcc.Add(address);
                        }
                        if (attachmentContentStream != null)
                        {
                            message.Attachments.Add(new Attachment(attachmentContentStream, attachmentFileName,
                                attachmentMediaType));
                        }
                        client.Send(message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.WarnFormat("EmailHelper: {0}", ex);
            }
        }

    }
}
