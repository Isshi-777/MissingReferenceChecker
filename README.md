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
普通のメッセージ＋Jsonの構成
```
MissingReferenceChecker(Prefab) : SamplePrefab 
 {
    "components": [
        {
            "objectName": "SamplePrefab",
            "componentName": "Image",
            "properties": [
                "m_Sprite"
            ]
        },
        {
            "objectName": "SamplePrefab",
            "componentName": "Test",
            "properties": [
                "spriteA",
                "spriteList.Array.data[1]",
                "testParam.sprite",
                "testParam.spriteList.Array.data[0]",
                "testParam.spriteList.Array.data[2]"
            ]
        },
        {
            "objectName": "Image2",
            "componentName": "Image",
            "properties": [
                "m_Sprite"
            ]
        }
    ]
}
 ```
