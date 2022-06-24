using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Grid
{
    public Vector3 Size = new Vector3(1, 1, 1);
    public Vector3Int Subdivision = new Vector3Int(5, 5, 5);

    IEnumerable<Vector3> BuildComb(int cx, int cy)
      => Enumerable.Range(0, (cx + 1) * (cy + 1))
          .Select(i => new Vector2Int(i % (cx + 1), i / (cx + 1)))
          .Select(i => new Vector2((float)i.x / cx - 0.5f, (float)i.y / cy - 0.5f))
          .Select(v => new [] { new Vector3(v.x, v.y, -0.5f), new Vector3(v.x, v.y, +0.5f) })
          .SelectMany(v => v);

    public void Generate(Mesh mesh)
    {
        var gz = BuildComb(Subdivision.x, Subdivision.y);
        var gy = BuildComb(Subdivision.z, Subdivision.x).Select(v => new Vector3(v.y, v.z, v.x));
        var gx = BuildComb(Subdivision.y, Subdivision.z).Select(v => new Vector3(v.z, v.x, v.y));
        var vtx = gx.Concat(gy).Concat(gz).Select(v => Vector3.Scale(v, Size)).ToList();

        var tx = Enumerable.Repeat(new Vector4(1, 0, 0, 1), (Subdivision.y + 1) * (Subdivision.z + 1) * 2);
        var ty = Enumerable.Repeat(new Vector4(0, 1, 0, 1), (Subdivision.z + 1) * (Subdivision.x + 1) * 2);
        var tz = Enumerable.Repeat(new Vector4(0, 0, 1, 1), (Subdivision.x + 1) * (Subdivision.y + 1) * 2);
        var tan = tx.Concat(ty).Concat(tz);

        var idx = Enumerable.Range(0, vtx.Count / 2)
          .Select(i => new[]{i * 2, i * 2 + 1}).SelectMany(i => i);

        mesh.SetVertices(vtx);
        mesh.SetTangents(tan.ToList());
        mesh.SetIndices(idx.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
