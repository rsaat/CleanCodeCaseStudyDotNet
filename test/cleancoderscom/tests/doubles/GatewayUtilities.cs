using System;
using System.Collections.Generic;

namespace cleancoderscom.tests.doubles
{


	public class GatewayUtilities<T> where T : cleancoderscom.Entity
	{
	  private Dictionary<string, T> entities;

	  public GatewayUtilities()
	  {
		this.entities = new Dictionary<string, T>();
	  }

	  public virtual IList<T> Entities
	  {
		  get
		  {
			IList<T> clonedEntities = new List<T>();
			foreach (T entity in entities.Values)
			{
			  addCloneToList(entity, clonedEntities);
			}
			return clonedEntities;
		  }
	  }

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("unchecked") private void addCloneToList(T entity, java.util.List<T> newEntities)
	  private void addCloneToList(T entity, IList<T> newEntities)
	  {
		try
		{
		  newEntities.Add((T) entity.Clone());
		}
		catch (Exception)
		{
		  throw new UnCloneableEntity();
		}
	  }

	  public virtual T save(T entity)
	  {
		if (entity.Id == null)
		{
		  entity.Id = System.Guid.NewGuid().ToString();
		}
		string id = entity.Id;
		saveCloneInMap(id, entity);
		return entity;
	  }

	  private void saveCloneInMap(string id, T entity)
	  {
		try
		{
		  entities[id] = (T) entity.Clone();
		}
		catch (Exception)
		{
		  throw new UnCloneableEntity();
		}
	  }

	  public virtual void delete(T entity)
	  {
		entities.Remove(entity.Id);
	  }

	  private class UnCloneableEntity : Exception
	  {
	  }
	}

}