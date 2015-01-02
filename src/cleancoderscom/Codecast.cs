using System;
namespace cleancoderscom
{

	public class Codecast : Entity
	{
	  private string title;
	  private DateTime publicationDate = DateTime.Now;

	  public virtual string Title
	  {
		  set
		  {
			this.title = value;
		  }
		  get
		  {
			return title;
		  }
	  }

	  public virtual DateTime PublicationDate
	  {
		  set
		  {
			this.publicationDate = value;
		  }
		  get
		  {
			return publicationDate;
		  }
	  }


	}

}