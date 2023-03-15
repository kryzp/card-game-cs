using MauMau.Rendering;
using MauMau.UI;
using MauMau.UI.Elements;

namespace MauMau.UI
{
    public class GameUI
    {
        private Game game;
        private Menu gameMenu;
        
        // elements
        private Card? lastPlayedCard;
        private SelectionList list;
        private Button runMoveButton;
        private Button drawNextCardButton;
        private TextBox topIndicator;

        public GameUI(Game owner)
        {
            lastPlayedCard = null;
            
            game = owner;
            gameMenu = new Menu();
            
            list = new SelectionList(new Coordinates((Program.WINDOW_WIDTH - 40) / 2 - 25, Program.WINDOW_HEIGHT - 20), (40, 10))
            {
                ID = 0,
                RightID = 1
            };

            runMoveButton = new Button(new Coordinates(Program.WINDOW_WIDTH / 2 + 5, Program.WINDOW_HEIGHT - 20), "Run Move", PlayOutMove)
            {
                ID = 1,
                LeftID = 0,
                DownID = 2
            };

            drawNextCardButton = new Button(new Coordinates(Program.WINDOW_WIDTH / 2 + 5, Program.WINDOW_HEIGHT - 13), "Draw New Card", DrawNewCard)
            {
                ID = 2,
                LeftID = 0,
                UpID = 1
            };

            topIndicator = new TextBox(new Coordinates((Program.WINDOW_WIDTH - 30) / 2, 8), (30, 4))
            {
                ID = 3
            };

            gameMenu.Add(list);
            gameMenu.Add(runMoveButton);
            gameMenu.Add(drawNextCardButton);
            gameMenu.Add(topIndicator);
            
            gameMenu.SetSelectedElement(0);
        }

        public void Init()
        {
            list.Options = game.CurrentPlayer.GetCardNames();
            topIndicator.Text = "Currently on top of deck:\n" + ((lastPlayedCard != null) ? lastPlayedCard.GetAsString(false) : "No card!");
        }

        public void Update()
        {
            gameMenu.Update();
        }

        public void Draw()
        {
            string whoIsPlayingText = $"It is currently {game.CurrentPlayer.Name}'s turn.";
				
            Program.Renderer.PushImage(
                new TextImage(whoIsPlayingText, ConsoleColor.White),
                new Coordinates((Program.WINDOW_WIDTH - whoIsPlayingText.Length) / 2, 1),
                0.5f
            );
            
            Program.Renderer.PushImage(
                new TextImage("Playing Prší (the card game)", ConsoleColor.White),
                new Coordinates(1, 1),
                0.5f
            );
            
            Program.Renderer.PushImage(
                new TextImage()
                    .DrawLine(
                        Coordinates.ORIGIN,
                        new Coordinates(0, 12),
                        new ColouredChar(Characters.VERT_BOX, ConsoleColor.DarkGray)
                    ),
                new Coordinates(Program.WINDOW_WIDTH / 2, Program.WINDOW_HEIGHT - 21),
                0.0f,
                true
            );
            
            Program.Renderer.PushImage(
                new TextImage()
                    .DrawBox(
                        Coordinates.ORIGIN,
                        new Coordinates(Program.WINDOW_WIDTH - 2, 5),
                        ConsoleColor.DarkGray
                    )
                    .DrawText(
                        "INSTRUCTIONS",
                        ConsoleColor.White,
                        new Coordinates(1, 1)
                    )
                    .DrawText(
                        "[ENTER] to select the card element and select a card",
                        ConsoleColor.White,
                        new Coordinates(1, 2)
                    )
                    .DrawText(
                        "[ENTER] to activate buttons",
                        ConsoleColor.White,
                        new Coordinates(1, 3)
                    )
                    .DrawText(
                        "[ARROW KEY]'s to move around the menu",
                        ConsoleColor.White,
                        new Coordinates(1, 4)
                    ),
                new Coordinates(1, Program.WINDOW_HEIGHT - 6),
                0.2f,
                true
            );
            
            gameMenu.Draw();
        }

        private void PlayOutMove()
        {
            Card? nextPlayedCard = game.CurrentPlayer.Cards[list.CurrentSelection];

            if (!Game.IsCardValid(nextPlayedCard, lastPlayedCard))
            {
                UIUtils.PopupError("Card is invalid!");
                return;
            }

            game.CurrentPlayer.RemoveCard(list.CurrentSelection);

            nextPlayedCard.SpecialAbility(game.CurrentPlayer, game.Deck);
            lastPlayedCard = nextPlayedCard;
            
            topIndicator.Text = "Currently on top of deck:\n" + ((lastPlayedCard != null) ? lastPlayedCard.GetAsString(false) : "No card!");

            game.Deck.GiveCard(nextPlayedCard);
            game.SwapTurns();
            
            list.Options = game.CurrentPlayer.GetCardNames();
            list.ResetCurrentSelection();
        }

        private void DrawNewCard()
        {
            Card dealt = game.Deck.DealFromTop();
            game.CurrentPlayer.GiveCard(dealt);
            UIUtils.PopupMessage("Dealt a " + dealt.GetAsString(false));
            list.Options = game.CurrentPlayer.GetCardNames();
        }
    }
}
