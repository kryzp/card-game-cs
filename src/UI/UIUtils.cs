using MauMau.Rendering;

namespace MauMau.UI
{
	public static class UIUtils
	{
		public static string PopupPrompt(string prompt)
		{
			var coords = new Coordinates(
				(Program.WINDOW_WIDTH / 2) - 2 - (prompt.Length / 2),
				(Program.WINDOW_HEIGHT / 2) - 2
			);

			Program.Renderer.PushImage(new TextImage()
					.DrawBox(
						Coordinates.ORIGIN,
						new Coordinates(prompt.Length + 3, 3),
						ConsoleColor.DarkGray
					).DrawText(
						prompt, ConsoleColor.Cyan,
						new Coordinates(2, 1)
					),
				coords, 10, true
			);

			Program.Renderer.Render();

			Console.SetCursorPosition(coords.X + 2, coords.Y + 2);
			Console.Write("> ");
			return Console.ReadLine() ?? "";
		}

		public static void PopupMessage(string prompt, ConsoleColor col = ConsoleColor.Cyan, int yoffset = 0)
		{
			var coords = new Coordinates(
				(Program.WINDOW_WIDTH / 2) - 2 - (prompt.Length / 2),
				(Program.WINDOW_HEIGHT / 2) - 2 + yoffset
			);

			Program.Renderer.PushImage(new TextImage()
					.DrawBox(
						new Coordinates(0, 0), new Coordinates(prompt.Length + 3, 2),
						ConsoleColor.DarkGray
					).DrawText(
						prompt, col, new Coordinates(2, 1)
					),
				coords, 10, true
			);

			Program.Renderer.Render();
			
			Console.ReadKey();
		}

		public static void PopupMessageColouredName(string prompt, string name, ConsoleColor nameColour, ConsoleColor col = ConsoleColor.Cyan, int yoffset = 0)
		{
			var coords = new Coordinates(
				(Program.WINDOW_WIDTH / 2) - 2 - (prompt.Length / 2),
				(Program.WINDOW_HEIGHT / 2) - 2 + yoffset
			);

			prompt = prompt.Replace("[[NAME]]", name);

			Program.Renderer.PushImage(new TextImage()
					.DrawBox(
						new Coordinates(0, 0), new Coordinates(prompt.Length + 3, 2),
						ConsoleColor.DarkGray
					).DrawText(
						prompt, col, new Coordinates(2, 1)
					).DrawText(
						name, nameColour, new Coordinates(2 + prompt.IndexOf(name), 1)
					),
				coords, 10, true
			);

			Program.Renderer.Render();
			
			Console.ReadKey();
		}

		public static void PopupError(string error)
		{
			PopupMessage(error, ConsoleColor.Red);
		}
	}
}
