using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;

namespace Isshi777
{
    /// <summary>
    /// AssetPostprocessorにより修正などがあったPrefabとSceneのMissingチェックをする（Sceneはセーブ時にする）
    /// </summary>
    class MissingCheckAssetPostProcesser : AssetPostprocessor
    {
        /// <summary>
        /// シーン保存時にチェックする等にする
        /// </summary>
        [InitializeOnLoadMethod]
        private static void SetSceneSavedCallback()
        {
            EditorSceneManager.sceneSaved += CheckSceneMissingReference;
        }

        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            foreach (string str in importedAssets)
            {
                // Prefabの場合はMissingチェック
                if (str.EndsWith(".prefab"))
                {
                    UnityEngine.Object obj = AssetDatabase.LoadMainAssetAtPath(str);
                    CheckPrefabMissingReference(obj as GameObject);
                }
            }
        }

        /// <summary>
        /// PrefabのMissingチェックとログ表示
        /// </summary>
        private static void CheckPrefabMissingReference(GameObject prefab)
        {
            var detail = MissingReferenceChecker.CheckMissingReference(prefab);
            if (detail != null && !detail.IsNotMissing)
            {
                Debug.LogWarning($" MissingReferenceChecker(Prefab): {prefab.name} \n {JsonUtility.ToJson(detail)} ");
            }
        }

        /// <summary>
        /// シーンファイルのMissingチェックとログ表示
        /// </summary>
        private static void CheckSceneMissingReference(Scene scene)
        {
            MissingReferenceChecker.MissingReferenceDetail detail = new MissingReferenceChecker.MissingReferenceDetail();

            string sceneName = scene.name;
            var rootObjcts = scene.GetRootGameObjects();
            foreach (var obj in rootObjcts)
            {
                var d = MissingReferenceChecker.CheckMissingReference(obj);
                if (d != null && !d.IsNotMissing)
                {
                    detail.gameObjectDetails.AddRange(d.gameObjectDetails);
                }
            }

            if (!detail.IsNotMissing)
            {
                Debug.LogWarning($" MissingReferenceChecker(Scene) : {sceneName} \n {JsonUtility.ToJson(detail)} ");
            }
        }
    }
}