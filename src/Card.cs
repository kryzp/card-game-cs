using MauMau.UI;

namespace MauMau
{
	public class Card
	{
		private int rank;
		private int suit;

		public int Rank => rank;
		public int Suit => suit;
		public int Score => (rank * 4) + suit;

		public Card(int r, int s)
		{
			this.rank = r;
			this.suit = s;
		}

		public string GetRankText()
		{
			return rank switch
			{
				1 => "Ace",
				2 => "Two",
				3 => "Three",
				4 => "Four",
				5 => "Five",
				6 => "Six",
				7 => "Seven",
				8 => "Eight",
				9 => "Nine",
				10 => "Ten",
				11 => "Jack",
				12 => "Queen",
				13 => "King",
				_ => ""
			};
		}

		public string GetSuitText()
		{
			return suit switch
			{
				1 => "Diamonds",
				2 => "Spades",
				3 => "Hearts",
				4 => "Clubs",
				_ => ""
			};
		}

		// technically i could do this with inheritance, but that would be stupid, time consuming and wasteful to make an entire class for a single line of code.
		public void SpecialAbility(Hand h, Deck d)
		{
			if (GetRankText() == "Ace")
			{
				Program.Game.ForceSkipOnNextPlayer = true;
				UIUtils.PopupMessage("Skipping next players turn >:)");
			}
			else if (GetRankText() == "Seven")
			{
				Program.Game.SevenTakeCardCounter += 2;
				UIUtils.PopupMessage($"Playing the lucky 7! Seven counter is at {Program.Game.SevenTakeCardCounter}!");
			}
			else if (GetRankText() == "Queen")
			{
				char c = ' ';
				do
				{
					string cc = UIUtils.PopupPrompt("What type of card should the queen take? [D]iamonds, [S]pades, [H]earts or [C]lubs").ToUpper();
					c = ((cc.Length > 0) ? cc : " ")[0];
				}
				while (c != 'D' && c != 'S' && c != 'H' && c != 'C');
				
				switch (c)
				{
					case 'D': suit = 1; break;
					case 'S': suit = 2; break;
					case 'H': suit = 3; break;
					case 'C': suit = 4; break;
				}
			}
			else if (Program.Game.SevenTakeCardCounter != 0)
			{
				UIUtils.PopupMessage($"Giving {Program.Game.SevenTakeCardCounter} cards due to seven counting.");
				
				for (int i = 0; i < Program.Game.SevenTakeCardCounter; i++)
					h.GiveCard(d.DealFromTop());

				Program.Game.SevenTakeCardCounter = 0;
			}
		}

		public string GetAsString(bool infoText)
		{
			if (!infoText)
				return GetRankText() + " of " + GetSuitText();

			return GetRankText() switch
			{
				"Ace" => "ACE! (Force a skip on next player)",
				"Seven" => "7! (Force next player to draw two cards (unless they play a 7 too, making you take 4, and so on...)",
				"Queen" => "QUEEN! (Can be played on any card (and choose the new type), unless the card is a 7 or an ACE)",
				_ => GetRankText() + " of " + GetSuitText()
			};
		}
	}
}
