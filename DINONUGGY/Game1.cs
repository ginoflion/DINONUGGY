using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using DINONUGGY.Sprites;
using DINONUGGY.State;
using DINONUGGY.ScoreManager;

namespace DINONUGGY
{
    public class Game1 : Game
    {
        public static int screenWidth = 1280;
        public static int screenHeight = 720;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Ground ground;
        Ground ground2;
        List<Homer> homer;
        List<Donuts> donut;
        double DonutTimer = 1000;
        double homerTimer = 2000;
       
        bool SpaceReleased = false;
        bool SavedHighScore = false;
        

        List<Texture2D> homerTextures;
        Texture2D donutTexture;
     

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            player = new Player(Content.Load<Texture2D>("NUGGY"), new Vector2(screenWidth / 2 - 35, screenHeight / 2 - 35));


        }

        

        protected override void Update(GameTime gameTime)
        {
          
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            player.Draw(_spriteBatch);


            base.Draw(gameTime);
        }

        //Cactus Logic

        private void AddCactus(GameTime gameTime)
        {
            
        }

        //Cloud Logic
        private void AddDonut(GameTime gameTime)
        {
            var rand = new Random(gameTime.TotalGameTime.Seconds);
            DonutTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (DonutTimer <= 0 && donut.Count < 3) 
            {
                donut.Add(new Donuts(donutTexture, gameTime));
                DonutTimer = 10000 + rand.NextDouble() * 20000;
            }
        }

        private void ResetGame()
        {
           
        }
    }
}