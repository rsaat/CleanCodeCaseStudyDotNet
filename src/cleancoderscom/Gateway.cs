using System.Collections.Generic;

namespace cleancoderscom
{

	public interface Gateway
	{
	  IList<Codecast> findAllCodecasts();

	  void delete(Codecast codecast);

	  void save(Codecast codecast);

	  void save(User user);

	  User findUser(string username);
	}

}