namespace cleancoderscom
{

	public class Codecast : Entity
	{
	  private string title;
	  private string publicationDate;

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

	  public virtual string PublicationDate
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