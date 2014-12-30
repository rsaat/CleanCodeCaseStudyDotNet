using System.Collections.Generic;

namespace cleancoderscom
{


	public class PresentCodecastUseCase
	{
	  public virtual IList<PresentableCodecast> presentCodecasts(User loggedInUser)
	  {
		return new List<PresentableCodecast>();
	  }
	}

}