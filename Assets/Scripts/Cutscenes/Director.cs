using System.Collections;
using Player;
using UnityEngine;

namespace Cutscenes
{
    public class Director: MonoBehaviour
    {
        protected IEnumerator MoveCharacterTo(PlayerMover mover, Transform loc)
        {
            bool reached = false;
            mover.MoveTo(loc, () =>
            {
                reached = true;
            });

            yield return new WaitUntil(() => reached);
        }
    }
}