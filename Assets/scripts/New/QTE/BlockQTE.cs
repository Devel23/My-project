using UnityEngine;
using UnityEngine.InputSystem;

public class BlockQTE : MonoBehaviour
{
    [Header("UI parametrs")]
    [SerializeField] private RectTransform m_areaGreenCircle;
    [SerializeField] private RectTransform m_sword;
    [SerializeField] private RectTransform m_targetCenter;
    [SerializeField] private GameObject m_visualQTE;

    [SerializeField] private BlockData m_blockData; //ser for test
    
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
    }
    private void Start()
    {
        SetQTE(m_blockData);
    }
    private void SetQTE(BlockData blockData)
    {
        m_visualQTE.SetActive(true);

        if (blockData != null)
            m_areaGreenCircle.localScale = new Vector2(blockData.Radius, blockData.Radius);

        float originalGreenWidth = m_areaGreenCircle.rect.width;
        float greenScale = m_areaGreenCircle.localScale.x;
        m_greenRadius = (originalGreenWidth * greenScale) * 0.5f;
        m_redRadius = m_targetCenter.rect.width * 0.5f;

        m_sword.anchoredPosition = startPos;
        m_currentFlyTime = 0f;
        m_flyDuration = blockData.Duration;
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
            float currentDistance = Vector2.Distance(m_sword.anchoredPosition, m_targetCenter.anchoredPosition);

            m_currentFlyTime += Time.deltaTime;
            float t = Mathf.Clamp01(m_currentFlyTime / m_flyDuration);

            Vector2 newPos = Vector2.Lerp(startPos, targetPos, t);
            m_sword.anchoredPosition = newPos;

            if (currentDistance <= m_redRadius || t >= 1f)
            {
                Debug.Log("Меч достиг центра — провал!");
                LooseQTE();
                EndQTE();
            }
        }
    }

    public void TryQTE()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {

            float currentDistance = Vector2.Distance(m_sword.anchoredPosition, m_targetCenter.anchoredPosition);

            Debug.Log($"Нажатие! Расстояние до центра: {currentDistance} (зелёный: {m_greenRadius})");

            if (currentDistance <= m_greenRadius)
            {
                // Попадание в зелёную зону — парирование
                Debug.Log("✅ Успех: парирование в зелёной зоне!");
                EndQTE();
            }
            else
            {
                // Нажатие слишком рано — меч ещё далеко
                Debug.Log("❌ Провал: нажатие мимо зон (слишком рано)!");
                EndQTE();
            }
        }
    }

    public void EndQTE()
    {
        isQTE = false;
        m_visualQTE.SetActive(false);
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
