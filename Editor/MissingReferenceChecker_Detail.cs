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
            public List<GameObjectDetail> gameObjectDetails;

            /// <summary>
            /// Missing��������
            /// </summary>
            public bool IsNotMissing => this.gameObjectDetails.Count == 0;

            public MissingReferenceDetail()
            {
                this.gameObjectDetails = new List<GameObjectDetail>();
            }
        }

        /// <summary>
        /// GameObject�̏��
        /// </summary>
        [System.Serializable]
        public class GameObjectDetail
        {
            /// <summary>
            /// Root�܂ł�Path
            /// </summary>
            public string rootPath;

            /// <summary>
            /// �R���|�[�l���g��񃊃X�g
            /// </summary>
            public List<ComponentDetail> components;

            public GameObjectDetail(string rootPath)
            {
                this.rootPath = rootPath;
                this.components = new List<ComponentDetail>();
            }
        }

        /// <summary>
        /// �R���|�[�l���g���
        /// </summary>
        [System.Serializable]
        public class ComponentDetail
        {
            /// <summary>
            /// �R���|�[�l���g��
            /// </summary>
            public string componentName;

            /// <summary>
            /// Missing�̂���v���p�e�B���̃��X�g
            /// </summary>
            public List<string> properties;

            public ComponentDetail(string componentName)
            {
                this.componentName = componentName;
                this.properties = new List<string>();
            }
        }
    }
}
