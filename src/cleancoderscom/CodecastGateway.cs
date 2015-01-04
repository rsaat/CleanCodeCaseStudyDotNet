using System.Collections.Generic;

namespace cleancoderscom
{

	public interface CodecastGateway
	{
	  IList<Codecast> findAllCodecastsSortedChronologically();

	  void delete(Codecast codecast);

	  Codecast save(Codecast codecast);

	  Codecast findCodecastByTitle(string codecastTitle);
	}

}