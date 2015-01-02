using System.Collections.Generic;

namespace cleancoderscom
{


    using LT = License.LicenseType;

	public class PresentCodecastUseCase
	{
	 
	  public virtual IList<PresentableCodecast> presentCodecasts(User loggedInUser)
	  {
		List<PresentableCodecast> presentableCodecasts = new List<PresentableCodecast>();
		IList<Codecast> allCodecasts = Context.gateway.findAllCodecastsSortedChronologically();
		foreach (Codecast codecast in allCodecasts)
		{
		  presentableCodecasts.Add(formatCodecast(loggedInUser, codecast));
		}

		return presentableCodecasts;
	  }
	  private PresentableCodecast formatCodecast(User loggedInUser, Codecast codecast)
	  {
		  PresentableCodecast cc = new PresentableCodecast();
		  cc.title = codecast.Title;
		cc.publicationDate = codecast.PublicationDate.ToString("M/dd/yyyy");
		cc.isViewable = isLicensedFor(LT.VIEWING, loggedInUser, codecast);
		cc.isDownloadable = isLicensedFor(LT.DOWNLOADING, loggedInUser, codecast);
		return cc;
	  }

	  public virtual bool isLicensedFor(License.LicenseType licenseType, User user, Codecast codecast)
	  {
		IList<License> licenses = Context.gateway.findLicensesForUserAndCodecast(user, codecast);
		foreach (License l in licenses)
		{
		  if (l.Type == licenseType)
		  {
			return true;
		  }
		}
		return false;
	  }
	}

}