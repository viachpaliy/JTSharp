using System;
using System.IO;
using JTNet.Codec;

namespace JTNet.Format
{
  public class ByteReader {

    MemoryStream backBuffer;

    public MemoryStream inflated;

    IInflater inf;

    private bool inflating;

    public int compressedDataLength;

    public int compressedDataLeft;

    public int uncompressedRead = 0;

    int bitBufferLeft = 0;

    int bitBuffer = 0;

    int bitBufferLength = 0;

    long bitPosition = 0;

    byte[] bitByteBuff = new byte[4];

    public  byte MAJOR_VERSION;

    protected static readonly int[] BIT_MASK = new int[8];

    protected static readonly int[] FIELD_MASK = new int[8];
    
    public int readU8()
    {
    	int ch = 0;
    	return ch;
    }
  }
}
