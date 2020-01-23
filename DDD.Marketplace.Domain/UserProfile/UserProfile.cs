using DDD.Marketplace.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Marketplace.Domain.UserProfile
{
    public class UserProfile : AggregateRoot<UserId>
    {
        public Guid UserProfileId { get; private set; }
        public FullName FullName { get; private set; }
        public DisplayName DisplayName { get; private set; }
        public string PhotoUrl { get; private set; }

        public UserProfile(UserId id, FullName fullName, DisplayName displayName)
        {
            Apply(new Events.UserRegistered { 
                UserId = id,
                FullName = fullName,
                DisplayName = displayName
            });
        }

        protected UserProfile() { }

        public void UpdateFullName(FullName fullName)
        {
            Apply(new Events.UserFullNameUpdated { UserId=Id, FullName=fullName});
        }

        public void UpdateDisplayName(DisplayName displayName)
        {
            Apply(new Events.UserDisplayNameUpdated { UserId=Id});
        }

        public void UpdateProfilePhoto(Uri photoUrl)
        {
            Apply(new Events.ProfilePhotoUploaded { UserId=Id, PhotoUrl=photoUrl.ToString()});
        }

        protected override void EnsureValiedState()
        {
            
        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.UserRegistered e:
                    Id = new UserId(e.UserId);
                    FullName = new FullName(e.FullName);
                    DisplayName = new DisplayName(e.DisplayName);
                    UserProfileId = e.UserId;
                    break;
                case Events.UserFullNameUpdated e:
                    FullName = new FullName(e.FullName);
                    break;
                case Events.UserDisplayNameUpdated e:
                    DisplayName = new DisplayName(e.DisplayName);
                    break;
                case Events.ProfilePhotoUploaded e:
                    PhotoUrl = e.PhotoUrl;
                    break;
            }
        }
    }
}
