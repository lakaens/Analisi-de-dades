using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePositions : MonoBehaviour
{

    public EventsHandler eventhandler = null;
    float timer = 0.0f;
    public float time2save = 0.3f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time2save)
        {
            eventhandler.WritePositions(transform.position);
            timer = 0.0f;
        }
    }
}