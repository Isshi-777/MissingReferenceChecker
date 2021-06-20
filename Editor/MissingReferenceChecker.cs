using UnityEditor;
using UnityEngine;

namespace Isshi777
{
    /// <summary>
    /// Missing�̃`�F�b�N�N���X
    /// </summary>
    public partial class MissingReferenceChecker
    {
        /// <summary>
        /// �`�F�b�N����
        /// </summary>
        /// <param name="obj">�I�u�W�F�N�g</param>
        /// <returns>Missing���</returns>
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

                            // �z��Ȃǂ̏ꍇ�ϐ����ł͂Ȃ��udata�v��uelement�v�ƕ\������Ă��܂�����propertyPath��\��(�uhoge.Array.data[1]�v�̂悤�Ȃȕ\��)
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
        /// �v���p�e�B��Missing��Ԃ��`�F�b�N
        /// </summary>
        /// <param name="p">�v���p�e�B</param>
        /// <returns>Missing�ł��邩</returns>
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
