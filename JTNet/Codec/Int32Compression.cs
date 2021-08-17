using System;
using System.IO;
using JTNet.Format;
using JTNet.Debug;
using System.Diagnostics;
using System.Collections.Generic;

namespace JTNet.Codec
{
  public class Int32Compression {

    public enum CompressedDataPacket {
        Int32CDP, Float64CDP
    }

    public static readonly int NULL_CODEC = 0;
    public static readonly int BITLENGTH_CODEC = 1;
    public static readonly int HUFFMAN_CODEC = 2;
    public static readonly int ARITHMETIC_CODEC = 3;
   
  }
}
