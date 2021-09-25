using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovedPlatforms : MonoBehaviour
{
    [SerializeField] Transform startPos, endPos;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform;
            StartCoroutine(MoveObject(transform, startPos.position, endPos.position, time));
            StartCoroutine(MoveObject(transform, endPos.position, startPos.position, time));

    }

    // Update is called once per frame
 
    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        while (true)
        {
            float i = 0.0f;
            float rate = 1.0f / time;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(startPos, endPos, i);
                yield return 0;
            }
            i = 0;
            while (i < 1.0f)
            {
                i += Time.deltaTime * rate;
                thisTransform.position = Vector3.Lerp(endPos, startPos, i);
                yield return 0;
            }
        }
    }
    
}