using System;
using System.Collections.Generic;
using System.Text;

namespace TotalNetCore.DDDEventPattern
{
    public class User : Entity
    {
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime UpdateAt { get; protected set; }

        public Guid Id { get; protected set; }

        protected User() { }

        public User(string email)
        {
            SetEmail(email);
        }

        public void SetEmail(string email)
        {
            if(string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be empty", nameof(email));
            }

            if(Email.Equals(email))
            {
                return;
            }

            Email = email;
            UpdateAt = DateTime.UtcNow;
            AddEvent(new UserEmailChanged(Id));
        }
    }
}
