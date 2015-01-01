namespace cleancoderscom
{

	public class User : Entity
	{
	  private string userName;

	  public User(string userName)
	  {
		this.userName = userName;
	  }

	  public virtual string UserName
	  {
		  get
		  {
			return userName;
		  }
	  }
	}

}