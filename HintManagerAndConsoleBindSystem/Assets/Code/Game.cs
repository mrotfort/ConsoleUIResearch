using Code.UI;
using UniRx;
using UnityEngine;

namespace Code
{
	public class Game
	{
		public static Game Instance { get; private set; }

		public ReactiveProperty<GamepadKey> CurrentGamepadKey = new ReactiveProperty<GamepadKey>(new GamepadKey {Code = KeyCode.None, Name = "None"});
		public ReactiveCollection<GamepadKey> CurrentGamepadKeys = new ReactiveCollection<GamepadKey>();

		private readonly GamepadController m_GamepadController;
		public WindowTestViewModel WindowTestViewModel = new WindowTestViewModel();

		public Game()
		{
			Instance = this;

			m_GamepadController = new GamepadController();

		}
	}
}