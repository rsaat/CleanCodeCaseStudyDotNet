using System.Collections.Generic;
using NUnit.Framework;
using cleancoderscom;

namespace cleancoderscom
{

    using LT = License.LicenseType;
    using System;

    public class PresentCodecastUseCaseTest
    {

        private User user;
        private Codecast codecast;
        private PresentCodecastUseCase useCase;

        [SetUp]
        public virtual void setUp()
        {
            Context.gateway = new MockGateway();
            user = Context.gateway.save(new User("User"));
            codecast = Context.gateway.save(new Codecast());
            useCase = new PresentCodecastUseCase();
        }

        [Test]
        public virtual void userWithoutViewLicense_cannotViewCodecast()
        {
            Assert.IsFalse(useCase.isLicensedFor(LT.VIEWING, user, codecast));
        }

        [Test]
        public virtual void userWithViewLicense_canViewCodecast()
        {
            License viewLicense = new License(LT.VIEWING, user, codecast);
            Context.gateway.save(viewLicense);
            Assert.IsTrue(useCase.isLicensedFor(LT.VIEWING, user, codecast));
        }

        [Test]
        public virtual void userWithoutViewLicense_cannotViewOtherUsersCodecast()
        {
            User otherUser = Context.gateway.save(new User("otherUser"));

            License viewLicense = new License(LT.VIEWING, user, codecast);
            Context.gateway.save(viewLicense);
            Assert.IsFalse(useCase.isLicensedFor(LT.VIEWING, otherUser, codecast));
        }

        [Test]
        public virtual void presentingNoCodecasts()
        {
            Context.gateway.delete(codecast);
            IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);

            Assert.AreEqual(0, presentableCodecasts.Count);
        }

        [Test]
        public virtual void presentOneCodecast()
        {
            codecast.Title = "Some Title";
            DateTime now = (new DateTime(2014, 5, 19));
            codecast.PublicationDate = now;
            IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
            Assert.AreEqual(1, presentableCodecasts.Count);
            PresentableCodecast presentableCodecast = presentableCodecasts[0];
            Assert.AreEqual("Some Title", presentableCodecast.title);
            Assert.AreEqual("5/19/2014", presentableCodecast.publicationDate.ToString());
        }

        [Test]
        public virtual void presentedCodecastIsNotViewableIfNoLicense()
        {
            IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
            PresentableCodecast presentableCodecast = presentableCodecasts[0];
            Assert.IsFalse(presentableCodecast.isViewable);
        }

        [Test]
        public virtual void presentedCodecastIsViewableIfLicenseExists()
        {
            Context.gateway.save(new License(LT.VIEWING, user, codecast));
            IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
            PresentableCodecast presentableCodecast = presentableCodecasts[0];
            Assert.IsTrue(presentableCodecast.isViewable);
        }

        [Test]
        public virtual void prestedCodecastIsDownloadableIfDownloadLicenseExists()
        {
            License downloadLicense = new License(LT.DOWNLOADING, user, codecast);
            Context.gateway.save(downloadLicense);
            IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(user);
            PresentableCodecast presentableCodecast = presentableCodecasts[0];
            Assert.IsTrue(presentableCodecast.isDownloadable);
            Assert.IsFalse(presentableCodecast.isViewable);
        }

    }

}
