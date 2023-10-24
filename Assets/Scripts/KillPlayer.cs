using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillPlayer : MonoBehaviour
{
    GameObject deathPlane;

    [SerializeField] EndingHandler handler;

    // Start is called before the first frame update
    void Start()
    {
        deathPlane = GetComponent<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            handler.TriggerEnding(1);
        }
    }
}
