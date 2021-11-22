# KoikatsuPmxExporterReverseEngineered

This is a reconstructed source code for the package PmxExporter version 1.2, whose author I don't know. It has been compiled and tested to work in the latest version of BepInEx.

Furthermore, I have removed some code that was causing it to conflict with other mods, namely BoneModX, because this mod used to delete all animators in the scene. It can now export custom characters using BoneModX.

## Releases
Compiled binaries are available in the [releases](https://github.com/Snittern/KoikatsuPmxExporterReverseEngineered/releases). To install, place the .dll in the BepInEx plugin folder.