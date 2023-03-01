using UnityEngine;
using System.Collections.Generic;

namespace Metawire {

[System.Serializable]
public sealed class Grid
{
    public Vector3 Size = new Vector3(1, 1, 1);
    public Vector3Int Subdivision = new Vector3Int(5, 5, 5);

    public void Generate(Mesh mesh)
    {
        var vtx = new List<Vector3>();
        var uv0 = new List<Vector3>();

        for (var iz = 0; iz <= Subdivision.z; iz++)
        {
            var w = (float)iz / Subdivision.z;
            for (var iy = 0; iy <= Subdivision.y; iy++)
            {
                var v = (float)iy / Subdivision.y;
                for (var ix = 0; ix <= Subdivision.x; ix++)
                {
                    var u = (float)ix / Subdivision.x;
                    var t = new Vector3(u, v, w);
                    vtx.Add(Vector3.Scale(t - Vector3.one * 0.5f, Size));
                    uv0.Add(t);
                }
            }
        }

        var idx = new List<int>();

        var i = 0;
        for (var iz = 0; iz <= Subdivision.z; iz++)
        {
            for (var iy = 0; iy <= Subdivision.y; iy++)
            {
                for (var ix = 0; ix < Subdivision.x; ix++)
                {
                    idx.Add(i);
                    idx.Add(i + 1);
                    i++;
                }
                i++;
            }
        }

        i = 0;
        for (var iz = 0; iz <= Subdivision.z; iz++)
        {
            for (var iy = 0; iy < Subdivision.y; iy++)
            {
                for (var ix = 0; ix <= Subdivision.x; ix++)
                {
                    idx.Add(i);
                    idx.Add(i + Subdivision.x + 1);
                    i++;
                }
            }
            i += Subdivision.x + 1;
        }

        i = 0;
        for (var iz = 0; iz < Subdivision.z; iz++)
        {
            for (var iy = 0; iy <= Subdivision.y; iy++)
            {
                for (var ix = 0; ix <= Subdivision.x; ix++)
                {
                    idx.Add(i);
                    idx.Add(i + (Subdivision.x + 1) * (Subdivision.y + 1));
                    i++;
                }
            }
        }

        mesh.SetVertices(vtx);
        mesh.SetUVs(0, uv0);
        mesh.SetIndices(idx, MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
