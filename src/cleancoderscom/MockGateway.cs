using System.Collections.Generic;

namespace cleancoderscom
{


	public class MockGateway : Gateway
	{

	  private IList<Codecast> codecasts;
	  private IList<User> users;
	  private List<License> licenses;

	  public MockGateway()
	  {
		codecasts = new List<Codecast>();
		users = new List<User>();
		licenses = new List<License>();
	  }

	  public virtual IList<Codecast> findAllCodecastsSortedChronologically()
	  {
		List<Codecast> sortedCodecasts = new List<Codecast>(codecasts);
		sortedCodecasts.Sort(new ComparatorAnonymousInnerClassHelper(this));
		return sortedCodecasts;
	  }
	  private class ComparatorAnonymousInnerClassHelper : IComparer<Codecast>
	  {
		  private readonly MockGateway outerInstance;
		  public ComparatorAnonymousInnerClassHelper(MockGateway outerInstance)
		  {
			  this.outerInstance = outerInstance;
		  }
		  public virtual int Compare(Codecast o1, Codecast o2)
		  {
			return o1.PublicationDate.CompareTo(o2.PublicationDate);
		  }
	  }

	  public virtual void delete(Codecast codecast)
	  {
		codecasts.Remove(codecast);
	  }

	  public virtual Codecast save(Codecast codecast)
	  {
		codecasts.Add((Codecast)establishId(codecast));
		return codecast;
	  }

	  public virtual User save(User user)
	  {
		users.Add((User)establishId(user));
		return user;
	  }

	  private Entity establishId(Entity entity)
	  {
		if (entity.Id == null)
		{
		  entity.Id =System.Guid.NewGuid().ToString();
		}
		return entity;
	  }

	  public virtual void save(License license)
	  {
		licenses.Add(license);
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

	  public virtual Codecast findCodecastByTitle(string codecastTitle)
	  {
		foreach (Codecast codecast in codecasts)
		{
		  if (codecast.Title.Equals(codecastTitle))
		  {
			return codecast;
		  }
		}
		return null;
	  }

	  public virtual IList<License> findLicensesForUserAndCodecast(User user, Codecast codecast)
	  {
		IList<License> results = new List<License>();
		foreach (License license in licenses)
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