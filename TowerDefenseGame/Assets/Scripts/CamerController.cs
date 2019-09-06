using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{

    public float panSpeed = 30f;
    public float panBorderThikness = 10f;
    

    public float minY = 10f;
    public float maxY = 50f;

    public float scrollSpeed = 5f;

    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            
        }
    
        

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThikness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThikness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThikness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThikness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        float scroll  = Input.GetAxis("Mouse ScrollWheel");

        Vector3 pos = transform.position;
       

        pos.y -= scrollSpeed * 1000 * scroll * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;


    }
}
