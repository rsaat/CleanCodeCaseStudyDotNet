using System.Collections.Generic;
using cleancoderscom;
using LT = cleancoderscom.License.LicenseType;

namespace cleancoderscom.tests.fixtures
{

    public class CodecastPresentation
    {
        private PresentCodecastUseCase useCase = new PresentCodecastUseCase();

        public CodecastPresentation()
        {
            TestSetup.setupContext();

        }

        public virtual bool addUser(string username)
        {
            Context.userGateway.save(new User(username));
            return true;
        }

        public virtual bool loginUser(string username)
        {
            User user = Context.userGateway.findUserByName(username);
            if (user != null)
            {
                Context.gateKeeper.LoggedInUser = user;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual bool createLicenseForViewing(string username, string codecastTitle)
        {
            User user = Context.userGateway.findUserByName(username);
            Codecast codecast = Context.codecastGateway.findCodecastByTitle(codecastTitle);
            License license = new License(LT.VIEWING, user, codecast);
            Context.licenseGateway.save(license);
            return useCase.isLicensedFor(LT.VIEWING, user, codecast);
        }
        public virtual bool createLicenseForDownloading(string username, string codecastTitle)
        {
            User user = Context.userGateway.findUserByName(username);
            Codecast codecast = Context.codecastGateway.findCodecastByTitle(codecastTitle);
            License license = new License(LT.DOWNLOADING, user, codecast);
            Context.licenseGateway.save(license);
            return useCase.isLicensedFor(LT.DOWNLOADING, user, codecast);
        }

        public virtual string presentationUser()
        {
            return Context.gateKeeper.LoggedInUser.UserName;
        }

        public virtual bool clearCodecasts()
        {
            IList<Codecast> codecasts = Context.codecastGateway.findAllCodecastsSortedChronologically();
            foreach (Codecast codecast in new List<Codecast>(codecasts))
            {
                Context.codecastGateway.delete(codecast);
            }
            return Context.codecastGateway.findAllCodecastsSortedChronologically().Count == 0;
        }

        public virtual int countOfCodecastsPresented()
        {
            IList<PresentableCodecast> presentations = useCase.presentCodecasts(Context.gateKeeper.LoggedInUser);
            return presentations.Count;
        }
    }

}