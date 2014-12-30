namespace cleancoderscom.fixtures
{


	public class GivenCodecasts
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

	  public virtual string Published
	  {
		  set
		  {
			this.publicationDate = value;
		  }
	  }

	  public virtual void execute()
	  {
		Codecast codecast = new Codecast();
		codecast.Title = title;
		codecast.PublicationDate = publicationDate;
		Context.gateway.save(codecast);
	  }

	}

}