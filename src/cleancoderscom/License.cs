namespace cleancoderscom
{

	public class License : Entity
	{
	  private User user;
	  private Codecast codecast;

	  public License(User user, Codecast codecast)
	  {

		this.user = user;
		this.codecast = codecast;
	  }

	  public virtual User User
	  {
		  get
		  {
			return user;
		  }
	  }

	  public virtual Codecast Codecast
	  {
		  get
		  {
			return codecast;
		  }
	  }
	}

}