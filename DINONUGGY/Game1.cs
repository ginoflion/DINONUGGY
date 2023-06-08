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
        Homer homer;
        Donuts donut;
        bool SpaceReleased = false;
        bool SavedHighScore = false;
        

      

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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = new Ground(Content.Load<Texture2D>("Ground"), new Vector2(0, 0), screenWidth, 50);
            player = new Player(Content.Load<Texture2D>("NUGGY"), new Vector2(screenWidth/ 4 - 35, screenHeight / 2 - 35));
            homer = new Homer(Content.Load<Texture2D>("HOMER"), new Vector2(screenWidth / 2 + 35, screenHeight / 2 +35));
            donut = new Donuts(Content.Load<Texture2D>("DONUT"), new Vector2(screenWidth / 2 + 50, screenHeight / 6));
        }

        

        protected override void Update(GameTime gameTime)
        {
           
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            donut.Draw(spriteBatch);
            homer.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        

        private void AddCactus(GameTime gameTime)
        {
            
        }

        

        
    }
}