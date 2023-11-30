using UnityEngine;

public class BeatDetection : MonoBehaviour
{
    public GameObject cubePrefab;
    public float cubeSpeed = 5f;
    public Transform[] spawnPoints; // Array of spawn points for the cubes

    private AudioSource audioSource;
    private float[] spectrumData = new float[512];
    private float[] beatMap = { 7f, 10f, 10.5f, 11f, 12f, 14f, 15f, 16f, 16.5f, 17f, 18f, 19f, 20f, 21f, 22, 23, 23.25f, 23.5f, 23.75f, 24, 24.5f, 25 }; // Example beat map with timestamps in seconds

    private int nextBeatIndex = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Detect beats based on the audio spectrum data
        if (nextBeatIndex < beatMap.Length)
        {
            float nextBeatTime = beatMap[nextBeatIndex];
            float songTime = audioSource.time;

            if (songTime >= nextBeatTime)
            {
                // Calculate the position to spawn the cube based on the beat map and spawn points
                Vector3 spawnPosition = spawnPoints[nextBeatIndex % spawnPoints.Length].position;
                // Add an offset on the Y-axis to adjust the cube height
                spawnPosition.y = 2f;

                // Spawn the cube at the calculated position
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);

                // Increment the beat index for the next beat
                nextBeatIndex++;
            }
        }

        // Move the existing cubes towards the player
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Finish");
        foreach (GameObject cube in cubes)
        {
            cube.transform.Translate(Vector3.back * cubeSpeed * Time.deltaTime);
        }
    }
}