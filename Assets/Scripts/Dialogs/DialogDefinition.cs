using System;

namespace Dialogs
{
    [Serializable]
    public struct DialogDefinition
    {
        public DialogCharacter Character;
        public string Text;
        public DialogSide Side;
    }

    public enum DialogCharacter
    {
        Tomoya,
        Akari,
        Butterfly,
        BigBad
    }

    public enum DialogSide
    {
        Left,
        Right
    }
}