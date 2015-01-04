namespace cleancoderscom.tests
{

	using InMemoryCodecastGateway = cleancoderscom.tests.doubles.InMemoryCodecastGateway;
	using InMemoryLicenseGateway = cleancoderscom.tests.doubles.InMemoryLicenseGateway;
	using InMemoryUserGateway = cleancoderscom.tests.doubles.InMemoryUserGateway;

	public class TestSetup
	{
	  public static void setupContext()
	  {
		Context.userGateway = new InMemoryUserGateway();
		Context.licenseGateway = new InMemoryLicenseGateway();
		Context.codecastGateway = new InMemoryCodecastGateway();
		Context.gateKeeper = new GateKeeper();
	  }
	}

}