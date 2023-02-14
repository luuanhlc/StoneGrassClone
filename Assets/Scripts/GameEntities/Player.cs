using CityBuilder.Controllers;
using CityBuilder.GameModes;
using CityBuilder.Resources;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.GameEntities
{
    /// <summary>
    /// City builder player, saves buildings and resources
    /// also execute events when their attributes have are modified
    /// </summary>
    public class Player : MonoBehaviour
    {
        private BaseGameMode gameMode;
        /// <summary>
        /// List of city buildings owned by the player
        /// </summary>
        private List<CityBuilding> ownedBuildings;
        /// <summary>
        /// Gold resource of the player
        /// </summary>
        [SerializeField]
        private ResourceAmount goldResource;
        /// <summary>
        /// Wood resource of the player
        /// </summary>
        private UnityAction<int> onGoldResourceModified;
        /// <summary>
        /// Wood resource modify event
        /// </summary>
        private UnityAction<CityBuilding> onBuildingAdded;

        public BaseGameMode GameMode
        {
            get { return gameMode; }
        }

        /// <summary>
        /// Accessor for the total gold amount
        /// </summary>
        public int TotalGold
        {
            get { return PlayerDataManager.GetTotalStar(); }
        }
        /// <summary>
        /// Accessor for the total wood amount
        /// </summary>
        /// <summary>
        /// Accessor for the total steel amount
        /// </summary>
        /// <summary>
        /// Accessor for the owned buildings list
        /// </summary>
        public List<CityBuilding> OwnedBuildings
        {
            get { return ownedBuildings; }
        }

        
        public void AddOnGoldModifyAction(UnityAction<int> action)
        {
            onGoldResourceModified += action;
            PlayerDataManager.GetTotalStar();
            Debug.Log("action " + action);
        }


        public void AddOnBuildingAddedAction(UnityAction<CityBuilding> action)
        {
            onBuildingAdded += action;
        }

        public void RemoveOnGoldModifyAction(UnityAction<int> action)
        {
            onGoldResourceModified -= action;
        }

        public void RemoveOnBuildingAddedAction(UnityAction<CityBuilding> action)
        {
            onBuildingAdded -= action;
        }

        /// <summary>
        /// Initialize components
        /// </summary>
        private void Awake()
        {
            ownedBuildings = new List<CityBuilding>();
            
        }

        public void SetGameMode(BaseGameMode gameMode)
        {
            this.gameMode = gameMode;
        }

        /// <summary>
        /// Add a building to the owned buildings list
        /// and executes the building added event if there is any
        /// </summary>
        /// <param name="building">Building to be added to the owned buildings</param>
        public void AddBuilding(CityBuilding building)
        {
            
            ownedBuildings.Add(building);
            SetBuildingCost(building.Cost);
            onBuildingAdded?.Invoke(building);
        }

        public int GetResourceAmount(ResourceType type)
        {
            int amount = 0;
            switch (type)
            {
                case ResourceType.Gold:
                    amount = TotalGold;
                    break;
            }
            return amount;
        }

        private void SetBuildingCost(List<ResourceAmount> cost)
        {
            for(int i = 0; i < cost.Count; i++)
            {
                RemoveResource(cost[i].Type, cost[i].Amount);
            } // end if
        }
        /// <summary>
        /// Adds a specified amount to a resource
        /// </summary>
        /// <param name="type">Resource type to be added</param>
        /// <param name="amount">Amount to be added</param>
        public void AddResource(ResourceType type, int amount)
        {
            switch(type)
            {
                case ResourceType.Gold:
                    AddGold(amount);
                    break;
            } // end switch
        }
        /// <summary>
        /// Removes a resource given a type and an amount
        /// </summary>
        /// <param name="type">Resource type to be removed</param>
        /// <param name="amount">Amount to be removed</param>
        public void RemoveResource(ResourceType type, int amount)
        {
            switch (type)
            {
                case ResourceType.Gold:
                    RemoveGold(amount);
                    break;
            }
        }
        /// <summary>
        /// Adds a gold amount to the player and executes the gold resource modify event
        /// if there is any
        /// </summary>
        /// <param name="amount">Gold amount to be added</param>
        private void AddGold(int amount)
        {
            goldResource.Amount += amount;
            onGoldResourceModified?.Invoke(goldResource.Amount);
        }
        /// <summary>
        /// Removes a gold amount if the amount is lesser or equal than the player gold
        /// and executes the gold resource modify event if there is any
        /// </summary>
        /// <param name="amount">Gold amount to be removed</param>
        private void RemoveGold(int amount)
        {
            if(goldResource.Amount >= amount)
            {
                goldResource.Amount -= amount;
                PlayerDataManager.SetTotalStar(Mathf.Max(PlayerDataManager.GetTotalStar() - amount, 0));
                UiTop.Ins.UpdateUi(2);
                onGoldResourceModified?.Invoke(goldResource.Amount);
            } // end if
        }
    }
}


