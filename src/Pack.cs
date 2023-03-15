namespace MauMau
{
	public class Pack : CardHolder
	{
		public Pack()
		{
			FillWithInitialCards();
		}

		public override void Reset()
		{
			base.Reset();
			FillWithInitialCards();
		}

		private void FillWithInitialCards()
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 13; j++)
				{
					this.GiveCard(new Card(j + 1, i + 1));
				}
			}
		}

		public void Shuffle()
		{
			for (int i = 0; i < cards.Count; i++)
			{
				int j = rng.Next(0, i + 1);
				(cards[i], cards[j]) = (cards[j], cards[i]);
			}
		}
	}
}
