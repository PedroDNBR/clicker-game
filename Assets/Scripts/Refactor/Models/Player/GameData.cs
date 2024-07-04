using System;
using System.Collections.Generic;
using UnityEngine;

namespace RC
{
    [System.Serializable]
    public class GameData
    {
        public int gold = 0;
        public int xp = 0;

        /*
         * ItemDatabase
         */
        [SerializeField] public Dictionary<string, IBaseItem> items = new Dictionary<string, IBaseItem>();

        /*
         * Inventory
         */
        [SerializeField] public Dictionary<string, InventoryItem> heroesId = new Dictionary<string, InventoryItem>();
        [SerializeField] public Dictionary<string, InventoryItem> weaponsId = new Dictionary<string, InventoryItem>();
        [SerializeField] public Dictionary<string, InventoryItem> helmetsId = new Dictionary<string, InventoryItem>();
        [SerializeField] public Dictionary<string, InventoryItem> chestplatesId = new Dictionary<string, InventoryItem>();
        [SerializeField] public Dictionary<string, InventoryItem> leggingsId = new Dictionary<string, InventoryItem>();
        [SerializeField] public Dictionary<string, InventoryItem> bootsId = new Dictionary<string, InventoryItem>();

        public IHeroItem heroInField = null;
        public string heroInFieldLocalId = null;

        public IEnemyItem enemy = null;

        public float enemiesPointsMultiplier;


        public GameData()
        {

        }
    }
}

