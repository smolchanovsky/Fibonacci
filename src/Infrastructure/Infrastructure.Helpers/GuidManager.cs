using System;

namespace Infrastructure.Helpers
{
	public interface IGuidManager
	{
		Guid GetNew();
	}
	
	public class GuidManager : IGuidManager
	{
		public Guid GetNew()
		{
			return Guid.NewGuid();
		}
	}
}