using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    #region Singleton
    public static BattleManager instance { get; private set; } = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
        Initialize();
    }
    #endregion

    public const string BASE_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/";

    private List<Task> downloadQueue = new();

    public float loadProgress
    {
        get => downloadQueue.Count > 0 ? ((float)downloadQueue.Where(t => t.IsCompleted).Count() / (float)downloadQueue.Count) : 0;
    }

    [SerializeField] private List<int> toDownload; 

    private void Initialize()
    {
        //make queries here

        for(int id = 1; id <= 3; id++)
        {
            string frontFile =  $"{BASE_URL}{id}.png";
            string backFile =   $"{BASE_URL}back/{id}.png";

            downloadQueue.Add(WebAPIManager.Instance.DownloadImage(frontFile));
            downloadQueue.Add(WebAPIManager.Instance.DownloadImage(backFile));
        }
    }
}


