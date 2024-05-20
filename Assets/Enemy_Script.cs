using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    Vector3 lastSeen;
    Vector3 target;

    public GameObject player;
    public float viewDistance;
    public float viewAngle;
    // Start is called before the first frame update
    void Start()
    {
        lastSeen = transform.position;
        target = player.transform.position;
    }

    bool SeePlayer()
    {
        Vector3 dir = player.transform.position - transform.position;
        if (dir.magnitude < viewDistance) 
        {
            float angle = Vector3.Dot(transform.up, dir.normalized);

            if((Mathf.Acos(angle) * Mathf.Rad2Deg) < viewAngle)
            {
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        bool seen = SeePlayer();

        if (seen)
        {
            Debug.DrawRay(transform.position, transform.up * viewDistance, Color.yellow, 0);
            lastSeen = player.transform.position;
            target = lastSeen;
        }
        if (Vector3.Distance(transform.position, target) < 0.5f) 
        {
            //AT TARGET, PICK UP NEW SPOT TO GO TO
            Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
            target = lastSeen + (rotation * transform.up * 2.0f);
        }
        else



        {
            Vector3 dir = target - transform.position;
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, p, Time.deltaTime * 10.0f);

            transform.position += transform.up * 3.0f * Time.deltaTime;
        }

        Debug.DrawRay(transform.position, transform.up * viewDistance, seen? Color.red : Color.yellow, 0);
    }
}
