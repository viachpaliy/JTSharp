using System;
using JTNet.Types;

namespace JTNet.Codec
{
  ///<summary> The deering codec, transform the sextant, octant, theta, and psi values
  /// to a 3D Vector (the normal).</summary>
  public class DeeringNormalCodec {
	
    long numBits;

    
    static DeeringNormalLookupTable lookupTable;
    public DeeringNormalCodec() {
        if (lookupTable == null)
            lookupTable = new DeeringNormalLookupTable();
        numBits = 6;
    }

    public DeeringNormalCodec(long numberbits) {
         if (lookupTable == null)
            lookupTable = new DeeringNormalLookupTable();
        numBits = numberbits;
    }

    public Vec3D convertCodeToVec(long sextant, long octant, long theta,
            long psi) {

        // Size of code = 6+2*numBits, and max code size is 32 bits,
        // so numBits must be <= 13.
        // Code layout: [sextant:3][octant:3][theta:numBits][psi:numBits]

        double psiMax = 0.615479709;
        long bitRange =Convert.ToInt64(1 << Convert.ToInt32(numBits)); // 2^numBits
        double fBitRange = bitRange;

        // For sextants 1, 3, and 5, theta needs to be incremented
        theta += (sextant & 1);

       
        DeeringLookupEntry lookupEntry = lookupTable.lookupThetaPsi(theta, psi,
                numBits);

        double x, y, z;
        double xx = x = lookupEntry.getCosTheta() * lookupEntry.getCosPsi();
        double yy = y = lookupEntry.getSinPsi();
        double zz = z = lookupEntry.getSinTheta() * lookupEntry.getCosPsi();

        // Change coordinates based on the sextant
        switch ((int) sextant) {
        case 0: // No op
            break;
        case 1: // Mirror about x=z plane
            z = xx;
            x = zz;
            break;
        case 2: // Rotate CW
            z = xx;
            x = yy;
            y = zz;
            break;
        case 3: // Mirror about x=y plane
            y = xx;
            x = yy;
            break;
        case 4: // Rotate CCW
            y = xx;
            z = yy;
            x = zz;
            break;
        case 5: // Mirror about y=z plane
            z = yy;
            y = zz;
            break;
        }
        ;

        // Change some more based on the octant
        // if first bit is 0, negate x component
        if ((octant & 0x4) == 0)
            x = -x;

        // if second bit is 0, negate y component
        if ((octant & 0x2) == 0)
            y = -y;

        // if third bit is 0, negate z component
        if ((octant & 0x1) == 0)
            z = -z;

        return new Vec3D(x, y, z);
    }

    public DeeringCode unpackCode(long code) {
        long mask = ((1 <<Convert.ToInt32(numBits)) - 1) & 0xFFFFFFFF;

        return new DeeringCode((code >>Convert.ToInt32(numBits + numBits + 3)) & 0x7,
                (code >> Convert.ToInt32(numBits + numBits)) & 0x7,
                (code >> Convert.ToInt32(numBits)) & mask, (code) & mask);
    }

}

}
