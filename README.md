# Pocket6

Pocket6 is a smartphone application using ARKit to turn regular iPhones into spatial input controllers for distant displays.

## Features

* **6DOF hand motion + touch inputs:** Users can interact by performing 3D hand movements in 6DOF (translation + rotation) and can reliably segment and further enhance their gestures by touchscreen inputs.

* **Subtle gestures:** Users can perform subtle hand motions within a small control space (e.g. 16 × 9 × 16 cm). This makes the gestural interaction more comfortable since users do not need to perform large hand motions.

* **Unconstrained user mobility:** Users can freely and fluently change their position, orientation, or posture while using the system. An auto-calibration algorithm will adapt the control space so that the user will stay correctly mapped to the distant display application (no user-initiated calibration required).

* **No specialized hardware required:** No external hardware, as external trackers or cameras, are required.

<img src="https://media.giphy.com/media/WOIZJV7Lyq4vCltyfj/giphy.gif" width="400">

*Interior Designer* demo, included in the project.

<img src="https://media.giphy.com/media/hrEtBeyw5ZiC82w79m/giphy.gif" width="400">

Another demo, currently not included in the project.

<img src="https://media.giphy.com/media/SS9KjJ5BleAE6kZWsW/giphy.gif" width="400">

Automated control space remapping.


## Quickstart guide

**Distant display app**
* Clone or download the repository.
* Start Unity and open the *Pocket6 Distant Display App* project.
* Open the *MainScene* from the *Scenes* folder.
* Press *Play*.
* Click *Start server* in the app and let it run.
* Proceed with the Pocket6 smartphone app.

**Smartphone app**
* Open the *Pocket6 Smartphone App* project in another Unity instance.
* Select iOS as the target platform under File > Build Settings > Platform >  iOS > click *Switch Platform*.
* Open the *MainScene* from the *Scenes* folder.
* Change the resolution of the *Game* window by selecting *Phone X/XS 2436x1125 Portrait*.
* Build the iOS app under File > Build Settings > Build > Create folder > Choose a name > click *Save*.
* In the created folder select *Unity-iPhone.xcodeproj* and open it in Xcode.
* Connect your iPhone to your Mac and select it as the build *Device*.
* In Xcode project settings under *Signing & Capabilities* select your Apple Developer Signing *Team* ID, *Bundle Identifier*, and press *Play*.
* Open the Pocket6 app on your iPhone.
* Enter the network address (IP) of the distant display Mac/PC. You can find it in the UI of the distant display app. Make sure, that both devices are in the same local network.
* Click *Connect*.
* Interact by moving your device-holding hand and observe the cursor on the distant display. Point at a furniture piece and press with your finger on the touchscreen to grab it. Alternatively, you can swipe your finger left or right to rotate the furniture piece.
* Use *Change view* on the smartphone, as well as the distant display, to see under the hood of Pocket6.

Advanced users, feel free to test various control space sizes and aspect ratios. These settings can be adjusted in the *Pocket6 Logic* game object in the smartphone app project.

## Compatibility
The project was made and tested in Unity 2019.1.5f1, Xcode 11.2.1, iPhone X and iOS 13.3.

System requirements:
* iPhone with ARKit capabilities
* Unity
* Xcode
* Mac or PC

Acknowledgement: we use [SAFullBodyIK](https://github.com/Stereoarts/SAFullBodyIK) to manipulate the humanoid rig.

## Improvement ideas
* The deprecated Unity HLAPI networking can be changed by [Mirror](https://github.com/vis2k/Mirror).
* Improve network lag by using [SmoothSync](https://assetstore.unity.com/packages/tools/network/smooth-sync-96925).
* Improve input precision by using the [1€ filter](https://github.com/DarioMazzanti/OneEuroFilterUnity).
* Save the network addresses using the device's internal storage or [Easy Save](https://assetstore.unity.com/packages/tools/input-management/easy-save-the-complete-save-load-asset-768).

## More information
This work is based on a publication [**"Pocket6: A 6DoF Controller Based On A Simple Smartphone Application"**](https://doi.org/10.1145/3267782.3267785 "Pocket6 publication") in the ACM SUI '18: Proceedings of the Symposium on Spatial User Interaction 2018. The publication also includes related work, user studies, applications, and future work directions.

------

Copyright (C) 2020 Teo Babic
