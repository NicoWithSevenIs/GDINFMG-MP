using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UI_Pooler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform container;

    [Header("Pooler Paremeters")]
    [SerializeField] private int startingPool;
    [SerializeField] private bool expandable = true;

    private List<GameObject> pool = new();
    private bool hasAvailable { get => pool.Any(t=>!t.activeSelf);  }

    private void Awake()
    {
        for (int i = 0; i < startingPool; i++)
            Create();
    }

    private GameObject Create()
    {
        GameObject go = Instantiate(prefab);
        go.transform.parent = container;
        go.transform.localPosition = Vector3.zero;
        pool.Add(go);
        return go;
    }

    public GameObject TryGet()
    {
        if(hasAvailable)
            return pool.Find(t=>!t.activeSelf);

        if(expandable)
           return Create();

        return null;
    }

}
