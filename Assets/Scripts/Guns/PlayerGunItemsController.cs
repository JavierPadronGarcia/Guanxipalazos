using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmaMejoras
{
    public float fireDistanceIncrease;
    public float fireRateIncrease;
    public int gunDamageIncrease;
}

public class PlayerGunItemsController : MonoBehaviour
{
    [Header("Lista de Armas")]
    public List<GameObject> gunList = new List<GameObject>();

    public Dictionary<string, GameObject> guns = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> activeGuns = new Dictionary<string, GameObject>();

    void Awake()
    {
        foreach (GameObject gun in gunList)
        {
            Gun gunScript = gun.GetComponent<Gun>();
            if (gun != null && !guns.ContainsKey(gunScript.gunName))
            {
                guns.Add(gunScript.gunName, gun);
            }
        }
    }

    public void ActivateGun(string gunName)
    {
        Debug.Log("Activando arma: " + guns.ContainsKey(gunName) + " " + !activeGuns.ContainsKey(gunName));
        if (guns.ContainsKey(gunName) && !activeGuns.ContainsKey(gunName))
        {
            guns.TryGetValue(gunName, out GameObject gun);
            if (gun != null)
            {
                activeGuns.Add(gunName, gun);
                gun.SetActive(true);
            }
        }
    }

    public void UpgradeGun(string gunName, ArmaMejoras gunUpgrades)
    {
        if (activeGuns.ContainsKey(gunName))
        {
            GameObject gun = activeGuns[gunName];
            Gun gunScript = gun.GetComponent<Gun>();

            if (gunScript != null)
            {
                gunScript.gunDamage += gunUpgrades.gunDamageIncrease;
                gunScript.fireRate += gunUpgrades.fireDistanceIncrease;
                gunScript.fireDistance += gunUpgrades.fireDistanceIncrease;
            }
        }
    }
}
