using DINONUGGY.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.ScoreManager
{
    public static class ScoreManager
    {
        public static int Score { get; private set; }
        private static double ScoreTimer = 0;
        private static DateTime StartTime;



        public static void StartGame()
        {
            Score = 0;
            StartTime = DateTime.Now;
            ScoreTimer = 0;
        }

        public static void UpdateScore(GameTime gameTime,Player player)
            {
            if (player.isDead)
            {
                return;
            }


            double elapsedSeconds = (DateTime.Now - StartTime).TotalSeconds;
            ScoreTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ScoreTimer >= 1)
            {
                Score = (int)(elapsedSeconds * 7);
                ScoreTimer = 0;
            }
        }




   
    }


}
