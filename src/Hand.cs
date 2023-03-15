using MauMau.Rendering;

namespace MauMau
{
	public class Hand : CardHolder
	{
		public string Name { get; private set; }

		public List<Card> Cards => cards;

		public Hand(string name)
		{
			this.Name = name;
		}

		public List<string> GetCardNames()
		{
			List<string> result = new List<string>();

			foreach (var c in cards)
				result.Add(c.GetAsString(false));
			
			return result;
		}
		
		public void PrintCardOptions()
		{
			TextImage output = new TextImage();
			output.DrawText("What card will you play?",		ConsoleColor.Cyan,	new Coordinates(0, 0));
			output.DrawText(" [0] Take from the deck",		ConsoleColor.White, new Coordinates(0, 1));
			output.DrawText("What card will you play?",		ConsoleColor.White, new Coordinates(0, 2));
			output.DrawText("What card will you play?",		ConsoleColor.White, new Coordinates(0, 3));

			Program.Renderer.PushImage(output, new Coordinates(10, 10), 0.75f, true);

			//PrintCards((i) => $" [{i + 1}] ");
		}

		public bool HasWon() => cards.Count <= 0;
	}
}
