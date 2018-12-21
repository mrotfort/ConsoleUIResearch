using System;
using System.Collections.Generic;

namespace MVVM
{
	public interface IBaseDisposable : IDisposable
	{
		event Action OnDispose;
	}

	public abstract class BaseDisposable : IBaseDisposable
	{
		protected bool IsDisposed = false;

		private List<IDisposable> disposables = null;

		protected void AddDisposable(IDisposable disposable)
		{
			if (disposables == null)
				disposables = new List<IDisposable>();

			if ((disposable != null) &&
			    !disposables.Contains(disposable))
			{
				disposables.Add(disposable);
			}
		}

		protected bool RemoveDisposable(IDisposable disposable)
		{
			if (disposable != null)
				return disposables.Remove(disposable);

			return false;
		}

		private Action onDispose;

		public event Action OnDispose
		{
			add { onDispose += value; }
			remove { onDispose -= value; }
		}

		protected abstract void DisposeImpl();

		public void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.IsDisposed = true;

				onDispose?.Invoke();

				if ((disposables != null) && (disposables.Count > 0))
				{
					for (int k = (disposables.Count - 1); k >= 0; --k)
						disposables[k].Dispose();

					disposables.Clear();
				}

				DisposeImpl();
			}
		}
	}
}