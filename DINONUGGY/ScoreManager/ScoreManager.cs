using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY.ScoreManager
{
    public static class ScoreManager
    {
        public static int HighScore { get; set; }
        public static int Score { get; set; }
        private static double ScoreTimer = 100;

        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DinoRunGame");

        public static void InitializeHighScore()
        {
            Directory.CreateDirectory(path);
            if (File.Exists(path + @"\score.txt"))
            {
                try
                {
                    HighScore = int.Parse(File.ReadLines(path + @"\score.txt").First());
                }
                catch
                {
                    HighScore = 0;
                }
            }
            else
            {
                var file = File.Create(path + @"\score.txt");
                file.Close();
                HighScore = 0;
            }
        }

        public static float UpdateScore(GameTime gameTime, float worldSpeed)
        {
            ScoreTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if (ScoreTimer <= 0)
            {
                Score++;
                if (Score % 100 == 0 && worldSpeed < 10)
                    worldSpeed += 0.5f;

                ScoreTimer = 100;
            }
            return worldSpeed;
        }

        public static int SetHighScore()
        {
            if (Score > HighScore)
            {
                File.WriteAllText(path + @"\score.txt", Score.ToString());
                HighScore = Score;
            }
            return HighScore;
        }
    }
}
