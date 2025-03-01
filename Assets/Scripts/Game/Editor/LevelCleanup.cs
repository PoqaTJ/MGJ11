using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    public class LevelCleanup
    {
        [MenuItem("Level/CountPlatformTypes")]
        private static void ClassifyLedges()
        {
            GameObject root = GameObject.Find("Level");
            if (root == null)
            {
                Debug.LogError("Couldn't find level root.");
            }

            var platforms = new List<GameObject>();
            for (int i = 0; i < root.transform.childCount; i++)
            {
                GameObject c = root.transform.GetChild(i).gameObject;
                if (c.name.StartsWith("Ledge"))
                {
                    platforms.Add(c);
                }
            }

            var platformCounts = new Dictionary<string, int>();
            foreach (var p in platforms)
            {
                string type = $"{p.transform.localScale.x},{p.transform.localScale.y}";
                if (platformCounts.ContainsKey(type))
                {
                    platformCounts[type] += 1;
                }
                else
                {
                    platformCounts[type] = 1;
                }
            }

            Debug.Log("Platform types:");
            foreach (var k in platformCounts.Keys)
            {
                Debug.Log($"Type {k}: {platformCounts[k]}");
            }
        }
    }
}