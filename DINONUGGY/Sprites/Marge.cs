using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace DINONUGGY.Sprites
{
    public class Marge : Objetos
    {
        public float speed;
        private float spawnTimer;
        private float spawnInterval = 4.0f;
        public int damage;
        public bool isCollided = false;

        public override Rectangle HitBox => new Rectangle((int)position.X, (int)position.Y, width, height);

        public Marge(Texture2D texture, Vector2 position, bool active, int damage) : base(texture, position, active)
        {
            this.texture = texture;
            this.position = position;
            this.active = active;
            height = 120;
            width = 120;
            speed = 150;
            this.damage = damage;
            spawnTimer = spawnInterval;
        }

        public void Update(double deltaTime, Player player)
        {
            if (player.isDead)
            {
                return;
            }
            
            velocity.X = -speed;
            position += velocity * (float)deltaTime;

            spawnTimer -= (float)deltaTime;

            if (spawnTimer <= 0)
            {
                SpawnMarge();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnMarge()
        {
            if (Game1.listaMarge.Any(marge => marge.position == new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 220)))
            {
                return;
            }
            Marge newMarge = new Marge(texture, new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 220), true, damage);
            Game1.listaMarge.Add(newMarge);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);

        }

    }
}
