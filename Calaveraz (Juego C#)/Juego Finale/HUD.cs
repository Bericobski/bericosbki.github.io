using System;
using System.Collections.Generic;
using System.Text;
using SFML.System;
using SFML.Graphics;

namespace Juego_Finale
{
    class HUD
    {
        private RenderWindow window;
        private Jugador player;
        private Font font;
        private Text livesText;
        private Text scoreText;

        private const string LivesMessage = "Vidas: ";
        private const string ScoreMessage = "Puntos: ";
        private const string ScoreMessageMax = "/100";
        private const float ScreenMargins = 30f;
        private const int FontSize = 30;
        private const float OutlineThickness = 3f;


        public HUD(RenderWindow window, Jugador player, string fontPath)
        {
            this.window = window;
            this.player = player;
            font = new Font(fontPath);
            livesText = new Text(LivesMessage + player.GetHp(), font);
            scoreText = new Text(ScoreMessage + player.Score, font);

            livesText.CharacterSize = FontSize;
            scoreText.CharacterSize = FontSize;

            livesText.OutlineThickness = OutlineThickness;
            scoreText.OutlineThickness = OutlineThickness;
            livesText.FillColor = Color.White;
            scoreText.FillColor = Color.White;

            livesText.OutlineColor = Color.Black;
            scoreText.OutlineColor = Color.Black;

            player.OnHpChange += OnPlayerChangeHp;
            //No quiero traer a todos los mobs a este metodo
        }

        private void OnPlayerChangeHp()
        {
            livesText.DisplayedString = LivesMessage + player.GetHp();
        }

        public void Update()
        {
            View view = window.GetView();
            float verticalOffset = -window.Size.Y / 2 + ScreenMargins;
            Vector2f leftCornerOffset = new Vector2f(-window.Size.X / 2  +  ScreenMargins, verticalOffset);
            Vector2f rightCornerOffset = new Vector2f(window.Size.X / 2 - scoreText.GetGlobalBounds().Width - ScreenMargins, verticalOffset);

            livesText.Position = view.Center + leftCornerOffset;
            scoreText.Position = view.Center + rightCornerOffset;
            scoreText.DisplayedString = ScoreMessage + player.Score + ScoreMessageMax;
        }

        public void Draw()
        {
            window.Draw(livesText);
            window.Draw(scoreText);
        }
    }
}
