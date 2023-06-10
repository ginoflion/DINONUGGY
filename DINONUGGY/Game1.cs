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
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatch;
        Player player;
        Ground ground;
        Homer homer;
        Donuts donut;
        List<Objetos> objetos = new List<Objetos>();
        Camera cam;
        Texture2D texture;

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


            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = new Ground(Content.Load<Texture2D>("Ground"), new Vector2(0, screenHeight-100),100, screenWidth);
            player = new Player(Content.Load<Texture2D>("NUGGY"), new Vector2(screenWidth/ 4 - 35, screenHeight / 2 - 35));
            homer = new Homer(Content.Load<Texture2D>("HOMER"), new Vector2(screenWidth / 2 + 35, screenHeight / 2 +35));
            donut = new Donuts(Content.Load<Texture2D>("DONUT"), new Vector2(screenWidth / 2 + 50, screenHeight / 6));
            
            objetos.Add(homer);
            objetos.Add(ground);
            cam = new Camera();
        }

        

        protected override void Update(GameTime gameTime)
        {
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
           
            player.Update(deltaTime,objetos);
            cam.Follow(player);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            ground.Draw(spriteBatch);
            player.Draw(spriteBatch);
            donut.Draw(spriteBatch);
            homer.Draw(spriteBatch);
           

            spriteBatch.End();
            base.Draw(gameTime);
        }

        

       

        
    }
}