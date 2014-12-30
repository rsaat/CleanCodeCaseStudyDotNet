using System.Collections.Generic;

namespace cleancoderscom
{


	public class MockGateway : Gateway
	{

	  private IList<Codecast> codecasts;
	  private IList<User> users;

	  public MockGateway()
	  {
		codecasts = new List<Codecast>();
		users = new List<User>();
	  }

	  public virtual IList<Codecast> findAllCodecasts()
	  {
		return codecasts;
	  }

	  public virtual void delete(Codecast codecast)
	  {
		codecasts.Remove(codecast);
	  }

	  public virtual void save(Codecast codecast)
	  {
		codecasts.Add(codecast);
	  }

	  public virtual void save(User user)
	  {
		users.Add(user);
	  }

	  public virtual User findUser(string username)
	  {
		foreach (User user in users)
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