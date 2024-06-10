using UnityEngine;

public class Elevator : MonoBehaviour
{
    [Header("Params")]
    public Vector2 values;
    public AnimationCurve curve;
    public float time;

    [Header("Objects")]
    public SpriteRenderer platform;
    public SpriteRenderer chainL;
    public SpriteRenderer chainR;

    private float _value;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > time) timer = 0.0f;

        float v = curve.Evaluate(timer / time);
        float targetValue = Mathf.LerpUnclamped(values.x, values.y, v);
        UpdateElevator(targetValue);
    }

    private void UpdateElevator(float value)
    {
        if (value < 0)
        {
            Debug.LogError("Invalid value for Elevator: " + value);
            value = 0.0f;
        }
        _value = value;

        platform.transform.localPosition = new Vector3(0.0f, -_value + 0.09375f, 0.0f);
        chainL.size = new Vector2(0.09375f, _value + 0.59375f);
        chainR.size = new Vector2(0.09375f, _value + 0.59375f);

        Debug.Log("Elevator value updated to: " + _value);
    }
}