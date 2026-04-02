using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName = "Enemy";
    public int health = 50;
    public float xpReward = 25;
    public int coinReward = 10;

    [Header("Resistances")]
    public float resPhys;
    public float resMag;
    public float resFire;
    public float resHoly;
    public float resLight;

    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(Weapon w)
    {
        float totalDmg = 0;
        totalDmg += w.finalPhys * (1f - resPhys);
        totalDmg += w.finalMag * (1f - resMag);
        totalDmg += w.finalFire * (1f - resFire);
        totalDmg += w.finalHoly * (1f - resHoly);
        totalDmg += w.finalLight * (1f - resLight);

        int finalDamageInt = Mathf.Max(1, Mathf.RoundToInt(totalDmg));
        health -= finalDamageInt;

        if (health <= 0) Die();
    }

    private void Die()
    {
        Player p = FindFirstObjectByType<Player>();
        if (p != null)
        {
            p.AddXP(xpReward);
            p.coins += coinReward;
        }
        Destroy(gameObject);
    }

    public void SetHighlight(bool active)
    {
        if (spriteRend == null) spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.color = active ? Color.red : Color.white;
    }
}