﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.Sprites
{
    public class Homer : Objetos
    {
        public float speed;
        private float spawnTimer;
        private float spawnInterval = 10.0f;
        private const int MaxHomers = 5;
        public override Rectangle HitBox => new Rectangle((int)position.X, (int)position.Y, width, height);

        public Homer(Texture2D texture, Vector2 position) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            height = 80;
            width = 80;
            speed = 60;
            spawnTimer = spawnInterval;
        }

        public void Update(double deltaTime)
        {
            velocity.X = -speed;
            position += velocity * (float)deltaTime;

          

            spawnTimer -= (float)deltaTime;

            if (spawnTimer <= 0)
            {
                SpawnHomer();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnHomer()
        {
            if (Game1.listaHomers.Count < MaxHomers)
            {
                Homer newHomer = new Homer(texture, new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 180));
                Game1.listaHomers.Add(newHomer);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
        }
    }

}
