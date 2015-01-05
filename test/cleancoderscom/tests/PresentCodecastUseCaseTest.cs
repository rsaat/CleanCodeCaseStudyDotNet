using System.Collections.Generic;
using NUnit.Framework;
using cleancoderscom;

namespace cleancoderscom.tests.presentation
{

    using LT = License.LicenseType;
    using System;
    /// <summary>
    ///   PresentCodecastUseCaseTest parent tests.
    ///   TODO:Remove nested classes tests or find a better implementation with C#. 
    ///   This code requires that outer class tests run before nested class tests.  
    ///   See C# nested classes are like C++ nested classes, not Java inner classes
    ///   http://blogs.msdn.com/b/oldnewthing/archive/2006/08/01/685248.aspx
    /// </summary>
    public class PresentCodecastUseCaseTest
    {

        private  User user;
        private  PresentCodecastUseCase useCase;
        private static PresentCodecastUseCaseTest PresentCodecastUseCaseTestInstance;

        [SetUp]
        public virtual void setUp()
        {
            TestSetup.setupContext();
            user = Context.userGateway.save(new User("User"));
            useCase = new PresentCodecastUseCase();
            PresentCodecastUseCaseTestInstance = this;
        }

        [Test]
        public virtual void __ThisTestMustBeExecutedBeforeInnerClassTests()
        {
            
        }

        #region Given No Codecasts tests 

        public class GivenNoCodecasts
        {
            private readonly PresentCodecastUseCaseTest outerInstance;

            public GivenNoCodecasts()
                : this(PresentCodecastUseCaseTestInstance) { }

            public GivenNoCodecasts(PresentCodecastUseCaseTest outerInstance)
            {
                this.outerInstance = outerInstance;
            }

            [Test]
            public virtual void noneArePresented()
            {
                IList<PresentableCodecast> presentableCodecasts = outerInstance.useCase.presentCodecasts(outerInstance.user);

                Assert.AreEqual(0, presentableCodecasts.Count);
            }
        } 

        #endregion

        #region Given One Codecast Tests 

        public class GivenOneCodecast
        {
            private readonly PresentCodecastUseCaseTest outerInstance;

            private static GivenOneCodecast GivenOneCodecastInstance;

            public GivenOneCodecast()
                : this(PresentCodecastUseCaseTestInstance) { }

            public GivenOneCodecast(PresentCodecastUseCaseTest outerInstance)
            {
                this.outerInstance = outerInstance;
                GivenOneCodecastInstance = this;
            }

            internal Codecast codecast;

            [SetUp]
            public virtual void setupCodecast()
            {
                codecast = Context.codecastGateway.save(new Codecast());

            }

            [Test]
            public virtual void oneIsPresented()
            {
                codecast.Title = "Some Title";
                DateTime now = (new DateTime(2014, 5, 19));
                codecast.PublicationDate = now;
                Context.codecastGateway.save(codecast);
                IList<PresentableCodecast> presentableCodecasts = outerInstance.useCase.presentCodecasts(outerInstance.user);
                Assert.AreEqual(1, presentableCodecasts.Count);
                PresentableCodecast presentableCodecast = presentableCodecasts[0];
                Assert.AreEqual("Some Title", presentableCodecast.title);
                Assert.AreEqual("5/19/2014", presentableCodecast.publicationDate.ToString());
            }

            public class GivenNoLicenses
            {
                private readonly PresentCodecastUseCaseTest.GivenOneCodecast outerInstance;

                public GivenNoLicenses()
                    : this(GivenOneCodecastInstance) { }

                public GivenNoLicenses(PresentCodecastUseCaseTest.GivenOneCodecast outerInstance)
                {
                    this.outerInstance = outerInstance;
                }
                [Test]
                public virtual void userCannotViewCodecast()
                {
                    Assert.IsFalse(outerInstance.outerInstance.useCase.isLicensedFor(LT.VIEWING, outerInstance.outerInstance.user, outerInstance.codecast));
                }
                [Test]
                public virtual void presentedCodecastShowsNotViewable()
                {
                    IList<PresentableCodecast> presentableCodecasts = outerInstance.outerInstance.useCase.presentCodecasts(outerInstance.outerInstance.user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsFalse(presentableCodecast.isViewable);
                }
            }

            public class GivenOneViewingLicenseForTheUser
            {
                private readonly PresentCodecastUseCaseTest.GivenOneCodecast outerInstance;

                public GivenOneViewingLicenseForTheUser()
                    : this(GivenOneCodecastInstance) { }

                public GivenOneViewingLicenseForTheUser(PresentCodecastUseCaseTest.GivenOneCodecast outerInstance)
                {
                    this.outerInstance = outerInstance;
                }
                internal License viewLicense;
                [SetUp]
                public virtual void setupLicense()
                {
                    viewLicense = new License(LT.VIEWING, outerInstance.outerInstance.user, outerInstance.codecast);
                    Context.licenseGateway.save(viewLicense);
                }
                [Test]
                public virtual void userCanViewCodecast()
                {
                    Assert.IsTrue(outerInstance.outerInstance.useCase.isLicensedFor(LT.VIEWING, outerInstance.outerInstance.user, outerInstance.codecast));
                }
                [Test]
                public virtual void unlicensedUserCannotViewOtherUsersCodecast()
                {
                    User otherUser = new User("otherUser");
                    Context.userGateway.save(otherUser);
                    Assert.IsFalse(outerInstance.outerInstance.useCase.isLicensedFor(LT.VIEWING, otherUser, outerInstance.codecast));
                }
                [Test]
                public virtual void presentedCodecastIsViewable()
                {
                    Context.licenseGateway.save(new License(LT.VIEWING, outerInstance.outerInstance.user, outerInstance.codecast));
                    IList<PresentableCodecast> presentableCodecasts = outerInstance.outerInstance.useCase.presentCodecasts(outerInstance.outerInstance.user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsTrue(presentableCodecast.isViewable);
                }
            }

            public class GivenOneDownloadLicenseForTheUser
            {
                private readonly PresentCodecastUseCaseTest.GivenOneCodecast outerInstance;

                public GivenOneDownloadLicenseForTheUser()
                    : this(GivenOneCodecastInstance) { }

                public GivenOneDownloadLicenseForTheUser(PresentCodecastUseCaseTest.GivenOneCodecast outerInstance)
                {
                    this.outerInstance = outerInstance;
                }
                internal License downloadLicense;
                [SetUp]
                public virtual void setupDownloadLicense()
                {
                    downloadLicense = new License(LT.DOWNLOADING, outerInstance.outerInstance.user, outerInstance.codecast);
                    Context.licenseGateway.save(downloadLicense);
                }
                [Test]
                public virtual void presentedCodecastIsDownloadable()
                {
                    IList<PresentableCodecast> presentableCodecasts = outerInstance.outerInstance.useCase.presentCodecasts(outerInstance.outerInstance.user);
                    PresentableCodecast presentableCodecast = presentableCodecasts[0];
                    Assert.IsTrue(presentableCodecast.isDownloadable);
                    Assert.IsFalse(presentableCodecast.isViewable);
                }
            }
        } 

        #endregion
    }

}
