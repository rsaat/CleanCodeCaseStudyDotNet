using System.Collections.Generic;

namespace cleancoderscom.tests.fixtures
{



	//OrderedQuery
	public class OfCodeCasts
	{
	  private IList<object> list(params object[] objects)
	  {
		return new List<object>(objects);
	  }

	  public virtual IList<object> query()
	  {
		User loggedInUser = Context.gateKeeper.LoggedInUser;
		PresentCodecastUseCase useCase = new PresentCodecastUseCase();
		IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(loggedInUser);
		IList<object> queryResponse = new List<object>();
		foreach (PresentableCodecast pcc in presentableCodecasts)
		{
		  queryResponse.Add(makeRow(pcc));
		}
		return queryResponse;

	  }

	  private IList<object> makeRow(PresentableCodecast pc)
	  {
		return list(new object[]{list("title", pc.title), list("publication date", pc.publicationDate), list("picture", pc.title), list("description", pc.title), list("viewable", pc.isViewable ? "+" : "-"), list("downloadable", pc.isDownloadable ? "+" : "-")});
	  }

	}

}