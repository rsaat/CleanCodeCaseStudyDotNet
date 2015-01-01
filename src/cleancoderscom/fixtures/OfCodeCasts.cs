using System.Collections.Generic;

namespace cleancoderscom.fixtures
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
		User loggedInUser = CodecastPresentation.gateKeeper.LoggedInUser;
		PresentCodecastUseCase useCase = new PresentCodecastUseCase();
		IList<PresentableCodecast> presentableCodecasts = useCase.presentCodecasts(loggedInUser);
		IList<object> queryResponse = new List<object>();
		foreach (PresentableCodecast pcc in presentableCodecasts)
		{
		  queryResponse.Add(makeRow(pcc.title, pcc.title, pcc.title, pcc.isViewable, false));
		}
		return queryResponse;

	  }

	  private IList<object> makeRow(string title, string picture, string description, bool viewable, bool downloadable)
	  {
		return list(new object[]{list("title", title), list("picture", picture), list("description", description), list("viewable", viewable ? "+" : "-"), list("downloadable", downloadable ? "+" : "-")});
	  }

	}

}