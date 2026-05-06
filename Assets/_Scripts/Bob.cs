using UnityEngine;

public class Bob : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;
Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time * speed) * height + pos.y, transform.position.z);
    }


}
