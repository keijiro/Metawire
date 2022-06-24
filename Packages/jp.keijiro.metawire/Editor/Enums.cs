using UnityEngine;

namespace Metawire {

public enum Shape { Line, Quad, Circle, Box, Ticks, Grid }

public enum Axis { X, Y, Z }

public static class AxisExtensions
{
    public static Vector3 Get2ndAxis(this Axis axis)
      => axis switch {
          Axis.X => new Vector3(0, 0, 1),
          Axis.Y => new Vector3(1, 0, 0),
          _      => new Vector3(-1, 0, 0) 
      };

    public static Vector3 Get3rdAxis(this Axis axis)
      => axis switch {
          Axis.X => new Vector3(0, 1, 0),
          Axis.Y => new Vector3(0, 0, 1),
          _      => new Vector3(0, 1, 0) 
      };
}

} // namespace Metawire
