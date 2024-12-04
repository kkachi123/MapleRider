using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    [SerializeField] float speed = 2.0f;
    [SerializeField] float animationSpeed = 0.08f;
    [SerializeField] GameObject player;
    int count = 0;
    float timeCount = 0.0f;
    void Update()
    {
        timeCount += Time.deltaTime;
        if(timeCount > animationSpeed)
        {
            timeCount = 0.0f;
            if (count >= 0 && count < 7)
            {
                count++;
                this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y + speed, player.transform.position.z);
            }
            else if (count >= 7 && count < 11)
            {
                count++;
                this.transform.position = new Vector3(player.transform.position.x, this.transform.position.y - speed, player.transform.position.z);
            }
            else
            {
                count = 0;
                this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 9.0f, player.transform.position.z);
            }
        }
        
        
    }
}
