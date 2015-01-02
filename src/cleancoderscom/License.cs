using System.Collections.Generic;
namespace cleancoderscom
{

    public class License : Entity
    {
        public enum LicenseType
        {
            DOWNLOADING,
            VIEWING
        };

        private LicenseType type;
        private User user;
        private Codecast codecast;
        public License(LicenseType type, User user, Codecast codecast)
        {
            this.type = type;
            this.user = user;
            this.codecast = codecast;
        }

        public virtual LicenseType Type
        {
            get
            {
                return type;
            }
        }

        public virtual User User
        {
            get
            {
                return user;
            }
        }

        public virtual Codecast Codecast
        {
            get
            {
                return codecast;
            }
        }
    }
}