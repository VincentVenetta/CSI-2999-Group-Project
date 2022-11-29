using UnityEngine;

public class UIRotation : MonoBehaviour
{
    [SerializeField] private GameObject UIElement;
    [SerializeField] [Range(5f, 10f)] private float rotationSpeed;

    //Rotate a GUI element constantly
    void Update()
    {
        Vector3 zRotation = new Vector3(0f, 0f, (((UIElement.transform.rotation.z + rotationSpeed) * Time.deltaTime)));
        UIElement.transform.Rotate(zRotation);
    }
}
