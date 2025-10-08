using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibles : MonoBehaviour
{
    // Start is called before the first frame update
    public float rotationSpeed = 25.0f;


    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Sphere")
        {
            FindObjectOfType<UIManager>().CollectibleCollected();  // ✅ Make sure this runs

            Destroy(gameObject);
        }
    }
}
