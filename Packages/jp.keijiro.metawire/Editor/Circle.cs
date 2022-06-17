using UnityEngine;
using System.Linq;

namespace Metawire {

[System.Serializable]
public class Circle
{
    public float Radius = 0.5f;
    public int Segments = 63;
    public Axis Axis = Axis.Z;

    public void Generate(Mesh mesh)
    {
        var p = Enumerable.Range(0, Segments + 1)
          .Select(i => (float)i / Segments);

        var idx = Enumerable.Range(0, Segments)
          .Select(i => new[]{i, (i + 1) % Segments}).SelectMany(i => i);

        var vx = Axis.Get2ndAxis() * Radius;
        var vy = Axis.Get3rdAxis() * Radius;
        var vtx = p.Select(x => x * Mathf.PI * 2)
          .Select(phi => vx * Mathf.Cos(phi) + vy * Mathf.Sin(phi));

        var uv0 = p.Select(x => Vector2.one * x);

        mesh.SetVertices(vtx.ToList());
        mesh.SetUVs(0, uv0.ToList());
        mesh.SetIndices(idx.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
