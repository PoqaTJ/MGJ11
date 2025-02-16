using System.Collections.Generic;
using UnityEngine;

namespace Dialogs
{
    [CreateAssetMenu]
    public class ConversationDefinition: ScriptableObject
    {
        public string ID = "";
        public List<DialogDefinition> Dialogs = new();
    }
}