using MauMau.Rendering;

namespace MauMau.UI.Elements
{
	public class SelectionList : UIElement
	{
		private int currentlySelectedOption;
		private int scroll;
		
		public List<string> Options;
		public string ListIndicator;
		
		public (int, int) Dimensions;

		public Action? OnSelectedElement;

		public int CurrentSelection => currentlySelectedOption + scroll;

		public SelectionList(Coordinates coords, (int, int) dimensions)
			: base(coords)
		{
			this.Dimensions = dimensions;
			this.Options = new List<string>();
			this.ListIndicator = "> ";
			this.OnSelectedElement = null;
			this.scroll = 0;
			this.currentlySelectedOption = 0;
		}

		public void ResetCurrentSelection()
		{
			this.currentlySelectedOption = 0;
			this.scroll = 0;
		}

		public override void Update()
		{
			if (!IsSelected)
				return;

			if (Input.IsPressed(ConsoleKey.Enter))
			{
				IsLocked = !IsLocked;

				if (IsLocked == false && OnSelectedElement != null)
					OnSelectedElement();
			}

			if (!IsLocked)
				return;
			
			if (Input.IsPressed(ConsoleKey.UpArrow))
				currentlySelectedOption--;

			if (Input.IsPressed(ConsoleKey.DownArrow))
				currentlySelectedOption++;

			if (currentlySelectedOption < 0)
			{
				if (currentlySelectedOption + scroll >= 0)
					scroll--;
				
				currentlySelectedOption++;
			}
			else if (currentlySelectedOption >= Dimensions.Item2 - 2)
			{
				if (currentlySelectedOption + scroll < Options.Count)
					scroll++;
				
				currentlySelectedOption--;
			}
			else if (currentlySelectedOption >= Options.Count)
			{
				currentlySelectedOption--;
			}
		}

		public override void Draw()
		{
			// draw border
			Program.Renderer.PushImage(
				new TextImage().DrawBox(
					Coordinates.ORIGIN, new Coordinates(Dimensions.Item1 - 1, Dimensions.Item2 - 1),
					IsSelected ? (IsLocked ? ConsoleColor.Magenta : ConsoleColor.Yellow) : ConsoleColor.DarkGray
				),
				Coords,
				1,
				true
			);
			
			// draw text
			for (int i = 0; i < Math.Min(Options.Count, Dimensions.Item2 - 2); i++)
			{
				string line = Options[i + scroll];
				
				Program.Renderer.PushImage(
					new TextImage().DrawText(
						ListIndicator + line, (i == currentlySelectedOption) ? (IsSelected ? ConsoleColor.Cyan : ConsoleColor.White) : (IsSelected ? ConsoleColor.White : ConsoleColor.DarkGray)
					),
					new Coordinates(Coords.X + 2 + ((i == currentlySelectedOption) ? 1 : 0), Coords.Y + i + 1),
					2,
					true
				);
			}
		}
	}
}
