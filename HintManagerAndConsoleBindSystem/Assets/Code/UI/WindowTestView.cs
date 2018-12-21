using System.Linq;
using System.Text;
using MVVM;
using TMPro;
using UniRx;

namespace Code.UI
{
	public class WindowTestView : ViewBase<IWindowTestViewModel>
	{
		public TextMeshProUGUI TextLabel;
		public TextMeshProUGUI TextLabelR1L1;

		protected override void DestroyViewImpl()
		{
			
		}

		public override void Bind(IWindowTestViewModel viewModel)
		{
			base.Bind(viewModel);
			//viewModel.CurrentGamepadKey.Subscribe(gk => { TextLabel.text = gk.Name; });
			viewModel.CurrentGamepadKeys.ObserveCountChanged(true).Subscribe(_ => { SetTextLabel(); });
		}

		private void SetTextLabel()
		{
			StringBuilder result = new StringBuilder();
			bool needSeparator = false;
			foreach (var item in ViewModel.CurrentGamepadKeys)
			{
				if (needSeparator)
				{
					result.Append(" + ");
				}

				result.Append(item.Name);
				needSeparator = true;
			}

			TextLabel.text = result.ToString();
		}
	}
}