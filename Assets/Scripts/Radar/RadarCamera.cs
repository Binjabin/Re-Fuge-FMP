using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 100f, player.position.z);
    }
}
