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
            public List<GameObjectDetail> gameObjectDetails;

            /// <summary>
            /// Missingが無いか
            /// </summary>
            public bool IsNotMissing => this.gameObjectDetails.Count == 0;

            public MissingReferenceDetail()
            {
                this.gameObjectDetails = new List<GameObjectDetail>();
            }
        }

        /// <summary>
        /// GameObjectの情報
        /// </summary>
        [System.Serializable]
        public class GameObjectDetail
        {
            /// <summary>
            /// RootまでのPath
            /// </summary>
            public string rootPath;

            /// <summary>
            /// コンポーネント情報リスト
            /// </summary>
            public List<ComponentDetail> components;

            public GameObjectDetail(string rootPath)
            {
                this.rootPath = rootPath;
                this.components = new List<ComponentDetail>();
            }
        }

        /// <summary>
        /// コンポーネント情報
        /// </summary>
        [System.Serializable]
        public class ComponentDetail
        {
            /// <summary>
            /// コンポーネント名
            /// </summary>
            public string componentName;

            /// <summary>
            /// Missingのあるプロパティ名のリスト
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
