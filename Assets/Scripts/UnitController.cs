using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{

    public float speed;
    public List<Vector3> points;
    int currentPoint;
    bool controlled;

    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        points = new List<Vector3>();
        controlled = false;
    }

    // Update is called once per frame
    void Update()
    {
      // Check if the position of the cube and sphere are approximately equal.
      if (points.Count > 0 && currentPoint < points.Count && !controlled){

        float step =  speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, points[currentPoint], step);
        if (Vector3.Distance(transform.position, points[currentPoint]) < 0.001f)
        {
            // Swap the position of the cylinder.
            currentPoint++;
        }

      }
      // if you're being controlled, instead...
      else if (controlled){
        Camera.main.transform.position = new Vector3(this.transform.position.x, Camera.main.transform.position.y, this.transform.position.z);
        var move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
      }
    }

    public void AddPoint (Vector3 point)
    {
        points.Add(point);
    }

    public void SetPoints (List<Vector3> _points){
        Debug.Log("Updating with " + _points.Count);
        points = _points;
    }

    public void Takeover()
    {
        controlled = true;
    }
    public void Release(){
        controlled = false;
    }

}
