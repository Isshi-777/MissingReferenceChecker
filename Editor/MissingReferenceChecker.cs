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
                    ComponentInfo compInfo = null;

                    SerializedObject so = new SerializedObject(c);
                    SerializedProperty sp = so.GetIterator();
                    while (sp.NextVisible(true))
                    {
                        if (IsMissing(sp))
                        {
                            if (compInfo == null)
                            {
                                compInfo = new ComponentInfo(c.gameObject.name, c.GetType().Name);
                            }

                            // 配列などの場合変数名ではなく「data」や「element」と表示されてしまうためpropertyPathを表示(「hoge.Array.data[1]」のようなな表示)
                            compInfo.properties.Add(sp.propertyPath);
                        }
                    }

                    if (compInfo != null)
                    {
                        detail.components.Add(compInfo);
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
    }
}
