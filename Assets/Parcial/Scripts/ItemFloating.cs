using UnityEngine;
public class ItemFloating : MonoBehaviour
{
    

    public float rotationSpeed = 50f;
    public float floatSpeed = 0.5f;
    public float floatAmplitude = 0.5f;

    private Vector3 initialPosition;

    void Start()
    {
        // Guarda la posición inicial del objeto
        initialPosition = transform.position;
    }

    void Update()
    {
        // Hace que el objeto rote constantemente
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));

        // Hace que el objeto se mueva hacia arriba y hacia abajo
        float newY = initialPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
