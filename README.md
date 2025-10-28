# pso2ngs-gpid

獲得GPが大きいGPIDを表示する。

    > pso2ngs-gpid.exe
    gpid0
    >

## 使い方

    pso2ngs-gpid.exe [オプション] [引数]

### オプション

-h, --help     ヘルプメッセージを表示する

-v, --version  コマンドのバージョンを表示する

-V, --verbose  詳細な処理情報を表示する

### 引数

日時 指定した日時での獲得GPが大きいGPIDを表示する。指定がない場合はコマンド実行日時を使用する。

## 環境変数

**PSO2NGSGPID_CYCLE**

GPIDの周期表の定義。形式は、繰り返し回数1,周期表のの断片1,繰り返し回数2,周期表のの断片2,……。

環境変数を設定していないときのデフォルト値:
13,0941852763674535496307218129,1,01

**PSO2NGSGPID_START**

GPIDの周期の始点。

環境変数を設定していないときのデフォルト値:
2023/06/07T04:00:00+0900

## ライセンス

MIT ライセンス

## 参考

[GPツリースケジュール](https://docs.google.com/spreadsheets/d/1KB9s8PgsmUI3M7RDirFBGo4FbY7z3Wo4NIs5LObZBHM/edit?usp=drive_link)
