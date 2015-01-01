using System.Collections.Generic;

namespace cleancoderscom
{

	public interface Gateway
	{
	  IList<Codecast> findAllCodecasts();

	  void delete(Codecast codecast);

	  Codecast save(Codecast codecast);

	  User save(User user);

	  void save(License license);

	  User findUser(string username);

	  Codecast findCodecastByTitle(string codecastTitle);

	  IList<License> findLicensesForUserAndCodecast(User user, Codecast codecast);
	}

}