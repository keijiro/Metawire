using UnityEngine;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Line
{
    public Vector3 From = Vector3.zero;
    public Vector3 To = Vector3.forward;
    public int Segments = 1;

    public void Generate(Mesh mesh)
    {
        var p = Enumerable.Range(0, Segments + 1)
          .Select(i => (float)i / Segments);

        var idx = Enumerable.Range(0, Segments)
          .Select(i => new[]{i, i + 1}).SelectMany(i => i);

        var vtx = p.Select(x => Vector3.Lerp(From, To, x));
        var uv0 = p.Select(x => Vector2.one * x);

        mesh.SetVertices(vtx.ToList());
        mesh.SetUVs(0, uv0.ToList());
        mesh.SetIndices(idx.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
