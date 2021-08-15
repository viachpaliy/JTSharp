using System;
using System.IO;

namespace JTNet.Format
{
  public class BitBuffer {
  
  MemoryStream buffer;
    int bitBuffer; // Temporary i/o buffer
    int nBits; // Number of bits in bitBuffer

    int bitPos;
	
	

	public BitBuffer(MemoryStream buffer) {
		
		this.buffer = buffer;
		this.bitPos = 0;
		this.bitBuffer = 0x0000;
        this.nBits = 0;
	}

	
	public int getBitPos() {
		return bitPos;
	}
	
	
	public long getBitBufBitSize() {
		return this.buffer.Length * 8;
		
	}
	
	public long readAsLong(int nbBits) {
		return readAsLong(0, nbBits);
	}
	
	
	///<summary> Read specified number of bits (max 32) starting from the given bit 
	///position, return the value as long.</summary>
	public long readAsLong(long bPos, int nbBits) {
		long value = 0;
		long len = bPos + nbBits;
			
		// len = number of bits to read, we skip bPos bits and create a long
		// with nbBits bits
		while (len > 0) {
			// Not enough bits in the buffer => We read another byte
			if (this.nBits == 0) {
				bitBuffer = buffer.ReadByte();
				this.nBits = 8;
				bitBuffer &= 0xFF;
			}
			
			// This test skips the first bPos bits
			if (bPos == 0) {				
				value <<= 1;	
				// The value of the msb is added to the value result
				value |= (int) (bitBuffer >> 7);
			} else {
				bPos--;
			}
			// Remove the msb so the 2nd bit becomes the msb
			bitBuffer <<= 1;
			bitBuffer &= 0xFF;
			this.nBits--;
			len--;
			this.bitPos++;
	   }
		
		return value;
	}
	
	

	public int readAsInt(int nbBits) {
		return (int)readAsLong(nbBits);
	}

	public int readAsInt(long bitPos, int nbBits) {
		return (int)readAsLong(bitPos, nbBits);
	}

	

	public byte readAsByte(int nbBits) {
		return (byte)readAsLong(nbBits);
	}
  
  
  }
}
