using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public GameObject Allies, Enemies, QR;

    public void Switch()
    {
        if (Allies.activeSelf)
        {
            Allies.SetActive(false);
            Enemies.SetActive(true);
        }
        else
        {
            Allies.SetActive(true);
            Enemies.SetActive(false);
        }
    }
    public void QRSwitch()
    {
        if (!QR.activeSelf)
        {
            Allies.SetActive(false);
            Enemies.SetActive(false);
            QR.SetActive(true);
        }
        else
        {
            Allies.SetActive(true);
            Enemies.SetActive(false);
            QR.SetActive(false);
        }
    }
}
