using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDevilMove : MonoBehaviour
{
    private TempCharacter player;
    [SerializeField] Vector3 distanceOffset;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCharacter").GetComponent<TempCharacter>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
