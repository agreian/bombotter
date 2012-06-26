using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BomberLoutre.Controls;
using BomberLoutre.Components;
using BomberLoutre.World;

namespace BomberLoutre.Screens
{
    public class GameScreen : BaseGameState
    {
        #region Field region
        GameStateManager gsManager;
        SoundEffect bombExplosionSound;
        SoundEffect itemLootSound;
        SoundEffect playerDeathSound;

        List<Player> playerList;
        List<Bomb> bombList;

        Map MapZone;
        #endregion

        #region Constructor region
        public GameScreen(BomberLoutre game, GameStateManager manager) : base(game, manager)
        {
            gsManager = manager;
        }
        #endregion

        #region XNA Method region

        public override void Initialize()
        {
            playerList = new List<Player>();
            bombList = new List<Bomb>();

            for (int i = 0; i < Config.PlayerNumber; ++i)
            {
                // Pour le moment, la loutre est placée au coin gauche supérieure, donc (X + largeur/2) et (Y + hauteur/2)
                playerList.Add(new Player(i, GameRef, new Vector2(Config.MapLayer.X + (Config.OtterWidth/2), Config.MapLayer.Y + (Config.OtterHeight/2)), 0, this));
            }

            MapZone = new Map(GameRef, gsManager);

            base.Initialize();
        }

        protected override void LoadContent()
        {
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
            MapZone.Draw(gameTime);

            GameRef.spriteBatch.Begin();

            /* DESSIN DE LA ZONE-MAP 
            Texture2D rect = new Texture2D(GameRef.graphics.GraphicsDevice, Config.MapLayer.Width, Config.MapLayer.Height);

            Color[] data = new Color[Config.MapLayer.Width * Config.MapLayer.Height];
            for (i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

            Vector2 coor = new Vector2(Config.MapLayer.X, Config.MapLayer.Y);
            GameRef.spriteBatch.Draw(rect, coor, Color.White);
            FIN DU DESSIN DE LA ZONE-MAP */

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
