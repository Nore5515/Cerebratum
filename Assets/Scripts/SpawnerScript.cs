using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject unit;
    public CameraController player;
    public float spawnTime;
    float curTime;
    GameObject u;

    // Start is called before the first frame update
    void Start()
    {
        curTime = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
      curTime -= Time.deltaTime;
      if (curTime < 0)
      {
          curTime = spawnTime;
          u = Instantiate(unit, this.transform.position, Quaternion.identity);
          player.AddUnit(u.GetComponent<UnitController>());
      }
    }
}
