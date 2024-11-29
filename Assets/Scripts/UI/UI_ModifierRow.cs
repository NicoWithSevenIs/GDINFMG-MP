using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ModifierRow : MonoBehaviour
{
    [SerializeField] private CanvasGroup ups;
    [SerializeField] private CanvasGroup downs;

    public void SetUIActive(CanvasGroup group, bool active)
    {
        group.alpha = active ? 1 : 0;
        group.blocksRaycasts = active;
        group.interactable = active;
    }

    private void Awake()
    {
        ups.gameObject.SetActive(true); 
        downs.gameObject.SetActive(true);

        SetUIActive(ups, false);
        SetUIActive(downs, false);
    }

    public void LoadMods(int modCount)
    {

        foreach(Transform u in ups.transform)
            u.gameObject.SetActive(false);

        foreach (Transform d in downs.transform)
            d.gameObject.SetActive(false);

        if (modCount == 0)
            return;

        modCount = Mathf.Clamp(modCount, -6, 6);

        SetUIActive(ups, modCount > 0);
        SetUIActive(downs, modCount < 0);

        Transform t = modCount > 0 ? ups.transform : modCount < 0 ? downs.transform : null;
        float s = Mathf.Abs(modCount);

        for (int i = 0; i < 6; i++)
            t.GetChild(i).gameObject.SetActive(i <= s-1);
        
    }

}
