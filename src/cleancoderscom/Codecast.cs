namespace cleancoderscom
{

	public class Codecast
	{
	  private string title;
	  private string publicationDate;

	  public virtual string Title
	  {
		  set
		  {
			this.title = value;
		  }
	  }

	  public virtual string PublicationDate
	  {
		  set
		  {
			this.publicationDate = value;
		  }
	  }
	}

}