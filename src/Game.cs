using MauMau.Rendering;
using MauMau.UI;

namespace MauMau
{
	public class Game
	{
		private bool firstFrame = true;
		
		private GameUI ui;
		
		private Pack pack;
		private Deck deck;
		private Hand player1;
		private Hand player2;
		
		private Hand currentPlayer;

		public Hand CurrentPlayer => currentPlayer;
		public Deck Deck => deck;

		public bool ForceSkipOnNextPlayer = false;
		public int SevenTakeCardCounter = 0;

		public Game()
		{
			Reset();
		}

		public string Run()
		{
			currentPlayer = player1;
			ui.Init();
			
			while (!player1.HasWon() && !player2.HasWon())
			{
				if (!firstFrame)
					Input.PollNewKey();

				ui.Update();
				ui.Draw();
				
				Program.Renderer.Clear();
				Program.Renderer.Render();

				firstFrame = false;
			}

			return player1.HasWon() ? player1.Name : player2.Name;
		}

		public void SwapTurns()
		{
			currentPlayer = (currentPlayer == player1 && !ForceSkipOnNextPlayer) ? player2 : player1;
			ForceSkipOnNextPlayer = false;
			
			Program.Renderer.Clear();
			UIUtils.PopupMessage("Switching players... (press [ANY KEY] to continue)");
		}

		public void Reset()
		{
			ui = new GameUI(this);

			pack    = new Pack();
			deck    = new Deck();
			player1 = new Hand("Player 1");
			player2 = new Hand("Player 2");
			pack.Shuffle();
			
			for (int i = 0; i < 4; i++)
			{
				player1.GiveCard(pack.DealCard());
				player2.GiveCard(pack.DealCard());
			}
			
			while (pack.CardCount > 0)
				deck.GiveCard(pack.DealCard());
		}
		
		public static bool IsCardValid(Card? newCard, Card? lastCard)
		{
			if (lastCard == null)
				return true;
			
			if (newCard == null)
				return false;

			string nr = newCard.GetRankText();
			string lr = lastCard.GetRankText();

			if (nr is ("Ace" or "Seven"))
				return true;

			if (nr != "Queen")
				return newCard.Rank == lastCard.Rank || newCard.Suit == lastCard.Suit;

			return lr is not ("Ace" or "Seven");
		}
	}
}
