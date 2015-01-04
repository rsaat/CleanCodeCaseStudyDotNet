using System.Collections.Generic;

namespace cleancoderscom.tests.doubles
{

	using cleancoderscom;

	public class InMemoryCodecastGateway : GatewayUtilities<Codecast>, CodecastGateway
	{
	  public virtual IList<Codecast> findAllCodecastsSortedChronologically()
	  {
		List<Codecast> sortedCodecasts = new List<Codecast>(Entities);
		sortedCodecasts.Sort(new ComparatorAnonymousInnerClassHelper(this));
		return sortedCodecasts;
	  }

	  private class ComparatorAnonymousInnerClassHelper : IComparer<Codecast>
	  {
		  private readonly InMemoryCodecastGateway outerInstance;

		  public ComparatorAnonymousInnerClassHelper(InMemoryCodecastGateway outerInstance)
		  {
			  this.outerInstance = outerInstance;
		  }

		  public virtual int Compare(Codecast o1, Codecast o2)
		  {
			return o1.PublicationDate.CompareTo(o2.PublicationDate);
		  }
	  }

	  public virtual Codecast findCodecastByTitle(string codecastTitle)
	  {
		foreach (Codecast codecast in Entities)
		{
		  if (codecast.Title.Equals(codecastTitle))
		  {
			return codecast;
		  }
		}
		return null;
	  }
	}

}