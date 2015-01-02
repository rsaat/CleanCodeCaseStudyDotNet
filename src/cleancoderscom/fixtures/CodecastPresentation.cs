using System.Collections.Generic;

namespace cleancoderscom.fixtures
{

	using cleancoderscom;
    using LT=License.LicenseType;

	public class CodecastPresentation
	{
	  private PresentCodecastUseCase useCase = new PresentCodecastUseCase();
	  public static GateKeeper gateKeeper = new GateKeeper();

	  public CodecastPresentation()
	  {
       
          Context.gateway = new MockGateway();
	      
	  }

	  public virtual bool addUser(string username)
	  {
		Context.gateway.save(new User(username));
		return true;
	  }

	  public virtual bool loginUser(string username)
	  {
		User user = Context.gateway.findUser(username);
		if (user != null)
		{
		  gateKeeper.LoggedInUser = user;
		  return true;
		}
		else
		{
		  return false;
		}
	  }

	  public virtual bool createLicenseForViewing(string username, string codecastTitle)
	  {
		User user = Context.gateway.findUser(username);
		Codecast codecast = Context.gateway.findCodecastByTitle(codecastTitle);
        License license = new License(LT.VIEWING, user, codecast);
		Context.gateway.save(license);
        return useCase.isLicensedFor(LT.VIEWING, user, codecast);
	  }
	  public virtual bool createLicenseForDownloading(string username, string codecastTitle)
	  {
		User user = Context.gateway.findUser(username);
		Codecast codecast = Context.gateway.findCodecastByTitle(codecastTitle);
		License license = new License(LT.DOWNLOADING, user, codecast);
		Context.gateway.save(license);
		return useCase.isLicensedFor(LT.DOWNLOADING, user, codecast);
	  }

	  public virtual string presentationUser()
	  {
		return gateKeeper.LoggedInUser.UserName;
	  }

	  public virtual bool clearCodecasts()
	  {
		IList<Codecast> codecasts = Context.gateway.findAllCodecastsSortedChronologically();
		foreach (Codecast codecast in new List<Codecast>(codecasts))
		{
		  Context.gateway.delete(codecast);
		}
		return Context.gateway.findAllCodecastsSortedChronologically().Count == 0;
	  }

	  public virtual int countOfCodecastsPresented()
	  {
		IList<PresentableCodecast> presentations = useCase.presentCodecasts(gateKeeper.LoggedInUser);
		return presentations.Count;
	  }
	}

}