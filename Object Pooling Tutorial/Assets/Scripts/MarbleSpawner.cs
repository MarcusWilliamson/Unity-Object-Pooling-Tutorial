using System.Collections;
using UnityEngine;

public class MarbleSpawner : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private GameObject prefab;  // The object to spawn.
    Bounds bounds;  // Our spawn area's boundaries.

    private GameObjectPool marblePool;  // Our new pool!

    // Start is called before the first frame update
    void Start()
    {
        bounds = GetComponent<Collider>().bounds;
        // Initializing our new pool with our marble prefab, default size 25, and max size 100.
        marblePool = new GameObjectPool(prefab, 25, 100);
        StartCoroutine(SpawnMarbles());
    }

    // Spawn marbles every x seconds, x=cooldown.
    private IEnumerator SpawnMarbles()
    {
        while(true)
        {
            yield return new WaitForSeconds(cooldown);
            marblePool.GetObject(RandomPointInBounds());
        }
    }

    // Lets a marble tell the spawner it needs to be deleted, without giving marbles access to the pool.
    public void RemoveMarble(GameObject marble)
    {
        marblePool.ReleaseObject(marble);
    }

    // Get a random point within our collider's bounds.
    private Vector3 RandomPointInBounds()
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
            );
    }
}