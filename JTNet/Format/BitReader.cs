using System;
using System.IO;

namespace JTNet.Format
{
  ///<summary> Give a way to read a ByteBuffer "bit by bit".
  ///The ByteReader given at the creation is used to read bytes and the 
  /// readU32(nbBits) method read nbbits bits in the bytes read.
  ///</summary>
  public class BitReader
  {
    private MemoryStream reader;
    BitBuffer bitBuf;

    public BitReader(MemoryStream reader) {
        this.reader = reader;
        bitBuf = new BitBuffer(new MemoryStream());
    }

    public int getNbBitsLeft() {
        return (int) (bitBuf.getBitBufBitSize() - bitBuf.getBitPos());
    }

    /* Read an U32 encoded on nbBits bits */
    public long readU32(int nbBits) { ///throws IOException {
        if (nbBits == 0)
            return 0;

        int nbLeft = getNbBitsLeft();

        // If there are not enough bits already read and stored in bitBuf we 
        // read additional bytes and update the bitBuffer
        if (nbLeft < nbBits) {
            int nbBytes = ((nbBits - nbLeft - 1) / 8) + 1;
            int sizeBytes = nbBytes;
            int cpt = 0;

            if (nbLeft != 0)
                sizeBytes += 1;

            byte[] byteBuf = new byte[sizeBytes];

            if (nbLeft != 0) {
                byte remainingByte = bitBuf.readAsByte(nbLeft);
                byteBuf[cpt] = remainingByte;
                cpt += 1;
            }

            byte[] tmpBytes = new byte[nbBytes]; ///reader.readBytes(nbBytes);
            int count = 0;
            while(count < nbBytes)
		{
    		tmpBytes[count++] = (byte)reader.ReadByte();
		}

            for (int i = cpt; i < sizeBytes; i++)
                byteBuf[i] = tmpBytes[i - cpt];

            bitBuf = new BitBuffer(new MemoryStream(byteBuf));

        }

        // Read the int
        if (nbLeft > 0) {
            if (nbLeft < nbBits)
                return bitBuf.readAsInt(8 - nbLeft, nbBits);
            else
                return bitBuf.readAsInt(nbBits);
        } else {
            long res = bitBuf.readAsInt(nbBits);
            return res;
        }
    }

    public BitBuffer getBitBuf() {
        return bitBuf;
    }
  
  }
  
 }
