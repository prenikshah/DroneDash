using System.Collections.Generic;
using UnityEngine;
public class MapGenrator : MonoBehaviour
{
    [SerializeField] GameObject[] cubePrefab;
    [SerializeField] Transform parentEnemyObject;
    public List<GameObject> enemyObjects = new();
    public static MapGenrator Instance { get; private set; }
    [SerializeField] TextAsset songBeats;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Instance = null;
        }
        Instance = this;
    }
    private void Start()
    {
        var beatsong = JsonUtility.FromJson<BeatMusic>(songBeats.text);
        for (int i = 0; i < beatsong.beats.Count; i++)
        {
            var target = Instantiate(cubePrefab[beatsong.beats[i].objectIndex], parentEnemyObject);
            float x;
            if (beatsong.beats[i].layerIndex == 0)
            {
                x = -0.45f;
            }
            else if (beatsong.beats[i].layerIndex == 2)
            {
                x = 0.45f;
            }
            else
            {
                x = 0;
            }
            target.GetComponent<Target>().targetId = i;
            target.transform.position = new Vector3(x, 5f, (beatsong.beats[i].beatSecond + 1) * Player.Instance.speed);
            enemyObjects.Add(target);
        }
    }

}

public class BeatMusic
{
    public List<Beat> beats = new();
}

[System.Serializable]
public struct Beat
{
    public float beatSecond;
    public int layerIndex;
    public int objectIndex;
    public Beat(float sec, int index, int o_index)
    {
        beatSecond = sec;
        layerIndex = index;
        objectIndex = o_index;
    }
}

