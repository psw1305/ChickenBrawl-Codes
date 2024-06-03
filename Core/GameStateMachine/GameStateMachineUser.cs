using System;
using static CKB.Utilities.CommonGameplayFacade;

namespace CKB.Core
{
    /// <summary>
    /// Just to avoid same code writing. So many scripts usually hashes GSM, so here is a.
    /// hasher template for them.
    /// </summary>
    public class GameStateMachineUser : SmartScript
    {
        protected virtual void OnEnable()
        {
            BindCallbacks();
        }

        protected virtual void OnDisable()
        {
            if (CoreData.stateMachineOptimizedMode)
                StateMachine.RemoveSubscriber(gameObject);
        }

        private void BindCallbacks()
        {
            On<ReadyState>(OnGameReady);
            On<PlayingState>(OnGamePlay);
            On<GameClearState>(OnGameClear);
            On<GameOverState>(OnGameOver);
            On<SceneLoading>(OnPostgame);

            On<GameClearState, GameOverState>(OnGameFinish);

            On<CustomStateA>(OnCustomStateA);
            On<CustomStateB>(OnCustomStateB);
            On<CustomStateC>(OnCustomStateC);
        }

        protected virtual void OnCustomStateA() { }
        protected virtual void OnCustomStateB() { }
        protected virtual void OnCustomStateC() { }

        protected virtual void OnGamePlay() { }
        protected virtual void OnGameReady() { }
        protected virtual void OnGameClear() { }
        protected virtual void OnGameOver() { }
        protected virtual void OnPostgame() { }
        protected virtual void OnGameFinish() { }

        protected void On<TState>(Action a) where TState : GameState
        {
            StateMachine.On<TState>(a, gameObject);
        }

        protected void On<TState1, TState2>(Action a)
            where TState1 : GameState
            where TState2 : GameState
        {
            StateMachine.On<TState1, TState2>(a, gameObject);
        }
    }
}