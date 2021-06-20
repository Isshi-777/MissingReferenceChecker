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
    /// AssetPostprocessor�ɂ��C���Ȃǂ�������Prefab��Scene��Missing�`�F�b�N������iScene�̓Z�[�u���ɂ���j
    /// </summary>
    class MissingCheckAssetPostProcesser : AssetPostprocessor
    {
        /// <summary>
        /// �V�[���ۑ����Ƀ`�F�b�N���铙�ɂ���
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
                // Prefab�̏ꍇ��Missing�`�F�b�N
                if (str.EndsWith(".prefab"))
                {
                    UnityEngine.Object obj = AssetDatabase.LoadMainAssetAtPath(str);
                    CheckPrefabMissingReference(obj as GameObject);
                }
            }
        }

        /// <summary>
        /// Prefab��Missing�`�F�b�N�ƃ��O�\��
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
        /// �V�[���t�@�C����Missing�`�F�b�N�ƃ��O�\��
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