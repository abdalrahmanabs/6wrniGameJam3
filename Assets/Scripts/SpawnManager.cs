using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    bool isCreated = false;
    private void Awake()
    {
        if (!isCreated)
        { Instance = this; isCreated = true; }
        else
            Destroy(gameObject);    


    }
    // Start is called before the first frame update
    public void SpawnObject(GameObject Object,Transform pos=null)
    {
        LeanPool.Spawn(Object);
    }
    public void DespawnObject(GameObject Object)
    {
        LeanPool.Despawn(Object);
    }
}
