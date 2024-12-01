using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EVENT_NAMES
{
    public static class BATTLE_EVENTS
    {
        public const string ON_POKEMON_FAINT = "ON_FAINT";
        public const string ON_POKEMON_CHANGED = "ON_POKEMON_CHANGED";
        public const string ON_POKEMON_HEALTH_CHANGED = "ON_POKEMON_HEALTH_CHANGED";
        public const string ON_POKEMON_MOVE_DECLARED = "ON_POKEMON_MOVE_DECLARED";
        public const string ON_CRITICAL_HIT = "ON_CRITICAL_HIT";
        public const string ON_PLAYER_WIN = "ON_PLAYER_WIN";
        public const string ON_ENEMY_WIN = "ON_ENEMY_WIN";
    }
    public static class UI_EVENTS
    {
        public const string ON_LOADING_FINISHED = "ON_LOADING_FINISHED";
        public const string ON_VIEWER_INVOKED = "ON_VIEWER_INVOKED";
        public const string ON_FORCE_SWITCH = "ON_FORCE_SWITCH";
        public const string ON_MOVE_VIEWER_INVOKED = "ON_MOVE_VIEWER_INVOKED";
        public const string ON_DIALOGUE_INVOKED = "ON_DIALOGUE_INVOKED";
        public const string ON_DIALOGUE_ENDED = "ON_DIALOGUE_ENDED";
    }

    public static class OVERWORLD_EVENTS
    {
        public const string ON_DIALOGUE_CONTINUED = "ON_DIALOGUE_CONTINUED";
    }

}
