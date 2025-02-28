using System;

namespace Game
{
    [Serializable]
    public class SaveGame
    {
        public bool SeenIntro;
        public string Spawner;
        public int Level;
        public bool Transformed;
        public bool UnlockedWallJump;
        public bool UnlockedDoubleJump;
        public bool UnlockedTripleJump;
    }
}
