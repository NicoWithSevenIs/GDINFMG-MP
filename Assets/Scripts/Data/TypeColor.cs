using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeColor
{
    #region Singleton

    private static TypeColor instance = null;
    public static TypeColor Instance { get => instance ?? (instance = new TypeColor()); }

    #endregion

    private Dictionary<EType, Color> colors = new();

    public static Color GetColor(EType type) => Instance.colors[type];

    private TypeColor()
    {
        colors[EType.NORMAL] = new(0.66f, 0.65f, 0.48f);
        colors[EType.FIRE] = new(0.93f, 0.51f, 0.19f);
        colors[EType.WATER] = new(0.39f, 0.56f, 0.94f);
        colors[EType.ELECTRIC] = new(0.97f, 0.82f, 0.17f);
        colors[EType.GRASS] = new(0.48f, 0.78f, 0.30f);
        colors[EType.ICE] = new(0.59f, 0.85f, 0.84f);
        colors[EType.FIGHTING] = new(0.76f, 0.18f, 0.16f);
        colors[EType.POISON] = new(0.64f, 0.24f, 0.63f);
        colors[EType.GROUND] = new(0.89f, 0.75f, 0.40f);
        colors[EType.FLYING] = new(0.66f, 0.56f, 0.95f);
        colors[EType.PSYCHIC] = new(0.98f, 0.33f, 0.53f);
        colors[EType.BUG] = new(0.65f, 0.73f, 0.10f);
        colors[EType.ROCK] = new(0.71f, 0.63f, 0.21f);
        colors[EType.GHOST] = new(0.45f, 0.34f, 0.59f);
        colors[EType.DRAGON] = new(0.44f, 0.21f, 0.99f);
        colors[EType.DARK] = new(0.44f, 0.34f, 0.27f);
        colors[EType.STEEL] = new(0.72f, 0.72f, 0.81f);
        colors[EType.FAIRY] = new(0.84f, 0.52f, 0.68f);
    }



}
