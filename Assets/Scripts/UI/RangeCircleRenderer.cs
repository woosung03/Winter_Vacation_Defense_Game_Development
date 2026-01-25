using UnityEngine;
using Systems;

[RequireComponent(typeof(LineRenderer))]
public class RangeCircleRenderer : MonoBehaviour
{
    [SerializeField] private BunkerGun bunkerGun;
    [SerializeField] private int segments = 64;

    [Header("Visual")]
    [SerializeField] private Color circleColor = new Color(1f, 1f, 1f, 0.15f);
    [SerializeField] private float lineWidth = 0.05f;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        if (bunkerGun == null) bunkerGun = GetComponent<BunkerGun>();

        lr.useWorldSpace = true;
        lr.loop = true;
        lr.positionCount = segments;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;

        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = circleColor;
        lr.endColor = circleColor;
    }

    private void Update()
    {
        if (bunkerGun == null) return;

        DrawCircle(transform.position, bunkerGun.Range);
    }

    private void DrawCircle(Vector3 center, float radius)
    {
        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * angleStep);
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, center + new Vector3(x, y, 0f));
        }
    }
}
