using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", destroyTime);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
