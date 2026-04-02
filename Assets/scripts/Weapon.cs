using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Base Damage")]
    public int basePhysical;
    public int baseMagic;
    public int baseFire;
    public int baseHoly;
    public int baseLightning;

    [Header("scaleDamageStats")]
    public float scalePhysSTR;
    public float scalePhysDEX;
    public float scaleMagINT;
    public float scaleFireINT;
    public float scaleFireFTH;
    public float scaleHolyFTH;
    public float scaleLightFTH;

    [HideInInspector] public int finalPhys;
    [HideInInspector] public int finalMag;
    [HideInInspector] public int finalFire;
    [HideInInspector] public int finalHoly;
    [HideInInspector] public int finalLight;
    public void RefreshFinalDamage(Player p)
    {
        finalPhys = basePhysical + Mathf.RoundToInt(p.dexterity * scalePhysDEX + p.strength * scalePhysSTR);
        finalMag = baseMagic + Mathf.RoundToInt(p.intelligence * scaleMagINT);
        finalFire = baseFire + Mathf.RoundToInt(p.intelligence * scaleFireINT + p.faith * scaleFireFTH);
        finalHoly = baseHoly + Mathf.RoundToInt(p.faith * scaleHolyFTH);
        finalLight = baseLightning + Mathf.RoundToInt(p.faith * scaleLightFTH);
    }
}