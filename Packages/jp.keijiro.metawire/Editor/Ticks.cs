using UnityEngine;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Ticks
{
    public int Count = 32;

    public void Generate(Mesh mesh)
    {
        var seg = Enumerable.Range(0, Count);

        var vtx = seg
          .Select(i => new[]{new Vector3(i, 0, 0), new Vector3(i, 1, 0)})
          .SelectMany(v => v);

        var idx = seg
          .Select(i => new[]{i * 2, i * 2 + 1})
          .SelectMany(i => i);

        var uv0 = seg
          .Select(i => (float)i / Count)
          .Select(u => new[]{new Vector2(u, 0), new Vector2(u, 1)})
          .SelectMany(v => v);

        mesh.SetVertices(vtx.ToList());
        mesh.SetUVs(0, uv0.ToList());
        mesh.SetIndices(idx.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
