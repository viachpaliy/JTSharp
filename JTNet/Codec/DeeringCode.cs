using System;

namespace JTNet.Codec
{
  public class DeeringCode {

    long sextant;
    long octant;
    long theta;
    long psi;

    public DeeringCode(long sextant, long octant, long theta, long psi) {
        this.sextant = sextant;
        this.octant = octant;
        this.theta = theta;
        this.psi = psi;
    }

  }

}
