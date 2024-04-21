using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;
using BepInEx.Configuration;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace overrideScripts
{
    public static class SharedState
    {
        public static ConfigEntry<KeyboardShortcut> toggleKey;
    }


    [BepInPlugin("com.casper.overridePlugin", "overridePlugin", "1.0.0")]
    public class overridePlugin : BaseUnityPlugin
    {
        private static readonly Harmony harmony = new Harmony("com.casper.overridePlugin");

        private void Awake()
        {

            // Load assets and configurations
            CreateConfigEntries();
            LoadAssets();

            // Apply Harmony patches
            ApplyPatches();

        }

        private void LoadAssets()
        {
            AssetBundleLoader.LoadAssetBundle();
            GameObject planterMk2Prefab = AssetBundleLoader.InstantiatePrefab1("GPUI_Plantermk2");
            GameObject equi = AssetBundleLoader.InstantiatePrefab1("test");

            if (planterMk2Prefab != null)
            {
                AssetBundleLoader.DebugPrefabComponents(planterMk2Prefab);
                Logger.LogInfo("Planter_Mk2 prefab successfully instantiated.");
            }
            else
            {
                Logger.LogError("Failed to instantiate the Planter_Mk2 prefab.");
            }
        }

        private void ApplyPatches()
        {
            harmony.PatchAll(typeof(GPUIManagerInitPatch));
            Logger.LogInfo("Harmony patches applied.");
        }

        private void CreateConfigEntries()
        {
            SharedState.toggleKey = Config.Bind("General", "Toggle GUI Key", new KeyboardShortcut(KeyCode.KeypadMinus), "Key to toggle the GUI.");
        }

        void Update()
        {
            if (SharedState.toggleKey.Value.IsDown())
            {
                // Toggle GUI visibility
            }

            if (Input.GetKeyDown(KeyCode.KeypadDivide)) // Example usage
            {
                Vector3 position = new Vector3(76.49f, 0.70f, -339.68f);
                Quaternion rotation = Quaternion.identity;
                AssetBundleLoader.InstantiatePrefab("GPUI_Plantermk2", position, rotation);
            }
        }

    }
}
