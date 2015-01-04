using System.Collections.Generic;

namespace cleancoderscom
{

	public interface LicenseGateway
	{
	  License save(License license);

	  IList<License> findLicensesForUserAndCodecast(User user, Codecast codecast);
	}

}