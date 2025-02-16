using UnityEngine;
using UnityEngine.VFX;

public class FireIntensityController : MonoBehaviour {
    [Header("Fire VFX Settings")]
    [SerializeField] float maxFireIntensity;
    [SerializeField] float fireIntensity;
    [SerializeField] float maxFuelCount;
    [SerializeField] float fuelCount;

    [SerializeField] float timePerFuel;
    [SerializeField] float smoothDecayMulti = 10f;
    [SerializeField] float fuelBurnsAtTime;
    [SerializeField] float timeScinceLastFuelBurned;
    float intensityPerFuel;

    [SerializeField] Light fireLight;
    [SerializeField] int maxLightIntensity;
    [SerializeField] VisualEffect VFX_fire;

    private void Start() {
        intensityPerFuel = maxFireIntensity / maxFuelCount;
    }


    Vector2 lightY_MinMax = new Vector2(-0.6f, -0.113f);
    private void Update() {
        timeScinceLastFuelBurned += Time.deltaTime;
        if (timeScinceLastFuelBurned >= timePerFuel / smoothDecayMulti && fuelCount > 0) {
            timeScinceLastFuelBurned = 0;
            fuelCount -= fuelBurnsAtTime / smoothDecayMulti;
        }

        if (fuelCount <= 0) {
            fuelCount = 0;
        }

        fireIntensity = fuelCount * intensityPerFuel;

        VFX_fire.SetFloat("FireSizeMulti", fireIntensity);
        fireLight.shadowStrength = Mathf.Sqrt(1 / maxFireIntensity * fireIntensity) * 0.45f;
        fireLight.intensity = maxLightIntensity / maxFireIntensity * fireIntensity;
        fireLight.range = (20 / maxFireIntensity) * 1.15f * Mathf.Sqrt(fireIntensity) + 1;
    }

    public bool AddFuel(float count) {
        fuelCount += count;
        return true;
    }
}
