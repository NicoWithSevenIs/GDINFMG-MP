using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class WebAPIManager : MonoBehaviour
{
    #region Singleton

    public static WebAPIManager Instance { get; private set; } = null;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    #endregion


    private Dictionary<string, Sprite> downloadedSprites = new();
    public async Task DownloadImage(string url)
    {
        if (downloadedSprites.ContainsKey(url))
            return;
                  
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();

        var response = (DownloadHandlerTexture)request.downloadHandler;
        if (string.IsNullOrEmpty(response.error))
        {
            Texture2D tex = response.texture;
            tex.filterMode = FilterMode.Point;
           
            downloadedSprites[url] =  Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
        }
        else
        {
            Debug.Log(response.error);
        }

    }

    /*
    public async Task<Sprite> WaitForSprite(string url)
    {
        await DownloadImage(url);
        Debug.Log("Downloaded");
        return downloadedSprites[url];
    }
    */

    public Sprite GetSprite(string url) => downloadedSprites.ContainsKey(url) ? downloadedSprites[url] : null;
 
  


}
