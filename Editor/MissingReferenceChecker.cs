using UnityEditor;
using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// Missingのチェッククラス
    /// </summary>
    public partial class MissingReferenceChecker
    {
        /// <summary>
        /// チェック処理
        /// </summary>
        /// <param name="obj">オブジェクト</param>
        /// <returns>Missing情報</returns>
        public static MissingReferenceDetail IsExistMissingReference(GameObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            MissingReferenceDetail detail = new MissingReferenceDetail();
            var components = obj.GetComponentsInChildren<Component>(true);
            if (components != null)
            {
                foreach (var c in components)
                {
                    SerializedObject so = new SerializedObject(c);
                    SerializedProperty sp = so.GetIterator();
                    while (sp.NextVisible(true))
                    {
                        if (IsMissing(sp))
                        {
                            // GameObjectの情報
                            string rootPath = GetRootPath(c.gameObject);
                            GameObjectDetail gDetail = detail.gameObjectDetails.Find(x => x.rootPath == rootPath);
                            if (gDetail == null)
                            {
                                gDetail = new GameObjectDetail(rootPath);
                                detail.gameObjectDetails.Add(gDetail);
                            }

                            // コンポーネントの情報
                            string compName = c.GetType().Name;
                            ComponentDetail cDetail = gDetail.components.Find(x => x.componentName == compName);
                            if (cDetail == null)
                            {
                                cDetail = new ComponentDetail(compName);
                                gDetail.components.Add(cDetail);
                            }

                            // 配列などの場合変数名ではなく「data」や「element」と表示されてしまうためpropertyPathを表示(「hoge.Array.data[1]」のようなな表示)
                            cDetail.properties.Add(sp.propertyPath);
                        }
                    }
                }
            }

            return detail;
        }

        /// <summary>
        /// プロパティがMissing状態かチェック
        /// </summary>
        /// <param name="p">プロパティ</param>
        /// <returns>Missingであるか</returns>
        private static bool IsMissing(SerializedProperty p)
        {
            if (p.propertyType != SerializedPropertyType.ObjectReference)
            {
                return false;
            }
            if (p.objectReferenceValue != null)
            {
                return false;
            }
            if (!p.hasChildren)
            {
                return false;
            }
            if (p.objectReferenceValue == null && p.objectReferenceInstanceIDValue == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// RootオブジェクトまでのPathを取得する
        /// </summary>
        private static string GetRootPath(GameObject obj)
        {
            var path = obj.name;
            var parent = obj.transform.parent;

            while (parent != null)
            {
                path = parent.name + "/" + path;
                parent = parent.parent;
            }

            return path;
        }
    }
}
