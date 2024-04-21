using System.IO;
using System.Reflection;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    public static AssetBundle loadedAssetBundle;
    public static bool IsLoaded => loadedAssetBundle != null;

    public static void LoadAssetBundle()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "overrideScripts.Assets.caspuinox";

        using (Stream stream = assembly.GetManifestResourceStream(resourceName))
        {
            if (stream == null)
            {
                Debug.LogError("Failed to load the Asset Bundle!");
                return;
            }

            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            loadedAssetBundle = AssetBundle.LoadFromMemory(buffer);
        }

        if (loadedAssetBundle == null)
        {
            Debug.LogError("Failed to create Asset Bundle from memory!");
        }
        else
        {
            Debug.Log("Asset Bundle loaded successfully!");
        }
    }


    public static void InstantiatePrefab(string prefabName)
    {
        if (!IsLoaded)
        {
            Debug.LogError("Asset Bundle not loaded.");
            return;
        }

        GameObject prefab = loadedAssetBundle.LoadAsset<GameObject>(prefabName);
        if (prefab != null)
        {
            Instantiate(prefab);
            Debug.Log($"Prefab '{prefabName}' instantiated successfully.");
        }
        else
        {
            Debug.LogError($"Failed to load prefab: {prefabName}");
        }
    }

    public static void OnDestroy()
    {
        if (loadedAssetBundle != null)
        {
            loadedAssetBundle.Unload(true);
            Debug.Log("Asset Bundle unloaded successfully!");
        }
    }


    public static GameObject InstantiatePrefab(string prefabName, Vector3 position, Quaternion rotation)
    {
        if (!IsLoaded)
        {
            Debug.LogError("Asset Bundle not loaded.");
            return null;
        }

        GameObject prefab = loadedAssetBundle.LoadAsset<GameObject>(prefabName);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab, position, rotation);
            Debug.Log($"Prefab '{prefabName}' instantiated successfully at {position}.");
            return instance;
        }
        else
        {
            Debug.LogError($"Failed to load prefab: {prefabName}");
            return null;
        }
    }

    public static GameObject InstantiatePrefab1(string prefabName)
    {
        if (!IsLoaded)
        {
            Debug.LogError("Asset Bundle not loaded.");
            return null;
        }

        GameObject prefab = loadedAssetBundle.LoadAsset<GameObject>(prefabName);
        if (prefab != null)
        {
            GameObject instance = Instantiate(prefab);
            Debug.Log($"Prefab '{prefabName}' instantiated successfully.");
            return instance;
        }
        else
        {
            Debug.LogError($"Failed to load prefab: {prefabName}");
            return null;
        }
    }

    public static void DebugPrefabComponents(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Provided prefab is null.");
            return;
        }

        Debug.Log($"Inspecting components on prefab: {prefab.name}");

        // Checking for MeshFilter component and logging mesh details
        MeshFilter meshFilter = prefab.GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            Debug.Log($"MeshFilter found with mesh: {meshFilter.mesh.name}");
        }
        else
        {
            Debug.LogError("MeshFilter component is missing or contains no mesh.");
        }

        // Checking for Renderer component and logging material details
        Renderer renderer = prefab.GetComponent<Renderer>();
        if (renderer != null && renderer.sharedMaterials != null && renderer.sharedMaterials.Length > 0)
        {
            Debug.Log($"Renderer found with material count: {renderer.sharedMaterials.Length}");
            foreach (Material mat in renderer.sharedMaterials)
            {
                Debug.Log($"Material: {mat.name}");
            }
        }
        else
        {
            Debug.LogError("Renderer component is missing or contains no materials.");
        }
    }
}
