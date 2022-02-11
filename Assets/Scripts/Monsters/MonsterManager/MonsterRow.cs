using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRow : MonoBehaviour
{
    [SerializeField] Vector3 firstPosition;
    [SerializeField] float positionInterval;
    private Vector3 lastPosition; 

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //最後尾の位置を更新してその位置を返す
    public Vector3 GetNextPosition()
    {
        if(lastPosition == Vector3.zero)
        {
            lastPosition = firstPosition;
            return lastPosition;
        }
        lastPosition += new Vector3(positionInterval,0,0);
        return lastPosition;
    }
}
