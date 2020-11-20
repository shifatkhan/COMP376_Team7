using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Delete this file.
[System.Obsolete("This class won't be used anymore. It's going to be replaced by Mop.cs", true)]
public class TriggerZone : MonoBehaviour
{
    Vector3 endPosition; //Position where player ends up after sliding
    float endPositionIncrement = 4;

    ParticleSystem ps;
    AudioSource mopAudio;
    GameObject[] mop;

    bool slidingEffect = true;

    void Awake()
    {
        ps = GetComponentInChildren<ParticleSystem>(); //Bubble effect

        mop = GameObject.FindGameObjectsWithTag("Mop");
        mopAudio = mop[0].GetComponent<AudioSource>(); //Cleaning audio of mop
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Player")
        {   
            //Check if player is holding a mop (should be a child object of PickupObject)      
            foreach(Transform children in col.transform.Find("PickupObject"))
            {
                if(children.gameObject.tag == "Mop")
                {
                    slidingEffect = false; //Cannot slide anymore

                    //transform.GetChild(0).gameObject.SetActive(false); //Uncomment this if we want water spill to become invisible immediately as mop goes over it
                    
                    mopAudio.Play();   //Sound effect of mopping
                    ps.Play();  //Show bubbly effect of spill

                    //Player cleans up spill if he is holding the mop
                    Destroy(gameObject, 2);
                }
            }

            //If Player is not holding mop, he will slide
            if(slidingEffect == true)
            {
                endPosition = col.transform.position;
                CalculateEndPosition(col.transform.gameObject); 

                //Sliding effect
                StartCoroutine (MoveOverSeconds (col.transform.gameObject, endPosition, 1f)); //Moves over 1 second
            }


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
