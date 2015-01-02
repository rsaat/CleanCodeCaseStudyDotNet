using System;
using System.Globalization;
namespace cleancoderscom.fixtures
{


	public class GivenCodecasts
	{
	  private string title;
	  private string publicationDate;
	  
	  public virtual string Title
	  {
		  set
		  {
			this.title = value;
		  }
	  }

	  public virtual string Published
	  {
		  set
		  {
			this.publicationDate = value;
		  }
	  }

	  public virtual void execute()
	  {
		Codecast codecast = new Codecast();
		codecast.Title = title;
		Console.WriteLine(publicationDate);
        Console.WriteLine(parseDate(publicationDate));
        codecast.PublicationDate = parseDate(publicationDate);
		Context.gateway.save(codecast);
	  }

	  DateTime parseDate(string dateToParse)
	  {
            var  cultureUS = new CultureInfo("en-US");
	        return DateTime.Parse(dateToParse, cultureUS);
	  }

	}

}