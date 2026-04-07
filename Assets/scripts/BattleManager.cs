using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    public Player player;

    [Header("Spawn Settings")]
    public GameObject[] enemyPool;
    public Transform[] spawnPoints;

    [Header("QTE Settings")]
    public BlockQTE blockQTE;

    private List<Enemy> activeEnemies = new();
    private int currentTargetIndex = 0;
    public Enemy selectedEnemy;
    void Start()
    {
        if (player == null) player = FindFirstObjectByType<Player>();
        SpawnEnemies();
    }

    void Update()
    {
        HandleBattleInput();
    }

    void HandleBattleInput()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame)
            ChangeTarget(-1);
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame)
            ChangeTarget(1);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            OnAttackButtonClick();
    }

    void SpawnEnemies()
    {
        activeEnemies.Clear();
        foreach (var point in spawnPoints)
        {
            GameObject randomPrefab = enemyPool[Random.Range(0, enemyPool.Length)];
            GameObject newEnemy = Instantiate(randomPrefab, point.position, Quaternion.identity);
            Enemy e = newEnemy.GetComponent<Enemy>();
            if (e != null) activeEnemies.Add(e);
        }
        UpdateSelection();
    }

    void ChangeTarget(int direction)
    {
        activeEnemies.RemoveAll(e => e == null);
        if (activeEnemies.Count == 0) return;
        currentTargetIndex = (currentTargetIndex + direction + activeEnemies.Count) % activeEnemies.Count;
        UpdateSelection();
    }

    void UpdateSelection()
    {
        activeEnemies.RemoveAll(e => e == null);
        if (activeEnemies.Count == 0)
        {
            selectedEnemy = null;
            SpawnEnemies();
            return;
        }
        currentTargetIndex = Mathf.Clamp(currentTargetIndex, 0, activeEnemies.Count - 1);
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            bool isSelected = (i == currentTargetIndex);
            activeEnemies[i].SetHighlight(isSelected);
            if (isSelected)
            {
                selectedEnemy = activeEnemies[i];
                player.targetEnemy = selectedEnemy;
            }
        }
    }

    public void OnAttackButtonClick()
    {
        if (selectedEnemy != null)
        {
            player.weapon.RefreshFinalDamage(player);
            selectedEnemy.TakeDamage(player.weapon);

            Enemy currentAttacker = selectedEnemy;
            Invoke("UpdateSelection", 0.1f);

            if (currentAttacker != null && currentAttacker.health > 0)
            {
                StartCoroutine(EnemyTurnRoutine(currentAttacker));
            }
        }
    }

    IEnumerator EnemyTurnRoutine(Enemy attacker)
    {
        yield return new WaitForSeconds(1.0f);
        if (attacker != null && blockQTE != null && attacker.qteSettings != null)
        {
            blockQTE.ActivateBlock(attacker.qteSettings);
        }
    }
}