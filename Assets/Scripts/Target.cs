using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject effect;
    public int targetId;
    public void DestroyObject()
    {
        StartCoroutine(ShowHitEffect());
    }
    void OnBecameInvisible()
    {
        enabled = false;
    }
    IEnumerator ShowHitEffect()
    {
        this.GetComponent<MeshRenderer>().enabled = false;
        Instantiate(effect, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        DestroyImmediate(this.gameObject);
    }
}
