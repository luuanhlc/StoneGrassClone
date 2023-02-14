using CityBuilder.GameEntities;
using CityBuilder.GameModes;
using CityBuilder.UI;
using CityBuilder.Data;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CityBuilder.Controllers
{
    public class GameController : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField]
        private GameObject buildingPrefab;
        [SerializeField]
        private GameObject cataloguePrefab;
        [SerializeField]
        private GameObject constructionBarPrefab;
        [SerializeField]
        private GameObject productionButtonBarPrefab;
        [SerializeField]
        private GameModeManager gameModeManager;
        [Header("Scene Objects")]
        [SerializeField]
        private Player gameplayer;
        [SerializeField]
        private CityGrid gameGrid;
        [SerializeField]
        private UIResourceObserver resourceObserver;
        private BaseGameMode currentGameMode;
        private Camera camera;
        [SerializeField]
        private Canvas mainCanvas;

        public CityBuildingData[] availableBuildings;

        public static GameController Ins;
        public BaseGameMode CurrentGameMode
        {
            get { return currentGameMode; }
        }

        public Camera GameCamera
        {
            get { return camera; }
        }

        public GameObject ConstructionBarPrefab
        {
            get { return constructionBarPrefab; }
        }

        public GameObject ProductionButtonBarPrefab
        {
            get { return productionButtonBarPrefab; }
        }

        public GameObject BuildingPrefab
        {
            get { return buildingPrefab; }
        }

        public Canvas MainCanvas
        {
            get { return mainCanvas; }
        }
        public GameObject CataloguePrefab
        {
            get { return cataloguePrefab; }
        }
        private void Awake()
        {
            camera = Camera.main;
            Ins = this;
        }

        private void Start()
        {
            resourceObserver.Initialize(this);
            SetBuildMode();
        }

        public void SetRegularMode()
        {
            currentGameMode = gameModeManager.SetGameMode(GameMode.RegularMode);
            currentGameMode.Initialize(this);
        }

        public void SetBuildMode()
        {
            currentGameMode = gameModeManager.SetGameMode(GameMode.BuildMode);
            currentGameMode.Initialize(this);
        }

        public Player GamePlayer
        {
            get { return gameplayer; }
        }

        public CityGrid GameGrid
        {
            get { return gameGrid; }
        }


    }
}

