# ScriptableObject-Architecture

A fork of https://github.com/DanielEverland/ScriptableObject-Architecture adding ScriptableObjectSystems, the CallbackDistributor, and the EditorAssistantAttribute and it's property drawer. Set up for PackageManager delivery directly from GitHub.


## Package Manager Installation

Simply modify your `manifest.json` file found at `/PROJECTNAME/Packages/manifest.json` by including the following line

```
{
	"dependencies": {
		...
		"com.github.cheesejedi.scriptableobject-architecture": "https://github.com/CheeseJedi/ScriptableObject-Architecture.git",
		...
	}
}
```


Makes using Scriptable Objects as a fundamental part of your architecture in Unity super easy

Based on Ryan Hipple's 2017 Unite talk https://www.youtube.com/watch?v=raQ3iHhE_Kk

# Features
- Automatic Script Generation
- Variables - All C# primitives
- Clamped Variables
- Variable References
- Typed Events
- Collections (Runtime Sets)
- Custom Icons
- ScriptableObjectSystem with CallbackDistributor for a wide range of callbacks
- EditorAssistant Attribute to assist in rapid 'wiring-up' in the editor

Visual debugging of events

![](https://i.imgur.com/GPP3aVR.gif)

Full stacktrace and editor invocation for events

![](https://i.imgur.com/S90VUWI.png)

Custom icons

![](https://i.imgur.com/simB0mK.png)

Easy and automatic script generation

![](https://i.imgur.com/xm2gNmo.png)
