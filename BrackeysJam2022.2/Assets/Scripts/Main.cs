using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject button;
    
    // Start is called before the first frame update
    private void OnMouseEnter()
    {
        button.GetComponent<Spin>().enabled = true;
        button.transform.localScale = new Vector3(1.1f, 1.1f);
    }

    // Update is called once per frame
    private void OnMouseExit()
    {
        button.GetComponent<Spin>().enabled = false;
        button.transform.localScale = new Vector3(1.0f, 1.0f);
    }
}
