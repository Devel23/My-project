using UnityEngine;
using UnityEngine.InputSystem;

public class BlockQTE : MonoBehaviour
{
    [Header("UI Parameters")]
    [SerializeField] private RectTransform m_areaGreenCircle;
    [SerializeField] private RectTransform m_sword;
    [SerializeField] private RectTransform m_targetCenter;
    [SerializeField] private GameObject m_visualQTE;

    private BlockData m_blockData;
    private float m_flyDuration;
    private float m_greenRadius;
    private float m_redRadius;
    private float m_currentFlyTime;
    private bool isQTE;
    private Vector2 startPos;
    private Vector2 targetPos;

    private void Awake()
    {
        startPos = m_sword.anchoredPosition;
        targetPos = m_targetCenter.anchoredPosition;
        m_visualQTE.SetActive(false);
    }

    public void ActivateBlock(BlockData data)
    {
        if (data == null) return;
        m_blockData = data;
        m_visualQTE.SetActive(true);

        m_flyDuration = data.Duration;
        m_currentFlyTime = 0;
        m_sword.anchoredPosition = startPos;

        m_areaGreenCircle.localScale = new Vector2(data.Radius, data.Radius);
        float originalGreenWidth = m_areaGreenCircle.rect.width;
        m_greenRadius = (originalGreenWidth * data.Radius) * 0.5f;
        m_redRadius = m_targetCenter.rect.width * 0.5f;

        isQTE = true;
    }

    private void Update()
    {
        if (!isQTE) return;

        HandleMovement();
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryQTE();
        }
    }

    private void HandleMovement()
    {
        float currentDistance = Vector2.Distance(m_sword.anchoredPosition, m_targetCenter.anchoredPosition);
        m_currentFlyTime += Time.deltaTime;
        float t = Mathf.Clamp01(m_currentFlyTime / m_flyDuration);
        m_sword.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

        if (currentDistance <= m_redRadius || t >= 1f)
        {
            LooseQTE();
            EndQTE();
        }
    }

    private void TryQTE()
    {
        float currentDistance = Vector2.Distance(m_sword.anchoredPosition, m_targetCenter.anchoredPosition);
        if (currentDistance <= m_greenRadius)
        {
            WinQTE();
        }
        else
        {
            LooseQTE();
        }
        EndQTE();
    }

    private void WinQTE() => Debug.Log("✅ Успех: Удар заблокирован!");
    private void LooseQTE() => Debug.Log("❌ Провал: Игрок получил урон!");

    private void EndQTE()
    {
        isQTE = false;
        m_visualQTE.SetActive(false);
    }
}