using System;
using MVVM;
using UniRx;

namespace Code.UI
{
	public interface IWindowTestViewModel : IViewModel
	{
		IReactiveProperty<GamepadKey> CurrentGamepadKey { get; }
		IReactiveCollection<GamepadKey> CurrentGamepadKeys { get; }
	}

	public class WindowTestViewModel : ViewModelBase, IWindowTestViewModel
	{
		
		public IReactiveProperty<GamepadKey> CurrentGamepadKey => Game.Instance.CurrentGamepadKey;
		public IReactiveCollection<GamepadKey> CurrentGamepadKeys => Game.Instance.CurrentGamepadKeys;
	}
}