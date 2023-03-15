using MauMau.Rendering;
using MauMau.UI;

/*
 * I am using the Czech variant: "Prší", as that's what I'm most familiar with.
 * Also I'm placing all of the classes into here to make uploading easier.
 * Players are dealt 4 cards each.
 * Ace forces the next player to skip.
 * 7 forces the next player to draw two cards, unless they too play a 7, thus requiring the next player to draw 4 cards and so on. A player who draws cards cannot play a card in the same turn.
 * A Queen can be played on any card except a 7 or Ace and its player may then choose a suit. The next player then plays as if the Queen was of the chosen suit.
 * When a player gets rid of their cards, they may be returned to the game if they would have taken cards because of ♥7 specifically (or ♥10 from the variation above). Thus the player truly wins only when they haven't had a card in their hand for one whole additional round. (Saying Prší is acceptable every time a person loses all his cards)
 */

namespace MauMau
{
	public class Program
	{
		public const int WINDOW_WIDTH = 120;
		public const int WINDOW_HEIGHT = 40;

		public static Game Game;
		public static Renderer Renderer;

		public static void Main()
		{
			Renderer = new Renderer();
			Game = new Game();
			
			UIUtils.PopupMessage(Game.Run() + " has won!");
		}
	}
}
