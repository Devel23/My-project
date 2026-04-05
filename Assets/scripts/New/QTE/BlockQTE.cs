using UnityEngine;
using UnityEngine.InputSystem;

public class BlockQTE : MonoBehaviour
{
    [SerializeField] private GameObject m_VisualAreaQTE;
    [SerializeField] private GameObject m_VisualSwordQTE;
    [SerializeField] private GameObject m_visual;
    [SerializeField] private Transform m_swordIndecator;
    [SerializeField] private Transform m_target;

    [SerializeField] private BlockData m_blockData; //ser for test
    private float m_rangeQTE;
    private float m_speed;
    private bool isQTE;
    private Vector2 startPos;

    private void Awake()
    {
        startPos = m_VisualSwordQTE.transform.position;
    }
    private void Start()
    {
        SetQTE(m_blockData);
        Debug.Log($"Press + {Vector2.Distance(m_swordIndecator.position, m_target.position)} " +
                   $"\n{(Vector2.Distance(m_swordIndecator.position, m_target.position)) / 33.3f}");
    }
    private void SetQTE(BlockData blockData)
    {
        m_blockData = blockData;
        m_speed = blockData.speed;
        m_rangeQTE = blockData.Radius;

        m_VisualSwordQTE.transform.position = startPos;
        m_visual.SetActive(true);
        m_VisualAreaQTE.transform.localScale = new Vector2(m_rangeQTE, m_rangeQTE);

        isQTE = true;
    }

    private void Update()
    {
        MoveQTE();
        TryQTE();
    }

    private void MoveQTE()
    {
        if (isQTE)
        {
            if (Vector2.Distance(m_swordIndecator.position, m_target.position) <= 0.01f)
            {
                Debug.Log("Достигнута цель!");
                EndQTE();
                LooseQTE();
                return;
                
            }

            m_swordIndecator.position = Vector2.MoveTowards(
                m_swordIndecator.position,
                m_target.position,
                m_speed * Time.deltaTime);
        }
    }

    public void TryQTE()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {

            Debug.Log("Press");

            if ((Vector2.Distance(m_swordIndecator.position, m_target.position)) / 33.3f < m_rangeQTE)
            {
                EndQTE();
                Debug.Log($"Press + {Vector2.Distance(m_swordIndecator.position, m_target.position)} " +
                    $"\n{(Vector2.Distance(m_swordIndecator.position, m_target.position)) / 33.3f}" );
                WinQTE();
                return;
            }
        }
    }

    public void EndQTE()
    {
        isQTE = false;
    }

    private void WinQTE()
    {
        Debug.Log("Win");
    }

    private void LooseQTE()
    {
        Debug.Log("Loose");
    }
}
