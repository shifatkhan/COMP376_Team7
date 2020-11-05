using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableInteractTemp : MonoBehaviour
{
    [SerializeField]
    Transform patienceStatusPrefab;
    [SerializeField]
    Transform skillCheckPrefab;

    private GameObject canvasRef;
    private Transform skillCheckObj;
    private Transform waterCupBarObj;
    private bool skillChecking = false;

    void Start()
    {
        canvasRef = GameObject.Find("Canvas - Game");

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waterCupBarObj = Instantiate(patienceStatusPrefab, screenPos, Quaternion.identity);
        waterCupBarObj.SetParent(canvasRef.transform);
    }

    void Update()
    {
        if (skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(false);

            if (skillCheckObj == null)
                skillChecking = false;
        }
        else if (!skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking)
        {
            skillChecking = true;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            skillCheckObj = Instantiate(skillCheckPrefab, screenPos, Quaternion.identity);
            skillCheckObj.SetParent(canvasRef.transform);
        }
    }
}
