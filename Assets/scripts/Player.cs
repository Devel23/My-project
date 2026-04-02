using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health = 100; // ты видишь, а скриптьы не видят
    public Weapon weapon;
    public Enemy targetEnemy;

    [Header("Base Stats")]
    public int strengthBase = 10;
    public int dexterityBase = 5;
    public int intelligenceBase = 0;
    public int faithBase = 0;
    public int luckBase = 5;

    [Header("Growth Stats (Per Level)")]
    public int strengthGrowth = 2;
    public int dexterityGrowth = 1;
    public int intelligenceGrowth = 0;
    public int faithGrowth = 0;
    public int luckGrowth = 1;

    [Header("Current Stats")]
    public int strength;
    public int dexterity;
    public int intelligence;
    public int faith;
    public int luck;

    [Header("Progression")]
    public int level = 1;
    public int coins = 0;
    public float currentXP = 0;
    public float xpToNextLevel = 100;

    private void Awake()
    {
        strength = strengthBase;
        dexterity = dexterityBase;
        intelligence = intelligenceBase;
        faith = faithBase;
        luck = luckBase;
    }

    public void AddXP(float amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        level++;
        xpToNextLevel = Mathf.Round(xpToNextLevel * 1.15f);

        strength += strengthGrowth;
        dexterity += dexterityGrowth;
        intelligence += intelligenceGrowth;
        faith += faithGrowth;
        luck += luckGrowth;
    }
}