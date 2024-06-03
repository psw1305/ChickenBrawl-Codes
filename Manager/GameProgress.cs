using UnityEngine;
using CKB.Core;
using static CKB.Utilities.CommonGameplayFacade;

public class GameProgress : GameStateMachineUser
{
    [SerializeField] private BossLevel bossLevel;

    [Header("Revive")]
    [SerializeField] private GameObject reviveWindow;
    public int ReviveCount { get; private set; }

    [Header("Timer")]
    [SerializeField] private Timer timer;
    [SerializeField] private float playTimeDuration = 80;
    [SerializeField] private float stepTimeDuration = 10;
    private float playTime = 0;
    private float stepTime = 0;
    private float rankTime = 0;

    [Header("Transform")]
    [SerializeField] private Transform chickRest;

    [Header("Grounds")]
    [SerializeField] private MeshRenderer mainFloor;
    [SerializeField] private MeshRenderer[] bossFloors;

    private bool isStarted = false;
    private bool isFinished = false;
    private int step = 1;

    public Transform ChickRest => chickRest;
    public bool IsStarted => isStarted;

    public void SetStarted()
    {
        isStarted = true;
    }

    private void Awake()
    {
        Progress = this;

        playTime = playTimeDuration;
        stepTime = stepTimeDuration;
        ReviveCount = 1;

        mainFloor.material = GameData.LevelFloorMaterial();
        for (int i = 0; i < bossFloors.Length; i++)
        {
            bossFloors[i].material = GameData.LevelFloorMaterial();
        }
    }

    private void Update()
    {
        if (isStarted && !isFinished)
        {
            PlayTime();
            StepTime();
            RankTime();
        }
    }

    private void PlayTime()
    {
        timer.DisplayTime(playTime);
        playTime -= Time.deltaTime;

        if (playTime <= -0.5f)
        {
            timer.DisplayTime();
            timer.DisplayAlertTime();
            StateMachine.Push(new GameClearState());
        }
        else if (playTime <= 5.0f)
        {
            timer.DisplayAlertTime(playTime);
        }
    }

    private void StepTime()
    {
        stepTime -= Time.deltaTime;

        if (stepTime <= 0)
        {
            stepTime = 15;
            step++;
        }
    }

    private void RankTime()
    {
        rankTime -= Time.deltaTime;

        if (rankTime <= 0)
        {
            rankTime = 1.0f;
            Rank.UpdateScore();
        }
    }

    #region State

    protected override void OnGamePlay()
    {
        isStarted = true;
        bossLevel.DisplayLevel();
        Rank.UpdateScore();
    }

    protected override void OnGameFinish()
    {
        isFinished = true;
        Rank.UpdateScore();
    }

    #endregion

    public int StepEnemyLevel()
    {
        return step switch
        {
            1 => 1,
            2 => Random.Range(25, 50),
            3 => Random.Range(50, 250),
            4 => Random.Range(250, 500),
            5 => Random.Range(500, 750),
            _ => Random.Range(750, 1200),
        };
    }

    public void ShowReviveWindow()
    {
        isStarted = false;
        reviveWindow.SetActive(true);
        ReviveCount--;
    }
}
