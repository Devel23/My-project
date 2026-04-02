using UnityEngine;

public sealed class EnemyData : MonoBehaviour
{
    [SerializeField] private int m_attackDamage;

    public int Damage => m_attackDamage;
}
