using System.Collections.Generic;
using cleancoderscom;

namespace cleancoderscom.fixtures
{

	public class CodecastPresentation
	{
	  private PresentCodecastUseCase useCase = new PresentCodecastUseCase();
	  private GateKeeper gateKeeper = new GateKeeper();

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

	  public virtual bool createLicenseForViewing(string user, string codecast)
	  {
		return false;
	  }

	  public virtual string presentationUser()
	  {
		return gateKeeper.LoggedInUser.UserName;
	  }

	  public virtual bool clearCodecasts()
	  {
		IList<Codecast> codecasts = Context.gateway.findAllCodecasts();
		foreach (Codecast codecast in new List<Codecast>(codecasts))
		{
		  Context.gateway.delete(codecast);
		}
		return Context.gateway.findAllCodecasts().Count == 0;
	  }

	  public virtual int countOfCodecastsPresented()
	  {
		IList<PresentableCodecast> presentations = useCase.presentCodecasts(gateKeeper.LoggedInUser);
		return presentations.Count;
	  }
	}

}