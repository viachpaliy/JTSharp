using System;

namespace JTNet.Types
{
  ///<summary> 32 bit precision float coordinate, expressed as x,y,z.</summary>
  public class CoordF32 {

    public float x;
    public float y;
    public float z;

    public CoordF32(float x, float y, float z) {
        
        this.x = x;
        this.y = y;
        this.z = z;
    }

    
    public override String ToString() {
        return "(X: " + x.ToString() + " Y: " + y.ToString() + " Z: " + z.ToString() + ")";
    }
    
    public double[] getVectorDouble(){

        return  new double[] {x,y,z,};
    }

  }
}
