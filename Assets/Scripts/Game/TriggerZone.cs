using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    Vector3 endPosition; //Position where player ends up after sliding
    float endPositionIncrement = 4;

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {         
            endPosition = col.transform.position;
            CalculateEndPosition(col.transform.gameObject); 

            //Sliding effect
            StartCoroutine (MoveOverSeconds (col.transform.gameObject, endPosition, 1f)); //Moves over 1 second
        }
    }
    
    //Sliding movement
    public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    //Calculate the end position of the player depending on which side he came from
    void CalculateEndPosition(GameObject objectToMove)
    {
        //This means the player is moving in the left direction, heading into the trigger zone right side;
        if(transform.position.x < objectToMove.transform.position.x)
        {
            endPosition.x -= endPositionIncrement;
        }

        //This means the player is moving in the right direction, heading into the trigger zone left side;
        if(transform.position.x >= objectToMove.transform.position.x)
        {
            endPosition.x += endPositionIncrement;
        }

        //This means the player is moving in the down direction, heading into the trigger zone top side;
        if(transform.position.z < objectToMove.transform.position.z)
        {
            endPosition.z -= endPositionIncrement;
        }

        //This means the player is moving in the up direction, heading into the trigger zone bottom side;
        if(transform.position.z >= objectToMove.transform.position.z)
        {
            endPosition.z += endPositionIncrement;
        }
    }
}
