using System;

namespace JTNet.Codec
{
  ///<summary>A table of sin/cos precomputed to optimize the calculation of normals.</summary>
  public class DeeringNormalLookupTable {

    long nBits;
    double[] cosTheta;
    double[] sinTheta;
    double[] cosPsi;
    double[] sinPsi;

    public DeeringNormalLookupTable() {
        int numberbits = 8;
        nBits = Math.Min(numberbits, 31);
        int tableSize =(1<<Convert.ToInt32(nBits));
        cosTheta = new double[tableSize + 1];
        sinTheta = new double[tableSize + 1];
        cosPsi = new double[tableSize + 1];
        sinPsi = new double[tableSize + 1];

        double psiMax = 0.615479709;

        double fTableSize = tableSize;

        for (int ii = 0; ii <= tableSize; ii++) {
            double theta = Math.Asin(Math.Tan(psiMax * (tableSize - ii)
                    / fTableSize));
            double psi = psiMax * ((ii) / fTableSize);
            cosTheta[ii] = Math.Cos(theta);
            sinTheta[ii] = Math.Sin(theta);
            cosPsi[ii] = Math.Cos(psi);
            sinPsi[ii] = Math.Sin(psi);
        }
    }

    public long numBitsPerAngle() {
        return nBits;
    }

    public DeeringLookupEntry lookupThetaPsi(long theta, long psi,
            long numberBits) {
        long offset = nBits - numberBits;

        long offTheta = (theta << Convert.ToInt32(offset)) & 0xFFFFFFFFL;
        long offPsi = (psi << Convert.ToInt32(offset)) & 0xFFFFFFFFL;

        return new DeeringLookupEntry(cosTheta[(int) offTheta],
                sinTheta[(int) offTheta], cosPsi[(int) offPsi],
                sinPsi[(int) offPsi]);
    }
  }

}
