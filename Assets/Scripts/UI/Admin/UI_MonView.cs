using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MonView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI idText;
    [SerializeField] private TextMeshProUGUI noAndNameText;
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
        monIcon.sprite = WebAPIManager.Instance.GetSprite($"{BattleManager.BASE_URL}{mon.spriteID}.png");

        type1Color.color = TypeColor.GetColor(mon.type1);
        type1Text.text = mon.type1.ToString();

        type2.SetActive(mon.type2 != null);

        if (type2.activeSelf)
        {
            type2Color.color = TypeColor.GetColor(mon.type2.Value);
            type2text.text = mon.type2.Value.ToString();
        }
    }

}
