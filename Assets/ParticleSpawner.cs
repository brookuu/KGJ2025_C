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
        // 先生成物件
        GameObject obj = Instantiate(prefab);

        // 設定為 ParticleLayer 的子物件
        obj.transform.SetParent(particleLayer, false);

        // 把世界座標轉成 UI 座標
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
