using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "TopDown/Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public bool isAutomatic;
    public float fireRate;
    public float damage;
    public int clipSize;
    public int maxAmmo;
    public GameObject projectilePrefab;
}