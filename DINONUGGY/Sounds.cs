using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DINONUGGY
{
    public static class Sounds
    {
        public static SoundEffect death,damage,jump, margeHit, homerHit;
        

        public static void LoadSounds(ContentManager Content)
        {
            death = Content.Load<SoundEffect>("death");
            damage = Content.Load<SoundEffect>("damage");
            jump = Content.Load<SoundEffect>("Jump");
            homerHit= Content.Load<SoundEffect>("Boring");
        }
    }
}
