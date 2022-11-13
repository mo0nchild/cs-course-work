using ServiceLibrary.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.DataTransfer
{
    public class DataTransferController : System.Object, ServiceContracts.IProjectTransfer
    {
        protected INetworkTransfer<SmptMessageEnvelope> TransferCore { get; private set; } = default;

        public DataTransferController(INetworkTransfer<SmptMessageEnvelope> transfer_core)
        {
            this.TransferCore = transfer_core;
        }

        // (export_entity - путь к файлу, transfer_data - {эл_почта откуда + пароль, эл_почта куда})
        public void ExportProject(string export_entity, ServiceContracts.TransferData transfer_data)
        {
            var transfer_frominfo = transfer_data.FromPath.Split(';');
            if (transfer_frominfo.Length < 2) throw new Exception("Данные передачи установлены неверно");

            TransferCore.SendData(new SmptMessageEnvelope()
            {
                SendingEmail = transfer_frominfo[0], EmailCredentials = transfer_frominfo[1],
                ReceivingEmail = transfer_data.ToPath, AttachmentObject = export_entity
            },
            transfer_message: "Проект был экспортирован на вашу почту, используйте его в приложении");
        }

        public void ImportProject(string export_entity, ServiceContracts.TransferData transfer_data)
        {
            
        }
    }
}
