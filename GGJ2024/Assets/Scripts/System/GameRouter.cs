using UnityEngine;
using Utility;

namespace System
{
    public static class GameRouter
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            Locator.Register(new GameEvent());
        }
    }
}