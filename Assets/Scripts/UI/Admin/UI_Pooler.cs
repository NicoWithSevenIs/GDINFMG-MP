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
        go.transform.SetParent(container);
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = Vector3.one;
        go.SetActive(false);
        pool.Add(go);
        return go;
    }

    public GameObject TryGet()
    {
        GameObject poolable = null;

        if(hasAvailable)
           poolable = pool.Find(t=>!t.activeSelf);

        if(expandable)
           poolable =  Create();

        poolable?.SetActive(true);

        return poolable;
    }

    public List<GameObject> TryGetBatch(int amount)
    {
        List<GameObject> l = new();
        for(int i =0; i < amount; i++)
        {
            GameObject go = TryGet();
            if (go == null)
                break;
            l.Add(go);
        }
        return l;
    }
}
