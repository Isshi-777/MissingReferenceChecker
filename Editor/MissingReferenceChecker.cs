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
                    SerializedObject so = new SerializedObject(c);
                    SerializedProperty sp = so.GetIterator();
                    while (sp.NextVisible(true))
                    {
                        if (IsMissing(sp))
                        {
                            // GameObject�̏��
                            string rootPath = GetRootPath(c.gameObject);
                            GameObjectDetail gDetail = detail.gameObjectDetails.Find(x => x.rootPath == rootPath);
                            if (gDetail == null)
                            {
                                gDetail = new GameObjectDetail(rootPath);
                                detail.gameObjectDetails.Add(gDetail);
                            }

                            // �R���|�[�l���g�̏��
                            string compName = c.GetType().Name;
                            ComponentDetail cDetail = gDetail.components.Find(x => x.componentName == compName);
                            if (cDetail == null)
                            {
                                cDetail = new ComponentDetail(compName);
                                gDetail.components.Add(cDetail);
                            }

                            // �z��Ȃǂ̏ꍇ�ϐ����ł͂Ȃ��udata�v��uelement�v�ƕ\������Ă��܂�����propertyPath��\��(�uhoge.Array.data[1]�v�̂悤�Ȃȕ\��)
                            cDetail.properties.Add(sp.propertyPath);
                        }
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

        /// <summary>
        /// Root�I�u�W�F�N�g�܂ł�Path���擾����
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
