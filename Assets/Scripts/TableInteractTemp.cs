using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractTemp : MonoBehaviour
{
    [SerializeField]
    Transform skillCheckPrefab;

    private GameObject canvasRef;
    private bool skillChecking = false;

    // Update is called once per frame
    void Update()
    {
        canvasRef = GameObject.Find("Justins Level Canvas");
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking)
        {
            skillChecking = true;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            var prefab = Instantiate(skillCheckPrefab, screenPos, Quaternion.identity);
            prefab.SetParent(canvasRef.transform);
        }
    }
}
