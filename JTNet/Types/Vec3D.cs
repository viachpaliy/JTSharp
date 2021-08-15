using System;

namespace JTNet.Types
{
  public class Vec3D {

    double x, y, z;

    public Vec3D(double x, double y, double z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    
    public override String ToString() {
        return String.Format("({0:F4},{1:F4}, {2:F4})", x, y, z);
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }

    public double getZ() {
        return z;
    }

    public float getXf() {
        return (float)x;
    }

    public float getYf() {
        return (float)y;
    }

    public float getZf() {
        return (float)z;
    }
  } 
}
