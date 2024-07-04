using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RC
{
    public class DataPersistanceController : MonoBehaviour
    {
        [Header("File Storage")]
        [SerializeField] string fileName;

        [SerializeField]
        public GameData gameData;

        public static DataPersistanceController instance;

        private FileDataController fileDataController;

        public Image test;

        public DataPersistanceController()
        {
            instance = this;
        }

        public void Init()
        {
            fileDataController = new FileDataController(Application.persistentDataPath, fileName);
            LoadGame();
            StartCoroutine(SaveEveryThirtySeconds());
        }

        IEnumerator SaveEveryThirtySeconds()
        {
            yield return new WaitForSeconds(30);
            SaveGame();
            StartCoroutine(SaveEveryThirtySeconds());
        }

        public void SaveGame()
        {
            GameData newGameData = new GameData();

            newGameData.items = ItemDatabase.instance.Items;

            newGameData.heroesId = Inventory.instance.HeroesId;
            newGameData.weaponsId = Inventory.instance.WeaponsId;
            newGameData.helmetsId = Inventory.instance.HelmetsId;
            newGameData.chestplatesId = Inventory.instance.ChestplatesId;
            newGameData.leggingsId = Inventory.instance.LeggingsId;
            newGameData.bootsId = Inventory.instance.BootsId;

            if (Inventory.instance.GetHeroInField() != null)
            {
                newGameData.heroInField = Inventory.instance.GetHeroInField();
                newGameData.heroInFieldLocalId = HeroController.instance.Hero.localHeroId;
            }
            else
            {
                newGameData.heroInField = null;
                newGameData.heroInFieldLocalId = null;
            }

            newGameData.gold = Inventory.instance.Gold;

            newGameData.xp = Level.instance.CurrentXp;

            if (EnemyController.instance.SpawnedEnemy != null)
            {
                newGameData.enemy = EnemyController.instance.SpawnedEnemy.CharacterItem as IEnemyItem;
            }
            else
            {
                newGameData.enemy = null;
            }

            newGameData.enemiesPointsMultiplier = EnemyController.instance.EnemiesPointsMultiplier;

            gameData = newGameData;
            fileDataController.Save(gameData);
        }

        public void LoadGame()
        {
            gameData = fileDataController.Load();
            if (gameData == null)
            {
                SaveGame();
                return;
            }
            else
            {
                ItemDatabase.instance.Items = gameData.items;
                ItemDatabase.instance.ClearStartingItemsSave();

                Inventory.instance.HeroesId = gameData.heroesId;
                Inventory.instance.WeaponsId = gameData.weaponsId;
                Inventory.instance.HelmetsId = gameData.helmetsId;
                Inventory.instance.ChestplatesId = gameData.chestplatesId;
                Inventory.instance.LeggingsId = gameData.leggingsId;
                Inventory.instance.BootsId = gameData.bootsId;

                if (gameData.heroInField != null)
                {
                    Inventory.instance.SetHeroInField(gameData.heroInField as HeroItem, gameData.heroInFieldLocalId);
                }

                Inventory.instance.Gold = gameData.gold;

                Level.instance.CurrentXp = gameData.xp;

                if (gameData.enemy != null)
                {
                    EnemyController.instance.SpawnEnemyFromSave(gameData.enemy);
                }

                gameData.enemiesPointsMultiplier = EnemyController.instance.EnemiesPointsMultiplier;
            }
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void CloseEvent()
        {
            SaveGame();
            // test.color = Color.red;
        }
    }
}
