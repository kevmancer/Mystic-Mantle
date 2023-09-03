using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeOnHover : MonoBehaviour
{
    public int sizeIncreaseAmount = 2;
    public void IncreaseTextSize()
    {
        TMPro.TextMeshProUGUI txt = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        txt.fontSize = txt.fontSize + sizeIncreaseAmount;
    }

    public void DecreaseTextSize()
    {
        TMPro.TextMeshProUGUI txt = gameObject.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        txt.fontSize = txt.fontSize - sizeIncreaseAmount;
    }
}
