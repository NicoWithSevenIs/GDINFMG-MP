using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TypeCheckingDebugger : MonoBehaviour
{
    [Header("Check Multiplier")]
    [SerializeField] internal EType attackerType;


    [SerializeField] internal EType defendingType1;

    [SerializeField] internal bool hasSecondType;
    [SerializeField] internal EType defendingType2;



    [Header("Effectivity Checker")]
    [SerializeField] internal EType attack;
    [SerializeField] internal EType defend;

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
        script.attackerType = (EType)EditorGUILayout.EnumPopup("Attacker Type Type", script.attackerType);
        script.defendingType1 = (EType)EditorGUILayout.EnumPopup("Defender First Type", script.defendingType1);

        script.hasSecondType = EditorGUILayout.Toggle("Has Second Type", script.hasSecondType);

        if (script.hasSecondType)
            script.defendingType2 = (EType)EditorGUILayout.EnumPopup("Defender Second Type", script.defendingType2);

        EditorGUILayout.Space();
        
        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);
        if (GUILayout.Button("Log Multiplier"))     
            Debug.Log(TypeChecker.GetEffectivenessMultiplier(script.attackerType, script.defendingType1, script.hasSecondType ? script.defendingType2 : null));
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space(20);

        GUILayout.Label("Effectiveness Checker", boldStyle);
        EditorGUILayout.Space(5);
        script.attack = (EType)EditorGUILayout.EnumPopup("Attacker Type Type", script.attack);
        script.defend = (EType)EditorGUILayout.EnumPopup("Defender First Type", script.defend);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);

        if (GUILayout.Button("Swap"))
        {
            EType temp = script.attack;
            script.attack = script.defend;
            script.defend = temp;
        }
           
        if (GUILayout.Button("Log Effectiveness"))
            TypeChecker.CheckEffectivity(script.attack, script.defend);
        EditorGUI.EndDisabledGroup();
    }
}
#endif