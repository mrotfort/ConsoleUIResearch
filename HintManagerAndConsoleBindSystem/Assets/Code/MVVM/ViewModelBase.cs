using System;

namespace MVVM
{
	public interface IViewModel : IBaseDisposable
	{
	}
	
	public class ViewModelBase : IViewModel
	{
		public void Dispose()
		{
			OnDispose?.Invoke();
		}

		public event Action OnDispose;
	}
}