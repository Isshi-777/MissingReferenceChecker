# MissingReferenceChecker

MissingReferenceをチェックする処理  
（GameObjectについているコンポーネントの各プロパティ）

## Sample
サンプルは本内容と同様にPackageManagerでインポート可能。 

●サンプル内容  
サンプルは下記の２つを作成  
　・PrefabとSceneの変更時にMissingチェックをしてログを表示  
　・Projectビューで選択したPrefabとSceneのMissingチェックをしてログを表示  
   
  　   
**下記のようなPrefabの場合**
![111](https://user-images.githubusercontent.com/36006543/122662682-d06a6080-d1cf-11eb-8a96-24fba06f823e.JPG)

 
**下記のようなログが表示される(実際にはJsonは整形されていない！)**  
普通のメッセージ（下記の最初の１行）＋Jsonの構成
```
MissingReferenceChecker(Prefab) : SamplePrefab 
 {
    "gameObjectDetails": [                      // MissingReferenceが発生しているGameObjectのリスト
        {
            "rootPath": "SamplePrefab",         // GameObjectのRootまでのPath（※このオブジェクトはRootObjectなので名前のみ）
            "components": [                     // MissingReferenceが発生しているコンポーネントのリスト
                {
                    "componentName": "Image",   // コンポーネント名
                    "properties": [             // MissingReferenceが発生しているプロパティ名のリスト
                        "m_Sprite"
                    ]
                },
                {
                    "componentName": "Test",
                    "properties": [
                        "spriteA",
                        "spriteList.Array.data[1]",
                        "testParam.sprite",
                        "testParam.spriteList.Array.data[0]",
                        "testParam.spriteList.Array.data[2]"
                    ]
                }
            ]
        },
        {
            "rootPath": "SamplePrefab/Image2",
            "components": [
                {
                    "componentName": "Image",
                    "properties": [
                        "m_Sprite"
                    ]
                }
            ]
        }
    ]
}
 ```
