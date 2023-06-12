using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using DINONUGGY.Sprites;
using DINONUGGY.ScoreManager;

namespace DINONUGGY
{
    public class Game1 : Game
    {
        public static int screenWidth = 1280;
        public static int screenHeight = 720;
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatchPlayer, spriteBatchUI;
        Player player;
        Ground ground;
        Homer homer;
        Donuts donut;
        Marge marge;
        public static List<Objetos> objetos = new List<Objetos>();
        public static List<Homer> listaHomers = new List<Homer>();
        public static List<Marge>listaMarge = new List<Marge>();
        Camera cam;
        private TimeSpan gameTimeElapsed;
        private SpriteFont _font;
        bool isTimePaused = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = screenHeight; 
            _graphics.PreferredBackBufferWidth = screenWidth; 
            _graphics.ApplyChanges();
            

            ScoreManager.ScoreManager.StartGame();
            cam = new Camera();
            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            spriteBatchPlayer = new SpriteBatch(GraphicsDevice);
            spriteBatchUI= new SpriteBatch(GraphicsDevice);
            ground = new Ground(Content.Load<Texture2D>("Ground"), new Vector2(0, screenHeight-100),100, screenWidth*2);
            player = new Player(Content.Load<Texture2D>("NUGGY"), new Vector2(screenWidth/ 4 - 35, screenHeight / 2 - 35));
            homer = new Homer(Content.Load<Texture2D>("HOMER"), new Vector2(screenWidth * 2 , screenHeight -180), true, 25);
            marge = new Marge(Content.Load<Texture2D>("MARGE"), new Vector2(screenWidth * 2, screenHeight - 200), true, 50);
            donut = new Donuts(Content.Load<Texture2D>("DONUT"), new Vector2(screenWidth -300, 0));
            _font = Content.Load<SpriteFont>("Fonte");
            Sounds.LoadSounds(Content);
            listaHomers.Add(homer);
            objetos.Add(ground);
            listaMarge.Add(marge);
            
        }



        protected override void Update(GameTime gameTime)
        {
            if (player.isDead)
            {
                
                isTimePaused = true;
            }
            if (!isTimePaused)
            {
                gameTimeElapsed += gameTime.ElapsedGameTime;
            }
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = listaHomers.Count - 1; i >= 0; i--)
            {
                Homer homer = listaHomers[i];
                homer.Update(deltaTime, player);


                if (homer.position.X < -homer.width)
                {
                    listaHomers.RemoveAt(i);
                }
            }
            for (int i = listaMarge.Count - 1; i >= 0; i--)
            {
                Marge marge = listaMarge[i];
                marge.Update(deltaTime, player);


                if (marge.position.X < -marge.width)
                {
                    listaMarge.RemoveAt(i);
                }
            }
            player.Update(deltaTime, objetos, listaHomers,listaMarge);

            ScoreManager.ScoreManager.UpdateScore(gameTime,player);
            cam.Follow(player);
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatchPlayer.Begin(transformMatrix: cam.Transform);
            spriteBatchUI.Begin();
            spriteBatchUI.DrawString(_font, "HP:" + player.hp , new Vector2(10, 50), Color.White);
            spriteBatchUI.DrawString(_font, ScoreManager.ScoreManager.Score.ToString(), new Vector2(screenWidth-220,20), Color.White);
            spriteBatchUI.DrawString(_font, "Tempo de Jogo:" + gameTimeElapsed.TotalSeconds.ToString("0.00")  +  " segundos", new Vector2(10,10), Color.White);
            ground.Draw(spriteBatchPlayer);
            foreach(Homer item in listaHomers)
            {
                if (item.active == true)
                {
                    item.Draw(spriteBatchPlayer);
                }
            }
            foreach (Marge item in listaMarge)
            {
                if (item.active == true)
                {
                    item.Draw(spriteBatchPlayer);
                }
            }
            player.Draw(spriteBatchPlayer);
            donut.Draw(spriteBatchUI);
            
           

            spriteBatchPlayer.End();
            spriteBatchUI.End();
            base.Draw(gameTime);
        }

        
    }
}