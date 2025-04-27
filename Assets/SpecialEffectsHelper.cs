using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpecialEffectsHelper : MonoBehaviour
{
    // ��ҥH�Ѩ䥦�}���ϥ�
    public static SpecialEffectsHelper Instance;
    // ���� prefab
    public ParticleSystem smokeEffect;
    // ���K prefab
    public ParticleSystem fireEffect;
    // Start is called before the first frame update
    void Awake()
    {
        // ���}�����ӥu�Q�K�[��@�ӳ�W��ҤW
        if (Instance != null)
        {
            Debug.LogError("Multiple instances of SpecialEffectsHelper!");
        }
        // ��l�Ƴ��
        Instance = this;
    }
    // �Ы��z���ĪG
    public void Explosion(Vector3 position)
    {
        // ��ҤƷ���
        instantiate(smokeEffect, position);
        // ��ҤƤ��K
        instantiate(fireEffect, position);
    }
    // ��Ҥ� �ɤl�t��
    private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
    {
        ParticleSystem newParticleSystem = Instantiate(
          prefab,
          position,
          Quaternion.identity
        ) as ParticleSystem;
        // Make sure it will be destroyed
        Destroy(
          newParticleSystem.gameObject,
          newParticleSystem.main.startLifetimeMultiplier
        );
        return newParticleSystem;
    }
}