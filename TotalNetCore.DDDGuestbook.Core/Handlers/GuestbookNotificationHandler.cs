using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TotalNetCore.DDDGuestbook.Core.Events;
using TotalNetCore.DDDGuestbook.Core.Interfaces;
using TotalNetCore.DDDGuestbook.Core.Specifications;

namespace TotalNetCore.DDDGuestbook.Core.Handlers
{
    /// <summary>
    /// Guestbook的事件处理
    /// </summary>
    public class GuestbookNotificationHandler : IHandle<EntryAddedEvent>
    {
        private IRepository _repository;
        private IMessageSender _messageSender;

        public GuestbookNotificationHandler(IRepository repository, IMessageSender messageSender)
        {
            _repository = repository;
            _messageSender = messageSender;
        }
        public void Handle(EntryAddedEvent entryAddedEvent)
        {
            var notificationPolicy = new GuestbookNotificationPolicy(entryAddedEvent.Entry.Id);

            //Send updates to previous entries made in the last day
            var emailsToNotify = _repository.List(notificationPolicy).Select(e => e.EmailAddress);

            foreach (var emailAddress in emailsToNotify)
            {
                string messageBody = $"{entryAddedEvent.Entry.EmailAddress} left a message {entryAddedEvent.Entry.Message}";
                _messageSender.SendGuestbookNotificationEmail(emailAddress, messageBody);
            }
        }
    }
}
