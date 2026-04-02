using Assets.Scripts.Entities;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IUnit
{
    [SerializeField] private HealthComponent m_health;
    [SerializeField] private EnemyData m_enemyData;

    private Player m_player;

    private void OnEnable()
    {
        m_health.Died += Die;//popa
    }

    private void OnDisable()
    {
        m_health.Died -= Die;
    }

    public void Instanstate(Player player)
    {
        m_player = player;
        m_health.Initialize(m_enemyData.Health);
    }
    public void Action()
    {
        throw new System.NotImplementedException();
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
