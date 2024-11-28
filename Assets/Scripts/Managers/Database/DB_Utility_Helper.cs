using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DB_Utility_Helper : MonoBehaviour
{
    public List<string> natureList = new List<string>();

    public ESex RandomGenerateSex()
    {
        int randomGender = Random.Range(1, 3);
        switch (randomGender)
        {
            case 1:
                return ESex.MALE;
            case 2:
                return ESex.FEMALE;
        }

        return ESex.NONE;
    }

    public Stat RandomGenerateIVs()
    {
        float hp = 0.0f;
        float atk = 0.0f;
        float sp_atk = 0.0f;
        float def = 0.0f;
        float sp_def = 0.0f;
        float spe = 0.0f;

        for (int i = 0; i < 6; i++)
        {
            float randomIV = Random.Range(0.0f, 32.0f);
            switch (i)
            {
                case 0:
                    hp = randomIV;
                    break;
                case 1:
                    atk = randomIV;
                    break;
                case 2:
                    sp_atk = randomIV;
                    break;
                case 3:
                    def = randomIV;
                    break;
                case 4:
                    sp_def = randomIV;
                    break;
                case 5:
                    spe = randomIV;
                    break;
            }
        }

        Stat ivStats = new Stat(hp, atk, def, sp_atk, sp_def, spe);
        return ivStats;
    }

    public Stat RandomGenerateEVs()
    {
        Stat evStats = new Stat(85.0f, 85.0f, 85.0f, 85.0f, 85.0f, 85.0f);
        return evStats;
    }

    public string RandomGenerateNature()
    {
        if (natureList.Count == 0)
        {
            natureList.Add("Hardy");
            natureList.Add("Lonely");
            natureList.Add("Brave");
            natureList.Add("Adamant");
            natureList.Add("Naughty");
            natureList.Add("Bold");
            natureList.Add("Docile");
            natureList.Add("Relaxed");
            natureList.Add("Impish");
            natureList.Add("Lax");
            natureList.Add("Timid");
            natureList.Add("Hasty");
            natureList.Add("Serious");
            natureList.Add("Jolly");
            natureList.Add("Naive");
            natureList.Add("Modest");
            natureList.Add("Mild");
            natureList.Add("Quiet");
            natureList.Add("Bashful");
            natureList.Add("Rash");
            natureList.Add("Hardy");
            natureList.Add("Calm");
            natureList.Add("Gentle");
            natureList.Add("Sassy");
            natureList.Add("Careful");
            natureList.Add("Quirky");
        }

        int randomNature = Random.Range(0, natureList.Count);
        return natureList[randomNature];
    }

    public EType getDecipheredType1(string type)
    {
        if (type == "Normal")
        {
            return EType.NORMAL;
        }
        else if (type == "Fire")
        {
            return EType.FIRE;
        }
        else if (type == "Grass")
        {
            return EType.GRASS;
        }
        else if (type == "Water")
        {
            return EType.WATER;
        }
        else if (type == "Electric")
        {
            return EType.ELECTRIC;
        }
        else if (type == "Ice")
        {
            return EType.ICE;
        }
        else if (type == "Fighting")
        {
            return EType.FIGHTING;
        }
        else if (type == "Poison")
        {
            return EType.POISON;
        }
        else if (type == "Ground")
        {
            return EType.GROUND;
        }
        else if (type == "Flying")
        {
            return EType.FLYING;
        }
        else if (type == "Psychic")
        {
            return EType.PSYCHIC;
        }
        else if (type == "Bug")
        {
            return EType.BUG;
        }
        else if (type == "Rock")
        {
            return EType.ROCK;
        }
        else if (type == "Ghost")
        {
            return EType.GHOST;
        }
        else if (type == "Dragon")
        {
            return EType.DRAGON;
        }
        else if (type == "Dark")
        {
            return EType.DARK;
        }
        else if (type == "Steel")
        {
            return EType.STEEL;
        }
        else if (type == "Fairy")
        {
            return EType.FAIRY;
        }

        return EType.NORMAL;
    }


    public EType? getDecipheredType2(string type)
    {
        if (type == "Normal")
        {
            return EType.NORMAL;
        }
        else if (type == "Fire")
        {
            return EType.FIRE;
        }
        else if (type == "Grass")
        {
            return EType.GRASS;
        }
        else if (type == "Water")
        {
            return EType.WATER;
        }
        else if (type == "Electric")
        {
            return EType.ELECTRIC;
        }
        else if (type == "Ice")
        {
            return EType.ICE;
        }
        else if (type == "Fighting")
        {
            return EType.FIGHTING;
        }
        else if (type == "Poison")
        {
            return EType.POISON;
        }
        else if (type == "Ground")
        {
            return EType.GROUND;
        }
        else if (type == "Flying")
        {
            return EType.FLYING;
        }
        else if (type == "Psychic")
        {
            return EType.PSYCHIC;
        }
        else if (type == "Bug")
        {
            return EType.BUG;
        }
        else if (type == "Rock")
        {
            return EType.ROCK;
        }
        else if (type == "Ghost")
        {
            return EType.GHOST;
        }
        else if (type == "Dragon")
        {
            return EType.DRAGON;
        }
        else if (type == "Dark")
        {
            return EType.DARK;
        }
        else if (type == "Steel")
        {
            return EType.STEEL;
        }
        else if (type == "Fairy")
        {
            return EType.FAIRY;
        }

        return null;
    }

    public EMoveType getDecipheredMoveType(string moveGroup)
    {
        if (moveGroup == "Physical")
        {
            return EMoveType.PHYSICAL;
        }
        else if (moveGroup == "Special")
        {
            return EMoveType.SPECIAL;
        }
        else if (moveGroup == "Status")
        {
            return EMoveType.STATUS;
        }

        return EMoveType.STATUS;
    }
    
}
