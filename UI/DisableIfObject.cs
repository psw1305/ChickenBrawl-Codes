using UnityEngine;
using CKB.Core;
using CKB.Utilities;

public class DisableIfObject : GameStateMachineUser
{
    [SerializeField] private GameState gameState;

    protected override void OnGamePlay() => DisableIf(GameState.Playing);

    protected override void OnGameFinish() => DisableIf(GameState.GameFinish);

    protected override void OnGameClear() => DisableIf(GameState.GameClear);

    protected override void OnGameOver() => DisableIf(GameState.GameOver);

    private void DisableIf(GameState state)
    {
        if (gameState == state)
        {
            gameObject.Off();
        }
    }
}
