Metawire
========

**Metawire** is a modified version of [Metamesh]. It generates wireframe meshes
(line primitives) instead of surface meshes.

[Metamesh]: https://github.com/keijiro/Metamesh

At the moment, it only supports very basic primitive shapes.

- Line
- Quad
- Circle
- Box
- Ticks (group of short line segments)
- Grid lines
- Wireframe (mesh to wireframe converter)

How To Install
--------------

This package uses the [scoped registry] feature to resolve package
dependencies. Open the Package Manager page in the Project Settings window and
add the following entry to the Scoped Registries list:

- Name: `Keijiro`
- URL: `https://registry.npmjs.com`
- Scope: `jp.keijiro`

![Scoped Registry](https://user-images.githubusercontent.com/343936/162576797-ae39ee00-cb40-4312-aacd-3247077e7fa1.png)

Now you can install the package from My Registries page in the Package Manager
window.

![My Registries](https://user-images.githubusercontent.com/343936/162576825-4a9a443d-62f9-48d3-8a82-a3e80b486f04.png)

[scoped registry]: https://docs.unity3d.com/Manual/upm-scoped.html
