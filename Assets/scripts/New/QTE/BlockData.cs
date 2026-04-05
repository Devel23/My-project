using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "ScriptableObjects/QTE/Block QTE", order = 1)]
public class BlockData : ScriptableObject
{
    [SerializeField][Range(1, 3)] private float m_rangeQTE;
    [SerializeField] private float m_duration;

    public float Radius => m_rangeQTE;
    public float Duration => m_duration; 
}
