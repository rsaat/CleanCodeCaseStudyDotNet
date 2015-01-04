using System;

namespace cleancoderscom
{

	public class Entity : ICloneable
	{
	  private string id;

	  public virtual bool isSame(Entity entity)
	  {
		return id != null && object.Equals(id, entity.id);
	  }

	  public virtual string Id
	  {
		  set
		  {
			this.id = value;
		  }
		  get
		  {
			return id;
		  }
	  }

	  public virtual object Clone()
	  {
		return base.MemberwiseClone();
	  }
	}

}