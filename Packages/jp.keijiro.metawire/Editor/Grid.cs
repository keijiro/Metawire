using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Grid
{
    public Vector3 Size = new Vector3(1, 1, 1);
    public Vector3Int Subdivision = new Vector3Int(5, 5, 5);

    IEnumerable<Vector2> BuildUVGrid(int cx, int cy)
      => Enumerable.Range(0, (cx + 1) * (cy + 1))
          .Select(i => new Vector2Int(i % (cx + 1),
                                      i / (cx + 1)))
          .Select(i => new Vector2((float)i.x / cx - 0.5f,
                                   (float)i.y / cy - 0.5f));

    IEnumerable<Vector2> InterleaveUVGrid(IEnumerable<Vector2> grid)
      => grid.Select(v => new[]{v, v}).SelectMany(v => v);

    IEnumerable<Vector3> ExtendUVGrid(IEnumerable<Vector2> grid)
      => grid
          .Select(v => new[]{new Vector3(v.x, v.y, -0.5f),
                             new Vector3(v.x, v.y, +0.5f)})
          .SelectMany(v => v);

    public void Generate(Mesh mesh)
    {
        var gx = BuildUVGrid(Subdivision.y, Subdivision.z);
        var gy = BuildUVGrid(Subdivision.z, Subdivision.x);
        var gz = BuildUVGrid(Subdivision.x, Subdivision.y);

        var ix = InterleaveUVGrid(gx);
        var iy = InterleaveUVGrid(gy);
        var iz = InterleaveUVGrid(gz);

        var ex = ExtendUVGrid(gx).Select(v => new Vector3(v.z, v.x, v.y));
        var ey = ExtendUVGrid(gy).Select(v => new Vector3(v.y, v.z, v.x));
        var ez = ExtendUVGrid(gz);

        var tx = Enumerable.Repeat(new Vector4(1, 0, 0, 1), ix.Count());
        var ty = Enumerable.Repeat(new Vector4(0, 1, 0, 1), iy.Count());
        var tz = Enumerable.Repeat(new Vector4(0, 0, 1, 1), iz.Count());

        var vtx = ex.Concat(ey).Concat(ez).Select(v => Vector3.Scale(v, Size));
        var uv0 = ix.Concat(iy).Concat(iz);
        var tan = tx.Concat(ty).Concat(tz);

        var idx = Enumerable.Range(0, vtx.Count() / 2)
          .Select(i => new[]{i * 2, i * 2 + 1}).SelectMany(i => i);

        mesh.SetVertices(vtx.ToList());
        mesh.SetTangents(tan.ToList());
        mesh.SetUVs(0, uv0.ToList());
        mesh.SetIndices(idx.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
