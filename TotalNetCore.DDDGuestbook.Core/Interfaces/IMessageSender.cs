using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDGuestbook.Core.Interfaces
{
    public interface IMessageSender
    {
        void SendGuestbookNotificationEmail(string toAddress, string messageBody);
    }
}
