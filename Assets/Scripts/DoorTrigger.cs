using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        foreach (GameObject target in targets) {
            target.SendMessage("Activate");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        foreach (GameObject target in targets) {
            target.SendMessage("Deactivate");
        }
    }
}
