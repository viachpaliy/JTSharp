using System;
using System.IO.Compression;
using Ionic.Zlib;


namespace JTNet.Codec
{
  ///<summary>Default inflater.</summary>
  public class InternalInflater : IInflater {

    ZlibCodec inflater = new ZlibCodec();

    public void init(bool nowrap) {
        inflater.InitializeInflate();
    }

    public bool finished() {
        if (inflater.NextIn >= inflater.InputBuffer.Length)  return true;
        return false;
    }

    public bool needsInput() {
        if (inflater.InputBuffer == null) return true;
        if (inflater.InputBuffer.Length == 0) return true;
        return false;
    }

    public void setInput(byte[] tmp) {
        inflater.InputBuffer = tmp;
    }

    public int inflate(byte[] p){     /// throws DataFormatException {
    	if (p.Length < inflater.OutputBuffer.Length)
    		throw new Exception("DataFormatException");
    	for(int i = 0; i < inflater.OutputBuffer.Length; i++)
    	{
    	  p[i] = inflater.OutputBuffer[i];
    	}
        return inflater.OutputBuffer.Length;
    }

    public void end() {
        inflater.EndInflate();
    }
  }

}
