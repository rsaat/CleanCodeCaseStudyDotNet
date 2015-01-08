using System.Collections.Generic;
using NUnit.Framework;
using cleancoderscom;

namespace cleancoderscom.tests.presentation
{

    using LT = License.LicenseType;
    using System;
    /// <summary>
    ///   PresentCodecastUseCaseTest parent tests.
    /// </summary>
    public class PresentCodecastUseCaseTest
    {

        private  User user;
        private  PresentCodecastUseCase useCase;

        [SetUp]
        public virtual void setUp()
        {
            TestSetup.setupContext();
            user = Context.userGateway.save(new User("User"));
            useCase = new PresentCodecastUseCase();
        }

            

        #region Given No Codecasts tests 



        public class GivenNoCodecasts : PresentCodecastUseCaseTest
            {

            [Test]
            public virtual void noneArePresented()
            {
                IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);

                Assert.AreEqual(0, presentableCodecasts.Count);
            }
        } 

        #endregion

        #region Given One Codecast Tests 




        public class GivenOneCodecastContext : PresentCodecastUseCaseTest
            {

            internal Codecast codecast;

            [SetUp]
            public virtual void setupCodecast()
            {
                codecast = Context.codecastGateway.save(new Codecast());

            }

            public class GivenOneCodecast : GivenOneCodecastContext
            {
            [Test]
            public virtual void oneIsPresented()
            {
                codecast.Title = "Some Title";
                DateTime now = (new DateTime(2014, 5, 19));
                codecast.PublicationDate = now;
                Context.codecastGateway.save(codecast);
                    IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
                Assert.AreEqual(1, presentableCodecasts.Count);
                PresentableCodecast presentableCodecast = presentableCodecasts[0];
                Assert.AreEqual("Some Title", presentableCodecast.title);
                Assert.AreEqual("5/19/2014", presentableCodecast.publicationDate.ToString());
            }

            }


            public class GivenNoLicenses : GivenOneCodecastContext
                {
              
                [Test]
                public virtual void userCannotViewCodecast()
                {
                    Assert.IsFalse(useCase.isLicensedFor(LT.VIEWING, user, codecast));
                }
                [Test]
                public virtual void presentedCodecastShowsNotViewable()
                {
                    IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsFalse(presentableCodecast.isViewable);
                }
            }

            public class GivenOneViewingLicenseForTheUser : GivenOneCodecastContext
            {
                


                internal License viewLicense;
                [SetUp]
                public virtual void setupLicense()
                {
                    viewLicense = new License(LT.VIEWING, user, codecast);
                    Context.licenseGateway.save(viewLicense);
                }
                [Test]
                public virtual void userCanViewCodecast()
                {
                    Assert.IsTrue(useCase.isLicensedFor(LT.VIEWING, user, codecast));
                }
                [Test]
                public virtual void unlicensedUserCannotViewOtherUsersCodecast()
                {
                    User otherUser = new User("otherUser");
                    Context.userGateway.save(otherUser);
                    Assert.IsFalse(useCase.isLicensedFor(LT.VIEWING, otherUser, codecast));
                }
                [Test]
                public virtual void presentedCodecastIsViewable()
                {
                    Context.licenseGateway.save(new License(LT.VIEWING, user, codecast));
                    IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsTrue(presentableCodecast.isViewable);
                }
            }

            public class GivenOneDownloadLicenseForTheUser : GivenOneCodecastContext
            {
            


                internal License downloadLicense;
                [SetUp]
                public virtual void setupDownloadLicense()
                {
                    downloadLicense = new License(LT.DOWNLOADING, user, codecast);
                    Context.licenseGateway.save(downloadLicense);
                }
                [Test]
                public virtual void presentedCodecastIsDownloadable()
                {
                    IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsTrue(presentableCodecast.isDownloadable);
                    Assert.IsFalse(presentableCodecast.isViewable);
                }
            }
        } 

        #endregion
    }

}
