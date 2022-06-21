using UnityEngine;

namespace Metawire {

[System.Serializable]
public sealed class Quad
{
    public Vector2 Size = new Vector2(1, 1);
    public Axis Axis = Axis.Z;

    public void Generate(Mesh mesh)
    {
        var vx = Axis.Get2ndAxis() * Size.x * 0.5f;
        var vy = Axis.Get3rdAxis() * Size.y * 0.5f;
        var vtx = new [] { -vx - vy, vx - vy, -vx + vy, vx + vy };

        var uv0 = new [] { new Vector2(0, 0), new Vector2(1, 0),
                           new Vector2(0, 1), new Vector2(1, 1) };

        var idx = new [] { 0, 1, 1, 3, 3, 2, 2, 0 };

        mesh.SetVertices(vtx);
        mesh.SetUVs(0, uv0);
        mesh.SetIndices(idx, MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
