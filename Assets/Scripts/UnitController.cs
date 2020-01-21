using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    public float speed;
    public List<Vector3> points;
    int currentPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
      // Check if the position of the cube and sphere are approximately equal.
      if (points.Count > 0 && currentPoint < points.Count){

        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint], step);
        if (Vector3.Distance(transform.position, points[currentPoint]) < 0.001f)
        {
            // Swap the position of the cylinder.
            currentPoint++;
        }

      }
    }

    public void AddPoint (Vector3 point)
    {
        points.Add(point);
    }

}
