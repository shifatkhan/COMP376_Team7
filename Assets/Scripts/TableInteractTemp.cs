using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractTemp : MonoBehaviour
{
    [SerializeField]
    Transform skillCheckPrefab;

    GameObject canvasRef;

    // Update is called once per frame
    void Update()
    {
        canvasRef = GameObject.Find("Level Canvas");
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") /*&& other.CompareTag("Player")*/)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            var prefab = Instantiate(skillCheckPrefab, screenPos, Quaternion.identity);
            prefab.SetParent(canvasRef.transform);
        }
    }
}
