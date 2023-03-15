namespace MauMau
{
	public abstract class CardHolder
	{
		protected List<Card> cards;
		protected Random rng;
		
		public int CardCount => cards.Count;

		public CardHolder()
		{
			cards = new List<Card>();
			rng = new Random();
		}
		
		public Card DealCard()
		{
			Card c = cards[^1];
			cards.RemoveAt(cards.Count - 1);
			return c;
		}
		
		public Card DealFromTop()
		{
			Card c = cards[0];
			cards.RemoveAt(0);
			return c;
		}

		public void GiveCard(Card c) => cards.Add(c);
		public void RemoveCard(int idx) => cards.RemoveAt(idx); 
		public virtual void Reset() => cards.Clear();
	}
}
