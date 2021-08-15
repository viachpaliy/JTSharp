using System;

namespace JTNet.Types
{
  public class BBoxF32 {

    public CoordF32 minCorner;
    public CoordF32 maxCorner;

    public BBoxF32(CoordF32 minCorner, CoordF32 maxCorner) {
        
        this.minCorner = minCorner;
        this.maxCorner = maxCorner;
    }

    
    public override String ToString() {
        return "Min: " + minCorner.ToString() + " - Max: " + maxCorner.ToString();
    }

  }
}
