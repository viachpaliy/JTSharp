using System;

namespace JTNet.Types
{

///<summary>A float representation of a colour.</summary>
public class RGBA {

    public float r, g, b, a;
    
    public float[] getColorTable() {
        float[] ret = new float[4];
        ret[0] = r;
        ret[1] = g;
        ret[2] = b;
        ret[3] = a;
        return ret;
    }

    public RGBA(float r, float g, float b, float a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public float getR() {
        return r;
    }

    public void setR(float r) {
        this.r = r;
    }

    public float getG() {
        return g;
    }

    public void setG(float g) {
        this.g = g;
    }

    public float getB() {
        return b;
    }

    public void setB(float b) {
        this.b = b;
    }

    public float getA() {
        return a;
    }

    public void setA(float a) {
        this.a = a;
    }
  }

}
