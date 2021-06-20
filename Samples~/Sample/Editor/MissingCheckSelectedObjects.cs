using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// Project�r���[�őI������Prefab��Scene��Missing�`�F�b�N�����ă��O��\������
    /// </summary>
    public class MissingCheckSelectedObjects
    {
        [MenuItem("Assets/MissingRefenceChecker", false)]
        private static void CheckMissingReference()
        {
            foreach (var guid in Selection.assetGUIDs)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);

                // Prefab�̏ꍇ
                if (path.EndsWith(".prefab"))
                {
                    GameObject prefab = AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
                    var detail = MissingReferenceChecker.IsExistMissingReference(prefab);
                    if (detail != null && !detail.IsNotMissing)
                    {
                        Debug.LogWarning($" MissingReferenceChecker(Prefab) : {prefab.name} \n {JsonUtility.ToJson(detail)} ");
                    }
                }
                else if (path.EndsWith(".unity")) // Scene�̏ꍇ
                {
                    // ���݊J���Ă���V�[����Path��ۑ�
                    var currentScenePath = EditorSceneManager.GetActiveScene().path;

                    MissingReferenceChecker.MissingReferenceDetail detail = new MissingReferenceChecker.MissingReferenceDetail();

                    var scene = EditorSceneManager.OpenScene(path);
                    var rootObjcts = scene.GetRootGameObjects();
                    foreach (var obj in rootObjcts)
                    {
                        var d = MissingReferenceChecker.IsExistMissingReference(obj);
                        if (d != null && !d.IsNotMissing)
                        {
                            detail.gameObjectDetails.AddRange(d.gameObjectDetails);
                        }
                    }

                    if(!detail.IsNotMissing)
                    {
                        Debug.LogWarning($" MissingReferenceChecker(Scene) : {scene.name} \n {JsonUtility.ToJson(detail)} ");
                    }

                    // �`�F�b�N�O�ɊJ���Ă����V�[���ɖ߂�
                    EditorSceneManager.OpenScene(currentScenePath);
                }
            }
        }
    }
}
