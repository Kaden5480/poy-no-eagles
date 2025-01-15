using System;
using System.Reflection;

using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

#if BEPINEX

using BepInEx;

namespace NoEagles {
    [BepInPlugin("com.github.Kaden5480.poy-no-eagles", "NoEagles", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public void Awake() {
            Harmony.CreateAndPatchAll(typeof(Plugin.PatchEagles));
        }

#elif MELONLOADER

using MelonLoader;

[assembly: MelonInfo(typeof(NoEagles.Plugin), "NoEagles", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace NoEagles {
    public class Plugin: MelonMod {

#endif
        [HarmonyPatch(typeof(Mermaid), "LoadMermaidStuff")]
        static class PatchEagles {
            static void Postfix(Mermaid __instance) {
                for (int i = 1; i <= 5; i++) {
                    if ($"Eagle_{i}".Equals(__instance.gameObject.name) == false) {
                        continue;
                    }

                    if (PlayerPrefs.HasKey($"Eagle{i}") == false) {
                        continue;
                    }

                    __instance.eagleParentObj.SetActive(false);
                }
            }
        }

    }
}
