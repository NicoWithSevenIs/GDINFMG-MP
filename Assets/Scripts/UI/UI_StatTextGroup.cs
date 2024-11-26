using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_StatTextGroup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI baseStat;
    [SerializeField] private TextMeshProUGUI ev;
    [SerializeField] private TextMeshProUGUI iv;
    [SerializeField] private TextMeshProUGUI total;

    public void SetStats(int baseStat, int ev, int iv, int total)
    {
        this.baseStat.text = baseStat.ToString();
        this.ev.text = ev.ToString();
        this.iv.text = iv.ToString();
        this.total.text = total.ToString();
    }
}
