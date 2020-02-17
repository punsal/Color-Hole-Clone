using UnityEngine;

namespace EventArguments
{
    public enum GameEventType
    {
        GameStart,
        LevelStart,
        ItemCollected,
        EnemyCollected,
        GroundCompleted,
        CameraFollow,
        LevelComplete,
        LevelFailed,
        IncrementCollectibleItemCount,
        LevelContinue,
        LevelReload
    }
}
