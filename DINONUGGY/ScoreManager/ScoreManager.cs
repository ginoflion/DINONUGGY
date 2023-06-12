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
        public static int HighScore { get; private set; }
        public static int Score { get; private set; }
        private static double ScoreTimer = 0;
        private static DateTime StartTime;


        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DINONUGGY");

        public static void InitializeHighScore()
        {
            Directory.CreateDirectory(path);
            if (File.Exists(Path.Combine(path, "score.txt")))
            {
                try
                {
                    HighScore = int.Parse(File.ReadLines(Path.Combine(path, "score.txt")).First());
                }
                catch
                {
                    HighScore = 0;
                }
            }
            else
            {
                var file = File.Create(Path.Combine(path, "score.txt"));
                file.Close();
                HighScore = 0;
            }
        }

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




        public static int SetHighScore()
        {
            if (Score > HighScore)
            {
                File.WriteAllText(Path.Combine(path, "score.txt"), Score.ToString());
                HighScore = Score;
            }
            return HighScore;
        }
    }


}
