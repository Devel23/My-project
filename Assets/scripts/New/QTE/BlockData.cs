using UnityEngine;

[CreateAssetMenu(fileName = "Block Data", menuName = "ScriptableObjects/QTE/Block QTE", order = 1)]
public class BlockData : ScriptableObject
{
    [SerializeField][Range(1, 3)] private float m_rangeQTE;
    [SerializeField] private float m_speed;
    [SerializeField] private string m_Name;

    public float Radius => m_rangeQTE;
    public float speed => m_speed; 
    public string Name => m_Name;
}
