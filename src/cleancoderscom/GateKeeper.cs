namespace cleancoderscom
{

	public class GateKeeper
	{
	  private User loggedInUser;

	  public virtual User LoggedInUser
	  {
		  set
		  {
			this.loggedInUser = value;
		  }
		  get
		  {
			return loggedInUser;
		  }
	  }

	}

}