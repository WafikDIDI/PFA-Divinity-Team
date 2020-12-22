using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DivinityPFA.Systems
{
    public interface ISaveable
    {
        object CaptureState ();
        void RestoreState (object state);
    }
}
