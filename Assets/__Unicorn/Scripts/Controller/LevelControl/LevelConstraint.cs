using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using RocketTeam.Sdk.Services.Ads;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelConstraint
{
    [SerializeField] private int[] bonusStep = { 5 };
    
    [SerializeField] private List<int> startIndexes;

    private int startIndex = -1;
    
    public int GetStartIndex()
    {
        if (startIndex != -1) return startIndex;

        startIndex = int.MaxValue;

        foreach (int startIndex in startIndexes)
        {
            this.startIndex = Mathf.Min(this.startIndex, startIndex);
        }
        
        return startIndex;
    }
    
    public int GetStartIndex(LevelType levelType)
    {
        try
        {
            return startIndexes[(int) levelType];
        }
        catch (ArgumentOutOfRangeException)
        {
            int levelTypeCount = Enum.GetValues(typeof(LevelType)).Length;
            if (levelTypeCount > startIndexes.Count)
                throw new ArgumentOutOfRangeException(
                    $"{nameof(startIndexes)} has less values than {nameof(LevelType)}. There should be {levelTypeCount} values.");

            throw;
        }
    }

    public int GetLevelCount()
    {
        startIndex = GetStartIndex();
        return SceneManager.sceneCountInBuildSettings - startIndex + 1;
    }
    
    public int GetLevelCount(LevelType levelType)
    {
        int levelTypeIntValue = (int) levelType;
        if (levelTypeIntValue > startIndexes.Count)
        {
            throw new ArgumentOutOfRangeException($"{nameof(startIndexes)} lacks a value for {levelType}");
        }
        
        int levelStartIndex = startIndexes[levelTypeIntValue];
        
        if (levelStartIndex == -1)
        {
            return -1;
        }

        if (levelTypeIntValue + 1 >= startIndexes.Count)
        {
            return SceneManager.sceneCountInBuildSettings - levelStartIndex;
        }

        return startIndexes[levelTypeIntValue] - levelStartIndex;
    }
    
    public LevelType GetLevelTypeFromBuildIndex(int buildIndex)
    {
        return (LevelType) (buildIndex / GetStartIndex());
    }

    public int GetLevelIndexFromBuildIndex(int buildIndex)
    {
        LevelType levelType = GetLevelTypeFromBuildIndex(buildIndex);
        int startIndex = GetStartIndex(levelType);
        return buildIndex - startIndex;
    }
    
    [Button(ButtonSizes.Medium)]
    public void UpdateLevelOrder()
    {

#if UNITY_EDITOR
        ReArrangeBuildIndex();
#endif
        
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        }
    }

    private bool IsBonusLevel(string sceneName)
    {
        return Regex.IsMatch(sceneName, "Level Bonus \\d");
    }

    private bool IsTutorialLevel(string sceneName)
    {
        return Regex.IsMatch(sceneName, "Level Tutorial \\d");
    }

    private bool IsNormalLevel(string sceneName)
    {
        return Regex.IsMatch(sceneName, "Level \\d");
    }
    
#if UNITY_EDITOR
    private void ReArrangeBuildIndex()
    {
        return;
        var scenes = UnityEditor.EditorBuildSettings.scenes;
        var nonLevelScenes = scenes
            .Where(scene => !Path.GetFileNameWithoutExtension(scene.path).Contains("Level"))
            .ToArray();
    
        scenes = scenes.Where(scene => Path.GetFileNameWithoutExtension(scene.path).Contains("Level"))
            .ToArray();
        
        Array.Sort(scenes,
            (s1, s2) =>
            {
                string s1Name = Path.GetFileNameWithoutExtension(s1.path);
                string s2Name = Path.GetFileNameWithoutExtension(s2.path);

                int point1 =
                    IsBonusLevel(s1Name)    ? 1 :
                    IsTutorialLevel(s1Name) ? 2 :
                    IsNormalLevel(s1Name)   ? 3 : 0;
                
                int point2 =
                    IsBonusLevel(s2Name)    ? 1 :
                    IsTutorialLevel(s2Name) ? 2 :
                    IsNormalLevel(s2Name)   ? 3 : 0;

                if (point1 == point2)
                {
                    int num1 = int.Parse(Regex.Replace(s1Name, @"[^0-9]", string.Empty));
                    int num2 = int.Parse(Regex.Replace(s2Name, @"[^0-9]", string.Empty));
                    
                    return num1 - num2;

                }
                else
                {
                    return point1 - point2;
                }
            }
            );

        UnityEditor.EditorBuildSettings.scenes = nonLevelScenes.Concat(scenes).ToArray();
    }
#endif
}
