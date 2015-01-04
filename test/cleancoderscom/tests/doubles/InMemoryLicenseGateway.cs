using System.Collections.Generic;

namespace cleancoderscom.tests.doubles
{

	using cleancoderscom;

	public class InMemoryLicenseGateway : GatewayUtilities<License>, LicenseGateway
	{
	  public virtual IList<License> findLicensesForUserAndCodecast(User user, Codecast codecast)
	  {
		IList<License> results = new List<License>();
		foreach (License license in Entities)
		{
		  if (license.User.isSame(user) && license.Codecast.isSame(codecast))
		  {
			results.Add(license);
		  }
		}
		return results;
	  }
	}

}