namespace cleancoderscom
{

	public interface UserGateway
	{
	  User save(User user);

	  User findUserByName(string username);
	}

}