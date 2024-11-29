using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ModifierHandler : MonoBehaviour
{
    [SerializeField] private List<UI_ModifierRow> rows;

    public void LoadMods(StatModHandler handler)
    {
        foreach (var mod in handler.Mods)
            rows[(int)mod.Key-1].LoadMods(mod.Value);
    }

}
