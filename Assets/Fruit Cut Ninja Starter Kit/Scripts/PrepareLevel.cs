using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareLevel : MonoBehaviour
{
    public GameObject GetReady;
    public GameObject GO;
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Timer>().timeAvailable = SharedSetting.ConfigTime;

    }
    void Start()
    {
        GameObject.Find("Canvas/LevelName/LevelName").GetComponent<Text>().text = SharedSetting.LevelName[SharedSetting.LoadLevel];
        StartCoroutine(PrepareRoutine());
    }
    IEnumerator PrepareRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        GetReady.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        GetReady.SetActive(false);
        GO.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        GO.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
