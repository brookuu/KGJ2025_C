using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public static ParticleSpawner Instance;
    public Transform particleLayer;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnParticle(GameObject prefab, Vector3 worldPosition)
    {
        // ���ͦ�����
        GameObject obj = Instantiate(prefab);

        // �]�w�� ParticleLayer ���l����
        obj.transform.SetParent(particleLayer, false);

        // ��@�ɮy���ন UI �y��
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            particleLayer.GetComponent<RectTransform>(),
            Camera.main.WorldToScreenPoint(worldPosition),
            Camera.main,
            out localPoint
        );
        obj.GetComponent<RectTransform>().localPosition = localPoint;
    }
}
