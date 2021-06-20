using System.Collections.Generic;

namespace Isshi777
{
    /// <summary>
    /// Missing�̃`�F�b�N�N���X(Missing��������ۂ̏ڍ׏��N���X)
    /// </summary>
    public partial class MissingReferenceChecker
    {
        /// <summary>
        /// �ڍ׃N���X
        /// </summary>
        [System.Serializable]
        public class MissingReferenceDetail
        {
            /// <summary>
            /// �R���|�[�l���g��񃊃X�g
            /// </summary>
            public List<ComponentInfo> components;

            /// <summary>
            /// Missing��������
            /// </summary>
            public bool IsNotMissing => this.components.Count == 0;

            public MissingReferenceDetail()
            {
                this.components = new List<ComponentInfo>();
            }
        }

        /// <summary>
        /// �R���|�[�l���g���
        /// </summary>
        [System.Serializable]
        public class ComponentInfo
        {
            /// <summary>
            /// �R���|�[�l���g�����Ă���I�u�W�F�N�g��
            /// </summary>
            public string objectName;

            /// <summary>
            /// �R���|�[�l���g��
            /// </summary>
            public string componentName;

            /// <summary>
            /// Missing�̂���v���p�e�B���̃��X�g
            /// </summary>
            public List<string> properties;

            public ComponentInfo(string objectName, string componentName)
            {
                this.objectName = objectName;
                this.componentName = componentName;
                this.properties = new List<string>();
            }
        }
    }
}
