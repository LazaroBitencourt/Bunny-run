using UnityEngine;
using UnityEngine.Rendering;

public class spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void Spawn()

    {
        float spawnChance = Random.value;

        foreach (var obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));

    }
    public float minGap = 6f;
    public float maxGap = 9f;
    public float minGapFinal = 1.5f;   
    public float maxGapFinal = 2f;     
    public float timeToMaxDifficulty = 10f; 

    private float GetDelay()
    {
        float speed = 5f;

        if (GameManager.Instance != null)
        {
            speed = Mathf.Max(GameManager.Instance.gameSpeed, 1f);
        }

        float t = Mathf.Clamp01(Time.timeSinceLevelLoad / timeToMaxDifficulty);

        float min = Mathf.Lerp(minGap, minGapFinal, t);
        float max = Mathf.Lerp(maxGap, maxGapFinal, t);

        return Random.Range(min, max) / speed;
    }
}

