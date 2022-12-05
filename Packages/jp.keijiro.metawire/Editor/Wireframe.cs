using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Metawire {

[System.Serializable]
public sealed class Wireframe
{
    public Mesh Source = null;

    public void Generate(Mesh mesh)
    {
        mesh.vertices = Source.vertices;

        var pairs = new HashSet<(int i1, int i2)>();

        for (var sub = 0; sub < Source.subMeshCount; sub++)
        {
            var indices = Source.GetIndices(sub);

            for (var i = 0; i < indices.Length; i += 3)
            {
                var i1 = indices[i + 0];
                var i2 = indices[i + 1];
                var i3 = indices[i + 2];

                pairs.Add(i1 < i2 ? (i1, i2) : (i2, i1));
                pairs.Add(i2 < i3 ? (i2, i3) : (i3, i2));
                pairs.Add(i1 < i3 ? (i1, i3) : (i3, i1));
            }
        }

        var flat = pairs.Select(i => new[]{i.i1, i.i2}).SelectMany(i => i);
        mesh.SetIndices(flat.ToList(), MeshTopology.Lines, 0);
    }
}

} // namespace Metawire
