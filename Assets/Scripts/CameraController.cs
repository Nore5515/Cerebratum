using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float speed;
    public GameObject clickMarker;
    public List<UnitController> units;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;

        if (Input.GetMouseButton (0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if ( Physics.Raycast (ray,out hit,100.0f)) {
                //Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                Debug.DrawRay(transform.position, ray.direction * 100.0f, Color.yellow);
                Instantiate(clickMarker, hit.point, Quaternion.identity);
                foreach (UnitController u in units){
                  u.AddPoint(hit.point);
                }
            }
        }
    }
}
