using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class HorrorLightFlicker : MonoBehaviour
{
    public float normalIntensity = 5f;
    public float flickerIntensity = 1f;

    public float minTimeBetweenFlickers = 2f;
    public float maxTimeBetweenFlickers = 8f;

    public int minFlickers = 2;
    public int maxFlickers = 6;

    private Light lightSource;

    void Start()
    {
        lightSource = GetComponent<Light>();
        lightSource.intensity = normalIntensity;

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(
                Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers));

            int flickers = Random.Range(minFlickers, maxFlickers + 1);

            for (int i = 0; i < flickers; i++)
            {
                lightSource.intensity = flickerIntensity;
                yield return new WaitForSeconds(Random.Range(0.03f, 0.08f));

                lightSource.intensity = normalIntensity;
                yield return new WaitForSeconds(Random.Range(0.02f, 0.06f));
            }
        }
    }
}