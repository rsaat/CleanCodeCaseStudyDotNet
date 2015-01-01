using System.Collections.Generic;

namespace cleancoderscom
{


	public class PresentCodecastUseCase
	{
	  public virtual IList<PresentableCodecast> presentCodecasts(User loggedInUser)
	  {
		List<PresentableCodecast> presentableCodecasts = new List<PresentableCodecast>();
		IList<Codecast> allCodecasts = Context.gateway.findAllCodecasts();
		foreach (Codecast codecast in allCodecasts)
		{

		  PresentableCodecast cc = new PresentableCodecast();
		  cc.title = codecast.Title;
		  cc.publicationDate = codecast.PublicationDate;
		  cc.isViewable = isLicensedToViewCodecast(loggedInUser, codecast);
		  presentableCodecasts.Add(cc);
		}
		return presentableCodecasts;
	  }

	  public virtual bool isLicensedToViewCodecast(User user, Codecast codecast)
	  {
		IList<License> licenses = Context.gateway.findLicensesForUserAndCodecast(user, codecast);
		return licenses.Count > 0;
	  }
	}

}