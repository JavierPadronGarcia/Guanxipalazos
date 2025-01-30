using TMPro;
using UnityEngine;

public class ButtonSwitch : MonoBehaviour
{
    public GameObject Allies, Enemies, QR, Return;

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
            Return.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "VOLVER";
        }
        else
        {
            Allies.SetActive(true);
            Enemies.SetActive(false);
            QR.SetActive(false);
            Return.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "VER EN AR";
        }
    }
}
