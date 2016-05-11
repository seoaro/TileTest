using UnityEngine;
using System.Collections;

public class TileMouseOver : MonoBehaviour {

    public Color hightLightColor;
    Color normalColor;
	// Use this for initialization
	void Start () {
        normalColor = GetComponent<Renderer>().material.color;

    }
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(GetComponent<Collider>().Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            GetComponent<Renderer>().material.color = hightLightColor;
        }
        else
        {
            GetComponent<Renderer>().material.color = normalColor;
        }
        
	}
    /*
    void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = Color.blue;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    */
}
