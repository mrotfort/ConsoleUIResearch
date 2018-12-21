using System;
using System.Collections.Generic;
using UnityEngine;

namespace MVVM
{
	public abstract class ViewBase<TViewModel> : MonoBehaviour
		where TViewModel : class, IViewModel
	{
		protected TViewModel ViewModel { get; private set; }

		private List<IDisposable> bindDisposables = null;

		protected void AddDisposable(IDisposable disposable)
		{
			if (bindDisposables == null)
				bindDisposables = new List<IDisposable>();

			if ((disposable != null) &&
			    !bindDisposables.Contains(disposable))
			{
				bindDisposables.Add(disposable);
			}
		}

		protected bool RemoveDisposable(IDisposable disposable)
		{
			if (disposable != null)
				return bindDisposables.Remove(disposable);

			return false;
		}

		public virtual void Bind(TViewModel viewModel)
		{
			if (this.ViewModel != null)
			{
				this.ViewModel.OnDispose -= DestroyView;

				if ((bindDisposables != null) && (bindDisposables.Count > 0))
				{
					for (int k = (bindDisposables.Count - 1); k >= 0; --k)
						bindDisposables[k].Dispose();

					bindDisposables.Clear();
				}
			}

			this.ViewModel = viewModel;
			if (viewModel != null)
				viewModel.OnDispose += DestroyView;
		}

		public void DestroyView()
		{
			Bind(null);
			DestroyViewImpl();
		}

		protected abstract void DestroyViewImpl();

		private void OnDestroy()
		{
			Bind(null);
		}
	}
}