# Color-Hole-Clone
 A clone game of Color Hole 3D from GoodJob Games

* Unity 2019.3.15f1
* Project uses Zenject.
* Apk :  https://drive.google.com/file/d/1q-WXOxV7IR6KcwC-Ya43WC5rENxiEZz5/view?usp=sharing

## Changes

* Previously I have been using Singleton for PlayerData, currently I inject an object to every dependent parts via Zenject.
* Disabling objects sometimes causes problems. To fix this issue I use UniRx for Physics because it responds more accurately.