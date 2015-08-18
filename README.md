## ExpeditionWindow
KanColleViewer プラグイン。遠征出し忘れが激しいので、専用ウィンドウ作った。

![MainWindow](https://cloud.githubusercontent.com/assets/1779073/9337483/8ef9b3ae-461a-11e5-8769-e520846daac0.png)

Windows 7 以降の TaskbarItemInfo を使用して、
* 一番最初に帰ってくる艦隊の遠征について、進行度を TaskbarItemInfo.ProgressValue で表示
![Taskbar](https://cloud.githubusercontent.com/assets/1779073/9337533/d938f1a0-461a-11e5-895d-c51f20bbdfd8.png)
* 帰投済み (残り時間 00:00:00) の艦隊がいる場合は、TaskbarItemInfo.ProgressState を Indeterminate を表示
* 第二艦隊以降で遠征未出撃の艦隊がいる場合は、TaskbarItemInfo.ProgressState を Pause で表示
* 第二艦隊以降で遠征に出撃している艦隊がいない場合は、TaskbarInfo.ProgressState を Indeterminate で表示
