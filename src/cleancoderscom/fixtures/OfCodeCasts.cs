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
		return list(list());
			  //list(name,value)
	  }
	}

}