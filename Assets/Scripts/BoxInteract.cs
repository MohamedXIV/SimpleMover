using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteract : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;
    [SerializeField] private float thrust = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (parentObject == null)
        {
            parentObject = GetComponentInParent<GameObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            //yield return StartCoroutine(WaitForSeconds(50));
            StartCoroutine(ShowBox());
            Debug.Log("We are beside a cube");
        }
    }

    private IEnumerator ShowBox()
    {
        yield return new WaitForSeconds(3);
        parentObject.GetComponent<Renderer>().enabled = true;
        parentObject.GetComponent<Rigidbody>().AddForce(transform.up * thrust);
        Mover.staminaBuff = 2f;
        Debug.Log(Mover.staminaBuff);
    }
}
