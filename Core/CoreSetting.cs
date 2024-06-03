using System;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using CKB.Utilities;

/// <summary>
/// Contains all system game data.
/// A bit a lot of responsibilities you might say, but creating data class
/// for every single system it is not so handful too
/// </summary>
[CreateAssetMenu(fileName = "CoreSettings", menuName = "SO/CoreSettings")]
public class CoreSettings : SingletonData<CoreSettings>
{
    public ProductionData production;

    [Header("Scenes")]
    public float nextLevelLoadDelay;
    public bool clearTweensOnSceneExit = true;

    [Header("Code")]
    public bool lazyRuntimeCreationEnabled = true;
    public bool raycastDebugPauseEnabled;
    public bool allocationEnabled = true;
    public bool objectInteractorValidation = true;

    [Tooltip("Use only if you have a tons of game states, subscribers")]
    public bool stateMachineOptimizedMode;

    [Header("UI")]
    public FlyingUISettings flyingUISettings;
    public int safeZoneOffset = -90;

#if UNITY_EDITOR

    private void OnValidate()
    {
        //if (defaultBunchData == null)
        //    defaultBunchData = GetAllInstances<BunchFlyingUIData>()[0];
    }

    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
        }

        return a;

    }

#endif
}

[Serializable]
public class FlyingUISettings
{
    public Vector2 claimCount;
    public float updateMoneyDbDelay;

    [Space]

    public float startScale;
    public float endScale;
    public float animationsDuration;
    public Ease animationEase;

    [Space]

    public Vector2 rainAmplitude;
}
