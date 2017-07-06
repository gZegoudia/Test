using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour {
    public delegate void DoAction();

    public DoAction doAction;
    private bool mouseDown = false;
    public float speed;
    [SerializeField]
    public GameObject ground;
    private Vector3 directionToTake;
    private void Awake()
    {
        SetModeIDLE();
    }
    // Use this for initialization
    void Start () {
		
	}

    public void SetModeIDLE()
    {
        doAction = DoActionIDLE;
    }
    
    private void DoActionIDLE()
    {
        
    }

    public void SetModeMove()
    {
        doAction = DoActionMove;
    }
    private void DoActionMove()
    {
        Vector3 mousePositionHit = GetMousePosition();
        if (mousePositionHit != Vector3.zero)
        {

            Vector3 vectorDistance = mousePositionHit - transform.position ;

            directionToTake.Set(vectorDistance.normalized.x, 0, vectorDistance.normalized.z);

            Debug.DrawRay(transform.position, vectorDistance, Color.red);

            transform.Translate(directionToTake * Time.deltaTime * speed, Space.World);
        }
        else return;
    }

    private Vector3 GetMousePosition()
    {
        
        Vector3 defaultPoint;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            return defaultPoint = new Vector3(hit.point.x, 0, hit.point.z);
        }
        else return defaultPoint = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {

        Debug.Log(Input.GetMouseButton(0));
        if (Input.GetMouseButton(0) && !mouseDown)
        {
            Debug.Log("MOUSE DOWN");
            mouseDown = true;
            SetModeMove();
        }

        else if (!Input.GetMouseButton(0) && mouseDown)
        {
            Debug.Log("MOUSE UP");
            mouseDown = false;
            SetModeIDLE();
        }
        doAction();
	}
}
