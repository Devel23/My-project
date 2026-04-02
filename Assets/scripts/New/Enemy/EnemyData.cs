using UnityEngine;

public sealed class EnemyData : MonoBehaviour
{
    [SerializeField] private int m_attackDamage;
    [SerializeField][Min(0)] private float m_health;

    public int Damage => m_attackDamage;
    public float Health => m_health;
}
