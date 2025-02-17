# Mixed Reality Toolkit for Unity on Apple Vision Pro

## Fork Notes:
*By: Griffin Hurt &lt;griffhurt@pitt.edu&gt;*

I patched the input system for MRTK so that hand tracking works with the Apple Vision Pro. This really only involved editing the `UnityHandsSubsystem` to estimate the position of the palm. Technical details are below.
### How to Create a Project:
1. Make a copy of the `UnityProjects/ProjectBase` folder and rename it to your project's name.
2. Start Unity Hub, click "Add" in the top right corner, then click "Add project from disk"
3. Select the folder you just created (the copy of `ProjectBase`)
4. Open the project by clicking its name in Unity Hub

The "Main" scene contains the basic setup for an MR scene including necessary camera changes and an `ARSession`.

**Note:** *"Play To Device" unfortunately does not work with this implementation, as the project must be compiled with Metal to support the MRTK shaders. To test, build the Unity project and then deploy with Xcode.*
### Technical Details
When I first tried to get MRTK to run on the AVP, I figured interaction would just work out of the box because of the `UnityHandsSubsystem`, which uses the `XR Hands` package (supported by AVP). Unfortunately, `UnityHandsSubsystem` requires all the hand joints to be present, and the AVP does not provide the "palm" joint. To remedy this, I added code that estimates the position of the palm using the vectors from the "middle metacarpal" to the "ring proximal", "middle proximal", and "index proximal". It seems to work well enough for UI interactions and object manipulators.

The other change I had to make was fixing the `CameraSettingsManager` on the `Main Camera`. Opaque display headsets (like the AVP) default to having their backgrounds replaced with a skybox in MRTK. I changed that to be a solid color of `rgba(0, 0, 0, 0)` so MR works correctly on the AVP.
### Future Direction
Ideally I would like to add `SpatialPointer` support in the future so that interactions more consistent with other AVP apps can be implemented. [Another repo](https://github.com/jelmer3000/MixedRealityToolkit-Unity-PolySpatial/tree/wip-polyspatial-visionOS-support) has done something similar. Eventually, I'd like to submit this as a PR to the MRTK repository, but the code is far too experimental for that at this point.

![Mixed Reality Toolkit](./Images/MRTK_Unity_header.png)

![MRTK3 Banner](./Images/MRTK3_banner.png)

**MRTK3** is the third generation of the Mixed Reality Toolkit for Unity. It's an open source project designed to accelerate cross-platform mixed reality development in Unity. MRTK3 is built on top of [Unity's XR Interaction Toolkit (XRI)](https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.1/manual/index.html) and OpenXR. This new generation of MRTK is intended to be faster, cleaner, and more modular, with an easier cross-platform development workflow enabled by OpenXR and the Unity Input System.

## Key improvements

### Architecture

* Built on Unity XR Interaction Toolkit and the Unity Input System.
* Dedicated to OpenXR, with flexibility for other XRSDK backends
* Open-ended and extensible interaction paradigms across devices, platforms, and applications

### Performance

* Rewrote and redesigned most features and systems, from UX to input to subsystems.
* Zero per-frame memory allocation.
* Tuned for maximum performance on HoloLens 2 and other resource-constrained mobile platforms.

### UI

* New interaction models (gaze-pinch indirect manipulation).
* Updated Mixed Reality Design Language.
* Unity Canvas + 3D UX: production-grade dynamic auto-layout.
* Unified 2D & 3D input for gamepad, mouse, and accessibility support.
* Data binding for branding, theming, dynamic data, and complex lists.

## Requirements

MRTK3 requires Unity 2021.3.21 or higher. In addition, you need the [Mixed Reality Feature Tool for Unity](https://aka.ms/mrfeaturetool) to find, download, and add the packages to your project.

## Getting started

[Follow the documentation for setting up MRTK3 packages as dependencies in your project here.](https://learn.microsoft.com/windows/mixed-reality/mrtk-unity/mrtk3-overview/getting-started/setting-up/setup-new-project) Alternatively, you can clone this repo directly to experiment with our template project. However, we *strongly* recommend adding MRTK3 packages as dependencies through the Feature Tool, as it makes updating, managing, and consuming MRTK3 packages far easier and less error-prone.

## Supported devices

| Platform | Supported Devices |
|---|---|
| OpenXR devices | Microsoft HoloLens 2 <br> Magic Leap 2 <br> Meta Quest 1/2 <br> Windows Mixed Reality (experimental) <br> SteamVR (experimental) <br> Oculus Rift on OpenXR (experimental) <br> Varjo XR-3 (experimental) <br> **If your OpenXR device already works with MRTK3, let us know!**
| Windows | Traditional flat-screen desktop (experimental)
| Apple | Apple Vision Pro (experimental) |
| And more coming soon! |

## Versioning

In previous versions of MRTK (HoloToolkit and MRTK v2), all packages were released as a complete set, marked with the same version number (ex: 2.8.0). Starting with MRTK3 GA, each package will be individually versioned, following the [Semantic Versioning 2.0.0 specification](https://semver.org/spec/v2.0.0.html). (As a result, the '3' in MRTK3 is not a version number!)


Individual versioning will enable faster servicing while providing improved developer understanding of the magnitude of changes and reducing the number of packages needing to be updated to acquire the desired fix(es).

For example, if a non-breaking new feature is added to the UX core package, which contains the logic for user interface behavior the minor version number will increase (from 3.0.x to 3.1.0). Since the change is non-breaking, the UX components package, which depends upon UX core, is not required to be updated. 

As a result of this change, there is not a unified MRTK3 product version.

To help identify specific packages and their versions, MRTK3 provides an about dialog that lists the relevant packages included in the project. To access this dialog, select `Mixed Reality` > `MRTK3` > `About MRTK` from the Unity Editor menu.

![About MRTK Panel](Images/AboutMRTK.png)

## Early preview packages

Some parts of MRTK3 are at earlier stages of the development process than others. Early preview packages can be identified in the Mixed Reality Feature Tool and Unity Package Manager by the `Early Preview` designation in their names.

As of June 2022, the following components are considered to be in early preview.

| Name | Package Name |
| --- | --- |
| Accessibility | org.mixedrealitytoolkit.accessibility |
| Data Binding and Theming | org.mixedrealitytoolkit.data |

The MRTK team is fully committed to releasing this functionality. It is important to note that the packages may not contain the complete feature set that is planned to be released or they may undergo major, breaking architectural changes before release.

We very much encourage you to provide any and all feedback to help shape the final form of these early preview features.

## Contributing

This project welcomes contributions, suggestions, and feedback. All contributions, suggestions, and feedback you submitted are accepted under the [Project's license](./LICENSE.md). You represent that if you do not own copyright in the code that you have the authority to submit it under the [Project's license](./LICENSE.md). All feedback, suggestions, or contributions are not confidential.

For more information on how to contribute Mixed Reality Toolkit for Unity Project, please read [CONTRIBUTING.md](./CONTRIBUTING.md).

## Governance

For information on how the Mixed Reality Toolkit for Unity Project is governed, please read [GOVERNANCE.md](./GOVERNANCE.md).

All projects under the Mixed Reality Toolkit organization are governed by the Steering Committee. The Steering Committee is responsible for all technical oversight, project approval and oversight, policy oversight, and trademark management for the Organization. To learn more about the Steering Committee, visit this link: https://github.com/MixedRealityToolkit/MixedRealityToolkit-MVG/blob/main/org-docs/CHARTER.md
