using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TypeCheckingDebugger : MonoBehaviour
{
    [Header("Check Multiplier")]
    [SerializeField] internal Type attackerType;


    [SerializeField] internal Type defendingType1;

    [SerializeField] internal bool hasSecondType;
    [SerializeField] internal Type defendingType2;



    [Header("Effectivity Checker")]
    [SerializeField] internal Type attack;
    [SerializeField] internal Type defend;

    private void Start() => hasSecondType = false;
}


#if UNITY_EDITOR 
[CustomEditor(typeof(TypeCheckingDebugger))]
public class TypeCheckingDebugger_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        GUIStyle boldStyle = new GUIStyle(GUI.skin.label);
        boldStyle.fontStyle = FontStyle.Bold;

        var script = (TypeCheckingDebugger)target;
        EditorGUILayout.Space(5);

        GUILayout.Label("Multiplier Checker", boldStyle);
        EditorGUILayout.Space(5);
        script.attackerType = (Type)EditorGUILayout.EnumPopup("Attacker Type Type", script.attackerType);
        script.defendingType1 = (Type)EditorGUILayout.EnumPopup("Defender First Type", script.defendingType1);

        script.hasSecondType = EditorGUILayout.Toggle("Has Second Type", script.hasSecondType);

        if (script.hasSecondType)
            script.defendingType2 = (Type)EditorGUILayout.EnumPopup("Defender Second Type", script.defendingType2);

        EditorGUILayout.Space();
        
        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
        if (GUILayout.Button("Log Multiplier"))     
            Debug.Log(TypeChecker.GetEffectivenessMultiplier(script.attackerType, script.defendingType1, script.hasSecondType ? script.defendingType2 : null));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(20);

        GUILayout.Label("Effectiveness Checker", boldStyle);
        EditorGUILayout.Space(5);
        script.attack = (Type)EditorGUILayout.EnumPopup("Attacker Type Type", script.attack);
        script.defend = (Type)EditorGUILayout.EnumPopup("Defender First Type", script.defend);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);

        if (GUILayout.Button("Swap"))
        {
            Type temp = script.attack;
            script.attack = script.defend;
            script.defend = temp;
        }
           
        if (GUILayout.Button("Log Effectiveness"))
            TypeChecker.CheckEffectivity(script.attack, script.defend);
        EditorGUI.EndDisabledGroup();
    }
}
#endif