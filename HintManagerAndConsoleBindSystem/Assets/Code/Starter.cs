using Code.UI;
using UnityEngine;

namespace Code
{
	public class Starter : MonoBehaviour
	{
		private Game m_Game;

		public WindowTestView WindowTestView;

		private void Start()
		{
			m_Game = new Game();
			WindowTestView.Bind(Game.Instance.WindowTestViewModel);
		}
	}
}