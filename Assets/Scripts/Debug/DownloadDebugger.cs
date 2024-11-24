using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadDebugger : MonoBehaviour
{
    [Header("ID")]
    [SerializeField] internal int enemyID;
    [SerializeField] internal int playerID;

    [Header("Images")]
    [SerializeField] internal Image playerSprite;
    [SerializeField] internal Image enemySprite;


    internal string p_url;
    internal string e_url;


}

#if UNITY_EDITOR 
[CustomEditor(typeof(DownloadDebugger))]
public class DownloadDebugger_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();

        var script = (DownloadDebugger)target;

        if (GUILayout.Button("Set"))
        {

            IEnumerator s(Image i, string url)
            {
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
                yield return request.SendWebRequest();

                var response = (DownloadHandlerTexture)request.downloadHandler;
                if (string.IsNullOrEmpty(response.error))
                {
                    Texture2D tex = response.texture;
                    i.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
                }
            }


            script.e_url = $"{BattleManager.BASE_URL}{script.playerID}.png";
            script.p_url = $"{BattleManager.BASE_URL}back/{script.enemyID}.png";

            script.StartCoroutine(s(script.playerSprite, script.p_url));
            script.StartCoroutine(s(script.enemySprite, script.e_url));


       

        }

 
    }
}
#endif