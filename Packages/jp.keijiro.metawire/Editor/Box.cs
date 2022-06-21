using UnityEngine;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Box
{
    public Vector3 Size = new Vector3(1, 1, 1);

    public void Generate(Mesh mesh)
    {
        var vtx = new [] { new Vector3(-1, -1, -1), new Vector3(+1, -1, -1),
                           new Vector3(-1, +1, -1), new Vector3(+1, +1, -1),
                           new Vector3(-1, -1, +1), new Vector3(+1, -1, +1),
                           new Vector3(-1, +1, +1), new Vector3(+1, +1, +1) };

        var uv0 = new [] { new Vector2(0, 0), new Vector2(1, 0),
                           new Vector2(0, 1), new Vector2(1, 1),
                           new Vector2(0, 0), new Vector2(1, 0),
                           new Vector2(0, 1), new Vector2(1, 1) };

        var idx = new [] { 0, 1, 1, 3, 3, 2, 2, 0,
                           4, 5, 5, 7, 7, 6, 6, 4,
                           0, 4, 1, 5, 2, 6, 3, 7 };

        var sized = vtx.Select(v => Vector3.Scale(v, Size) * 0.5f);

        mesh.SetVertices(sized.ToList());
        mesh.SetUVs(0, uv0);
        mesh.SetIndices(idx, MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
