using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeOut : MonoBehaviour
{
    public float speed = 0.5f;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(gameObject.activeSelf)
        {
            color.a -= Time.deltaTime * speed;
            GetComponent<Text>().color = color;
        }
    }
}
