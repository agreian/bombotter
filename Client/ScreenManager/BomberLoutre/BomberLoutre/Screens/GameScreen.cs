using Microsoft.Xna.Framework;
using BomberLoutre.Controls;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using BomberLoutre.Components;

namespace BomberLoutre.Screens
{
    public class GameScreen : BaseGameState
    {
        #region Field region

        SoundEffect bombExplosionSound;
        SoundEffect itemLootSound;
        SoundEffect playerDeathSound;

        List<Player> playerList;
        List<Bomb> bombList;
        
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            playerList = new List<Player>();
            bombList = new List<Bomb>();

            for (int i = 0; i < Config.PlayerNumber; ++i)
            {
                playerList.Add(new Player(i, GameRef, new Vector2(Config.MapLayer.X, Config.MapLayer.Y), 0, this));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentManager Content = GameRef.Content;

            if(Properties.App.Default.MusicState)
                MediaPlayer.Play(GameRef.Content.Load<Song>("Audio/Musics/Battle"));
            MediaPlayer.Volume = 0.40f;

            bombExplosionSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/bombExplosion");
            itemLootSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/itemLoot");
            playerDeathSound = GameRef.Content.Load<SoundEffect>("Audio/Sounds/playerDeath");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            int i;

            ControlManager.Update(gameTime, PlayerIndex.One);            

            if (InputHandler.KeyDown(Keys.Escape))
            {
                StateManager.PushState(GameRef.TitleScreen);
                MediaPlayer.Stop();
            }

            for(i = 0; i < Config.PlayerNumber; ++i)
                playerList[i].Update(gameTime);

            for (i = 0; i < bombList.Count; ++i)
                bombList[i].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            int i;

            GameRef.spriteBatch.Begin();
            for(i = 0; i < Config.PlayerNumber; ++i)
                playerList[i].Draw(gameTime);

            for (i = 0; i < bombList.Count; ++i)
                bombList[i].Draw(gameTime);
            GameRef.spriteBatch.End();
            base.Draw(gameTime);
        }

        #endregion

        #region Game Screen Methods
        public void AddBomb(Bomb bomb)
        {
            bombList.Add(bomb);
        }
        #endregion
    }
}
