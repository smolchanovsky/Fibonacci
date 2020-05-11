using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.BackgroundJob
{
	public interface IBackgroundJobDirector
	{
		string Enqueue(Expression<Func<Task>> methodCall);
	}
}