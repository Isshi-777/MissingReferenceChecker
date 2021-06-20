using System.Collections.Generic;

namespace Isshi777
{
    /// <summary>
    /// Missingのチェッククラス(Missingが会った際の詳細情報クラス)
    /// </summary>
    public partial class MissingReferenceChecker
    {
        /// <summary>
        /// 詳細クラス
        /// </summary>
        [System.Serializable]
        public class MissingReferenceDetail
        {
            /// <summary>
            /// コンポーネント情報リスト
            /// </summary>
            public List<ComponentInfo> components;

            /// <summary>
            /// Missingが無いか
            /// </summary>
            public bool IsNotMissing => this.components.Count == 0;

            public MissingReferenceDetail()
            {
                this.components = new List<ComponentInfo>();
            }
        }

        /// <summary>
        /// コンポーネント情報
        /// </summary>
        [System.Serializable]
        public class ComponentInfo
        {
            /// <summary>
            /// コンポーネントがついているオブジェクト名
            /// </summary>
            public string objectName;

            /// <summary>
            /// コンポーネント名
            /// </summary>
            public string componentName;

            /// <summary>
            /// Missingのあるプロパティ名のリスト
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
