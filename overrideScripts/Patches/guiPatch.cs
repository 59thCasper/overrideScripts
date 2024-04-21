using HarmonyLib;
using UnityEngine;
using System.Text;
using System.Linq;
using GPUInstancer;

[HarmonyPatch(typeof(GPUI_Manager), "Init")]
public static class GPUIManagerInitPatch
{
    [HarmonyPrefix]
    public static void Prefix(GPUI_Manager __instance)
    {
        GameObject planterMk23Prefab = AssetBundleLoader.InstantiatePrefab1("GPUI_Plantermk2");
        GameObject equi = AssetBundleLoader.InstantiatePrefab1("test");
        StringBuilder logMessage = new StringBuilder();
        logMessage.AppendLine("[GPUI_Manager] Init method completed");

        if (GameDefines.instance?.resources == null)
        {
            logMessage.AppendLine("GameDefines instance or resources are null");
            Debug.Log(logMessage.ToString());
            return;
        }

        logMessage.AppendLine("Detailed Resources Info:");
        foreach (var resource in GameDefines.instance.resources)
        {

            if (!(resource is BuilderInfo builderInfo && builderInfo.name == "Planter"))
                continue;

            logMessage.AppendLine("Detailed Resources Info:");
            Material replacementMaterial = planterMk23Prefab.GetComponent<Renderer>()?.material;  // test replave for mk2 material
            Material replacementMaterial2 = equi.GetComponent<Renderer>()?.material;      // test replace mode and texture
            MeshFilter replacementmesh = equi.GetComponent<MeshFilter>();
            logMessage.AppendLine($" ******** Mesh {replacementmesh}");


            if (replacementMaterial == null)
            {
                logMessage.AppendLine("Replacement material not found. No changes will be made.");
            }
            else
            {
                for (int i = 0; i < builderInfo.builderPrefab.gpuiStaticMeshes.Length; i++)
                {
                    var mesh = builderInfo.builderPrefab.gpuiStaticMeshes[i];
                    var gameObject = mesh.prefab.gameObject;
                    var prefab = mesh.prefab;
                    var renderer = gameObject.GetComponent<Renderer>();
                    var meshy = gameObject.GetComponent<MeshFilter>();
                    if (renderer != null)
                    {

                        // Replace the material
                        renderer.material = replacementMaterial;   //mk2 material

                        //meshy.mesh = replacementmesh.mesh;//test replace astroid mesh
                        //renderer.material = replacementMaterial2;   //test replace astroid material

                        // Log the new material info
                        logMessage.AppendLine($" ******** Mesh {i + 1}: GameObject={gameObject.name}, New Material={renderer.material.name}");
                    }
                    else
                    {
                        logMessage.AppendLine($" ******** Mesh {i + 1}: GameObject={gameObject.name}, No Renderer Available");
                    }
                }
            }
        }

        Debug.Log(logMessage.ToString());
    }
}


