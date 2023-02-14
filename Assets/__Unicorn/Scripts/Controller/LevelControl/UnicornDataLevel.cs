using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RocketTeam.Sdk.Services.Ads;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Unicorn
{
    [Serializable]
    public class UnicornDataLevel : IDataLevel, ILevelInfo
    {
        [JsonProperty] private LevelType levelType = LevelType.Normal;
        [JsonProperty] private int displayLevel = 1;
        [JsonProperty] private bool isKeyCollected = false;
        [JsonProperty] private Dictionary<LevelType, LevelTypeInfo> levelTypesIndex;

        private List<int> loopLevels = new List<int>();
        private LevelConstraint levelConstraint;

        public LevelConstraint LevelConstraint
        {
            get
            {
                if (levelConstraint == null)
                {
                    Debug.LogError(nameof(levelConstraint) + " is not set, using default value!");
                    levelConstraint = new LevelConstraint();
                    levelConstraint.UpdateLevelOrder();
                }

                return levelConstraint;
            }
            set => levelConstraint = value;
        }

        public LevelType LevelType => levelType;
        public int DisplayLevel => displayLevel;

        [JsonIgnore]
        public bool IsKeyCollected
        {
            get => isKeyCollected;
            set
            {
                isKeyCollected = value;
                Save();
            }
        }

        [JsonIgnore]
        private Dictionary<LevelType, LevelTypeInfo> LevelTypesIndex
        {
            get
            {
                if (levelTypesIndex.Count < Enum.GetValues(typeof(LevelType)).Length)
                {
                    foreach (LevelType levelType in Enum.GetValues(typeof(LevelType)))
                    {
                        if (levelTypesIndex.ContainsKey(levelType)) continue;
                        levelTypesIndex.Add(levelType, new LevelTypeInfo(levelType));
                    }
                }
                return levelTypesIndex;
            }
            set => levelTypesIndex = value;
        }

        public UnicornDataLevel(LevelConstraint levelConstraint)
        {
            levelTypesIndex = new Dictionary<LevelType, LevelTypeInfo>();
            foreach (LevelType value in Enum.GetValues(typeof(LevelType)))
            {
                levelTypesIndex.Add(value, new LevelTypeInfo(value));
            }
        }
    
        public override string ToString()
        {
            return $"level: {GetCurrentLevel()}, " +
                   $"{nameof(levelType)}: {levelType}";
        }

        public void SetLevel(LevelType levelType, int level)
        {
            LevelTypesIndex[levelType].SetLevel(level, LevelConstraint);
            this.levelType = levelType;
            Save();
        }

        public void SetLevel(int buildIndex)
        {
            // set level by buildIndex
            // TODO
            Debug.LogError(new NotImplementedException());
            return;
            Save();
        }

        public void SetStar(int starCount)
        {
            Save();
        }

        public int GetBuildIndex()
        {
            int startIndex = LevelConstraint.GetStartIndex(levelType);
            if (startIndex < 1)
            {
                levelType = GetNextLevelType();
                startIndex = LevelConstraint.GetStartIndex(levelType);

                if (startIndex < 1)
                {
                    // No valid scene found!
                    return -1;
                }
            }
        
            int level = GetCurrentLevel();

            return startIndex + level - 1;
        }
    
        public int GetCurrentLevel() => LevelTypesIndex[levelType].CurrentLevel;

        public void IncreaseLevel()
        {
            displayLevel++;
            IsKeyCollected = false;
            
            LevelTypesIndex[levelType].IncreaseLevel(LevelConstraint);

            levelType = GetNextLevelType();        
            Save();
        }
    
        private LevelType GetNextLevelType()
        {
            int levelTypeIntValue = (int) levelType;
            if (++levelTypeIntValue >= Enum.GetValues(typeof(LevelType)).Length)
            {
                levelTypeIntValue = 0;
            }

            return (LevelType) levelTypeIntValue;
        }

        public void Save()
        {
            PlayerDataManager.SetDataLevel(this);
        }
    
    }
}