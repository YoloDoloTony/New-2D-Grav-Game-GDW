using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isToggled;
    bool canInteract;
    public GameObject doorObject;

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            isToggled = !isToggled;
            doorObject.SetActive(isToggled);
        }
    }

    private void OnTriggerEnter2D(Collider2D leverBox)
    {
        if (leverBox.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D leverBox)
    {
        if (leverBox.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
