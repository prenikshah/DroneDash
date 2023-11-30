using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] float second;

    private void Start()
    {
        Destroy(this.gameObject, second);
    }
}
