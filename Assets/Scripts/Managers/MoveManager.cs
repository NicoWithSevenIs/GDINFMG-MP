using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class MoveManager: MonoBehaviour
{
    #region Singleton
    public static MoveManager instance { get; private set; } = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);

        Build();
    }
    #endregion

    private Dictionary<int, Move> moveDict = new();

    //Debugging

    [Serializable]
    private class Debugger
    {
        public int ID;
        public string Name;
    }
    [SerializeField] private List<Debugger> debuggerList = new();

    public MoveData RetrieveMoveData(int key) => moveDict.GetValueOrDefault(key).Data;
    public Action<Pokemon, Pokemon> RetrieveMoveAction(int key) => moveDict.GetValueOrDefault(key).PerformMove;

    public void Build()
    {
        Type moveType = typeof(Move);
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> types = assembly.GetTypes();

        types = types.Where(t => t.IsSubclassOf(moveType));

        foreach (Type type in types)
        {
            var move = Activator.CreateInstance(type) as Move;

            if(moveDict.ContainsKey(move.ID))
            {
                Debug.LogError($"Could not add {move.GetType().Name} as ID [{move.ID}] already exists for {moveDict[move.ID].GetType().Name}");
                continue;
            }

            moveDict.Add(move.ID, move);
        }

        DebugDict();
    }

    public void DebugDict()
    {
        foreach(var move in moveDict)
        {
            var d = new Debugger();
            d.ID = move.Value.ID;
            d.Name = move.Value.GetType().Name;
            debuggerList.Add(d);    
        }
    }


}
