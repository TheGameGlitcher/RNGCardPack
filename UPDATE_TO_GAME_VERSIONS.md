This file contains the necessary changes to apply when the game version changes.

### 1.8.0

* AddWatermark is now deprecated. WatermarkWrapper.cs can be deleted. In BepinexPlugin.cs, the following lines have to be removed commented.

```c#
//line 21
[BepInDependency(AddWatermark.API.GUID, BepInDependency.DependencyFlags.SoftDependency)]

//line 83
if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey(AddWatermark.API.GUID))
                WatermarkWrapper.ActivateWatermark();
```

* CardConfig now has a new parameter called pack. In Config/DefaultConfig.cs -> DefaultCardConfig() add "Pack: null" anywhere in the config.

```c#
    Owner: OwnerName,
    ImageId: "",
//->
    ...
    Owner: OwnerName,
    Pack: null,
    ImageId: "",
    ...
```