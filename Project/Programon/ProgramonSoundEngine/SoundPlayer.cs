using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace ProgramonSoundEngine
{
    public class SoundPlayer
    {
        public void PlayStaticSound(SoundEffect sound)
        {
            sound.Play();
        }

        public void PLayStaticSound(SoundEffect sound, float volume, float pitch, float pan)
        {
            sound.Play(volume, pitch, pan);
        }
    }
}
