using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move
{
    public abstract int ID {get;}
    public abstract void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target);

    private MoveData? m_Data = null;

    public MoveData Data {
        get => m_Data.Value;
        set => m_Data = value;
    }
}
