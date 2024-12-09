using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshPro noAndNameText;
    [SerializeField] private Image monIcon;
    [SerializeField] private Image type1Color;
    [SerializeField] private TextMeshProUGUI type1Text;
    [SerializeField] private GameObject type2;
    [SerializeField] private Image type2Color;
    [SerializeField] private TextMeshProUGUI type2text;

    public void AssignMon(int index, Pokemon_Data mon)
    {
        idText.text = $"[ {index} ]";
        noAndNameText.text = $"#{mon.spriteID} {mon.name}";

    }

}
