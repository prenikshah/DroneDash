using UnityEngine;
using TMPro;

public class Laser : MonoBehaviour
{
    private readonly float maxScore = 100;
    private readonly float multiPlyFactor = 500.0f;
    public int lastId = -1;
    [HideInInspector] public int currentCombo = 1;

    public float gameScore = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public int maxCombo = 8;
    private void Start()
    {
        gameScore = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            if (other.GetComponent<Target>().targetId != 0)
            {
                if (other.GetComponent<Target>().targetId == (lastId + 1))
                {
                    currentCombo = Mathf.Clamp(currentCombo * 2, 2, maxCombo);
                }
                else
                {
                    currentCombo = 1;
                }
            }
            lastId = other.GetComponent<Target>().targetId;
            Bounds bounds = other.GetComponent<BoxCollider>().bounds;
            Vector3 centerPoint = bounds.center;
            Vector3 pointOfContact = GetComponent<BoxCollider>().ClosestPoint(centerPoint);
            float distanceFromCenterX = Mathf.Abs(centerPoint.x) - Mathf.Abs(pointOfContact.x);
            var scoreX = Mathf.Abs(maxScore - (Mathf.Abs(distanceFromCenterX) * multiPlyFactor));
            gameScore += (scoreX * currentCombo);
            scoreText.text = gameScore.ToString("0.00");
            comboText.text = $"{currentCombo}X";
            other.GetComponent<Target>().DestroyObject();
            FindObjectOfType<Player>().MakeEffect();
        }
    }
}
