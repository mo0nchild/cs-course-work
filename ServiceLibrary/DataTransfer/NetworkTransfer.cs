using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;
using ServiceLibrary.ServiceLocatorTool;

namespace ServiceLibrary.DataTransfer
{
    public interface INetworkTransfer<TMessageEnvelope> : System.IDisposable, ServiceLocatorTool.IServiceContract
        where TMessageEnvelope : class
    {
        void SendData(TMessageEnvelope envelope, System.String transfer_message);
        System.Boolean CheckChannel(TMessageEnvelope envelope);
    }

    public sealed class SmptMessageEnvelope : System.Object
    {
        public System.String SendingEmail { get; set; } = default(string);
        public System.String EmailCredentials { get; set; } = default(string);

        public System.String ReceivingEmail { get; set; } = default(string);
        public System.String AttachmentObject { get; set; } = default(string);
    }

    public sealed class SmtpTransferFactory : ServiceLocatorTool.ServiceFactoryBase
    {
        public override ServiceLocatorTool.ServiceBase BuildService() => new SmtpTransfer();
    }

    public class SmtpTransfer : ServiceLocatorTool.ServiceBase, DataTransfer.INetworkTransfer<SmptMessageEnvelope>
    {
        protected virtual System.String DataTransferName { get; } = "NodeMapApp";
        protected virtual System.String CheckMessage { get; } = "Подтвердение почты";
        protected SmtpClient SmptTransferClient { get; private set; } = default;

        public System.Type FactoryType { get => typeof(SmtpTransferFactory); }

        public SmtpTransfer() : base(typeof(SmtpTransfer).Name) 
        { this.SmptTransferClient = new SmtpClient("smtp.gmail.com", 587) { EnableSsl = true}; }

        protected virtual MailMessage MessageBuild(string sendfrom, string sendto, string message)
        {
            return new MailMessage(new MailAddress(sendfrom, this.DataTransferName), new MailAddress(sendto)) 
            { Body = message, Subject = "NodeMap Transfer" };
        }

        public System.Boolean CheckChannel(SmptMessageEnvelope envelope)
        {
            using (var smtp_message = this.MessageBuild(envelope.SendingEmail, envelope.SendingEmail, this.CheckMessage))
            {
                this.SmptTransferClient.Credentials = new NetworkCredential(envelope.SendingEmail, envelope.EmailCredentials);
                try { this.SmptTransferClient.Send(smtp_message); } catch (System.Exception) { return false; } return true;
            }
        }

        public void SendData(SmptMessageEnvelope envelope, System.String transfer_message)
        {
            using (var smtp_message = this.MessageBuild(envelope.SendingEmail, envelope.ReceivingEmail, transfer_message))
            {
                smtp_message.Attachments.Add(new Attachment(envelope.AttachmentObject));

                this.SmptTransferClient.Credentials = new NetworkCredential(envelope.SendingEmail, envelope.EmailCredentials);
                this.SmptTransferClient.Send(smtp_message);
            }
        }

        public void Dispose() => this.SmptTransferClient.Dispose();
    }
}
