namespace cleancoderscom.tests.doubles
{


	public class InMemoryUserGateway : GatewayUtilities<User>, UserGateway
	{
	  public virtual User findUserByName(string username)
	  {
		foreach (User user in Entities)
		{
		  if (user.UserName.Equals(username))
		  {
			return user;
		  }
		}
		return null;
	  }
	}

}