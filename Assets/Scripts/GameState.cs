using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public Level level;
    public Inventory inventory;

    public ViewLevel viewLevel;
    public ViewInventory viewInventory;
    public EnemyController enemyController;

    private void Start()
    {
        level.Init();
        inventory.Init();

        viewLevel.Init();
        viewInventory.Init();
        enemyController.Init();
    }
}
