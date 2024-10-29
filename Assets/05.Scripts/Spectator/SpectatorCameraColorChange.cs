using System.Collections.Generic;
using UnityEngine;

public class SpectatorCameraColorChange : MonoBehaviour
{
    public Material ScientistOutlineMaterial; // 과학자 아웃라인 머테리얼
    public Material MonsterOutlineMaterial; // 몬스터 아웃라인 머테리얼

    public void EnableOutlineEffect()
    {
        GameObject[] Scientists = GameObject.FindGameObjectsWithTag("ScientistOutline");

        foreach (GameObject obj in Scientists)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = obj.GetComponent<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer != null)
            {
                Material[] materials = skinnedMeshRenderer.materials;

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = ScientistOutlineMaterial;
                }

                skinnedMeshRenderer.materials = materials;
            }
        }

        GameObject[] Monsters = GameObject.FindGameObjectsWithTag("MonsterOutline");

        foreach (GameObject obj in Monsters)
        {
            SkinnedMeshRenderer skinnedMeshRenderer = obj.GetComponent<SkinnedMeshRenderer>();

            if (skinnedMeshRenderer != null)
            {
                Material[] materials = skinnedMeshRenderer.materials;

                for (int i = 0; i < materials.Length; i++)
                {
                    materials[i] = MonsterOutlineMaterial;
                }

                skinnedMeshRenderer.materials = materials;
            }
        }

        Debug.Log("Outline Effect Enabled");
    }
}