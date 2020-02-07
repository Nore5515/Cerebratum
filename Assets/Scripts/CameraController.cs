using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed, bulletSpeed;
    public GameObject clickMarker, bullet;
    public List<UnitController> units;
    public List<Vector3> points;
    bool controlling;
    UnitController controlledUnit;

    // Start is called before the first frame update
    void Start()
    {
        controlling = false;
    }

    public void AddUnit (UnitController u){
      units.Add(u);
      //Debug.Log("Updating Units");
      foreach (UnitController uc in units){
        //Debug.Log("Giving unit " + uc.gameObject.name + " the points " + points.Count);
        //u.AddPoint(hit.point);
        uc.SetPoints(points);
      }
    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;


        //if you are controlling a unit...
        if (controlling){
            if (Input.GetKeyDown(KeyCode.Tab)){
              controlling = false;
              controlledUnit.Release();
            }
            if (Input.GetMouseButtonDown(0)){
              RaycastHit hit;
              Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
              GameObject b = Instantiate(bullet, controlledUnit.gameObject.transform.position, Quaternion.identity);
              if ( Physics.Raycast (ray,out hit,100.0f)) {
                Vector3 forceVector = hit.point - b.transform.position;
                b.GetComponent<Rigidbody>().AddForce(forceVector * bulletSpeed);
                Debug.Log(forceVector);
              }
            }
        }
        else if (Input.GetMouseButton (0) && !controlling){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ( Physics.Raycast (ray,out hit,100.0f)) {
                if (hit.transform.gameObject.tag == "Unit"){
                  //run unit takeover
                  Debug.Log("Howdy!");
                  controlledUnit = hit.transform.gameObject.GetComponent<UnitController>();
                  controlledUnit.Takeover();
                  controlling = true;
                }
                else{
                  //add a point
                  //Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                  Debug.DrawRay(transform.position, ray.direction * 100.0f, Color.yellow);
                  Instantiate(clickMarker, hit.point, Quaternion.identity);
                  points.Add(hit.point);
                  Debug.Log("Updating Units");
                  foreach (UnitController u in units){
                    //u.AddPoint(hit.point);
                    u.SetPoints(points);
                  }
                }
            }
        }
    }
}
