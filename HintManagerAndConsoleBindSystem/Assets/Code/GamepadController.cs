using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniRx;
using UnityEngine;

namespace Code
{
	public enum ControllerType
	{
		None,
		Ps4Controller,
		XBoxController,
		Switch,
		SteamController
	}

	public enum PsControllerKeyType
	{
		Square,
		Cross,
		Circle,
		Triangle,
		L1,
		R1,
		L2,
		R2,
		Shared,
		Options,
		L3,
		R3,
		Touchpad
	}

	public class GamepadKey
	{
		public KeyCode Code;

		public Sprite Icon;

		public string Name;

		public GamepadKey()
		{
			var pressedStream = Observable.EveryUpdate().Where(_ => Input.GetKeyDown(Code));
			pressedStream.Subscribe(xs => PressedCallback());

			var releaseStream = Observable.EveryUpdate().Where(_ => Input.GetKeyUp(Code));
			releaseStream.Subscribe(xs => ReleaseCallback());

			var holdStream = Observable.EveryUpdate().Where(_ => Input.GetKey(Code));
			holdStream.Subscribe(xs => HoldCallback());
		}

		private void PressedCallback()
		{
			//Debug.Log($"{Name} is pressed");
			//Game.Instance.CurrentGamepadKey.Value = this;
			Game.Instance.CurrentGamepadKeys.Add(this);
		}

		private void ReleaseCallback()
		{
			//Debug.Log($"{Name} is release");
			Game.Instance.CurrentGamepadKeys.Remove(this);
		}

		private void HoldCallback()
		{
			//Debug.Log($"{Name} is hold");
		}
	}

	public class GamepadController
	{
		private ControllerType m_ControllerType;

		private readonly List<GamepadKey> m_GamepadKeys = new List<GamepadKey>();

		public bool IsSomeControlerConnected => m_ControllerType != ControllerType.None;

		public GamepadController()
		{
			Activate();
		}

		public void Activate()
		{
			m_ControllerType = ControllerType.None;

			var names = Input.GetJoystickNames();
			foreach (var joyName in names)
			{
				//нужно найти более вменяемый способ поиска контроллеров
				//а также не забыть про Switch
				switch (joyName.Length)
				{
					case 19:
						Debug.Log("PS4 CONTROLLER IS CONNECTED");
						m_ControllerType = ControllerType.Ps4Controller;
						break;
					case 33:
						Debug.Log("XBOX ONE CONTROLLER IS CONNECTED");
						m_ControllerType = ControllerType.XBoxController;
						break;
				}
			}

			if (!IsSomeControlerConnected)
			{
				return;
			}

			if (m_Ps4Controller)
			{
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button0, Name = "Square"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button1, Name = "Cross"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button2, Name = "Circle"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button3, Name = "Triangle"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button4, Name = "L1"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button5, Name = "R1"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button6, Name = "L2"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button7, Name = "R2"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button8, Name = "Shared"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button9, Name = "Options"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button10, Name = "L3"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button11, Name = "R3"});
				m_GamepadKeys.Add(new GamepadKey {Code = KeyCode.Joystick1Button13, Name = "Touchpad"});
				return;
			}
		}

		public void Deactivate()
		{
		}
	}
}