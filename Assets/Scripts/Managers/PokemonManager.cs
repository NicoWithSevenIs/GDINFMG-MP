using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonManager : MonoBehaviour
{
    #region Singleton

    public static PokemonManager Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    #endregion

    public static float CalculateDamage(
        Pokemon_Battle_Instance attacker, 
        Pokemon_Battle_Instance target, 
        float power, 
        EType type,
        EMoveType moveClass,
        out bool isACriticalStrike
    )
    {
        if (moveClass == EMoveType.STATUS)
        {
            isACriticalStrike = false;
            return 0f;
        }
            

        EStatType attackingStat = moveClass == EMoveType.PHYSICAL ? EStatType.ATTACK : EStatType.SPECIAL_ATTACK;
        EStatType defendingStat = moveClass == EMoveType.PHYSICAL ? EStatType.DEFENSE : EStatType.SPECIAL_DEFENSE;

        float A = attacker.stat.GetByEnum(attackingStat);
        float D = target.stat.GetByEnum(defendingStat);

        float levelMultiplier = 2 * (float)Pokemon_Battle_Instance.LEVEL / 5 + 2;
        float baseDamage = levelMultiplier * power * A/D / 50 + 2;


        //STAB
        if (attacker.Pokemon.data.type1 == type || attacker.Pokemon.data.type2 == type)
            baseDamage *= 1.5f;

        //CRIT
        isACriticalStrike = Random.Range(1, 25) == 24;
        if (isACriticalStrike)
            baseDamage *= (2 * Pokemon_Battle_Instance.LEVEL + 5) / Pokemon_Battle_Instance.LEVEL + 5;

        //Type Effectiveness
        baseDamage *= TypeChecker.GetEffectivenessMultiplier(type, target.Pokemon.data.type1, target.Pokemon.data.type2);

        //Random
        baseDamage *= Random.Range(85f, 100f) / 100f;

        //Burn
        if (attacker.AilmentType == EAilmentType.BURN)
            baseDamage *= 0.5f;


        return baseDamage;
    }


}
