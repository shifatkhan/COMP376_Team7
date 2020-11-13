using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPourable : MonoBehaviour
{
    [SerializeField]
    Transform waterStatusPrefab;
    [SerializeField]
    Transform waterCheckPrefab;
    [SerializeField]
    float drainRate = 0.1f;

    private GameObject canvasRef;
    private Transform skillCheckObj;
    private Transform waterCupBarObj;
    private Slider slider;

    private bool skillChecking = false;

    void Start()
    {
        canvasRef = GameObject.Find("Canvas - Game");

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        waterCupBarObj = Instantiate(waterStatusPrefab, screenPos, Quaternion.identity);
        waterCupBarObj.SetParent(canvasRef.transform);

        slider = waterCupBarObj.GetComponent<Slider>();
        slider.value = 1f;
    }

    void Update()
    {
        // make water cup bar follow table when camera follows player

        if (skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(false);

            // enable water draining again if done skill check
            if (skillCheckObj == null)
                skillChecking = false;
        }
        else if (!skillChecking)
        {
            waterCupBarObj.gameObject.SetActive(true);

            if (slider.value > 0)
                slider.value -= drainRate * Time.deltaTime;
        }
    }

    public void waterFilled()
    {
        slider.value = 1f;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("HERE");
        if (Input.GetButton("Interact") && other.CompareTag("Player") && !skillChecking)
        {
            skillChecking = true;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            skillCheckObj = Instantiate(waterCheckPrefab, screenPos, Quaternion.identity);
            skillCheckObj.SetParent(canvasRef.transform);
        }
    }
}
