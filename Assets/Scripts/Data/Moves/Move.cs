using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move
{
    public abstract int ID {get;}
    public abstract void PerformMove(Pokemon_Battle_Instance attacker, Pokemon_Battle_Instance target);

    private MoveData? m_Data = null;

    public MoveData Data { 
        get {
            RequestMove(); //force request move if move is still somewhat null
            return m_Data.Value;
        } 
    }

    public virtual void RequestMove()
    {
        if (m_Data != null)
            return;

        var def = new MoveData("[ERROR]", "Move Request Failure", 0, 0, 1);
        m_Data = def;
        //make query here
    }

    
}
