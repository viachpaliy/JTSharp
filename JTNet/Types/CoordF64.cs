using System;

namespace JTNet.Types
{
  ///<summary>64 bit precision float coordinate, expressed as x,y,z.</summary>
  public class CoordF64 {

    private double x;
    private double y;
    private double z;

    public CoordF64(double x, double y, double z) {
        
        this.x = x;
        this.y = y;
        this.z = z;
    }

    
    public override String ToString() {
        return "(X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString() + ")";
    }

  }

}
