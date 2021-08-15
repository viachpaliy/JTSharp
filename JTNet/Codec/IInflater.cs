using System;

namespace JTNet.Codec
{

  ///<summary>Interface for inflater, created by IInflaterFactory.</summary>
  public interface IInflater {
    
    ///<summary>Inits inner Inflater.</summary>
    public void init(bool nowrap);
    
    ///<returns>true when stream under inflater is empty, false otherwise</returns>
    public bool finished();
    
    ///<returns>true when input is needed.</returns>
    public bool needsInput();
    
   
    ///<summary>Sets input (byte array)</summary>
    ///<param> tmp bytearray, that contains data to inflate.</param>
    public void setInput(byte[] tmp);
    
    
    ///<summary>Inflates internal data.</summary>
    ///<param> p destination</param>
    ///<returns> number of bytes inflated</returns>
    ///<exception> DataFormatException </exception>
    public int inflate(byte[] p);//throws DataFormatException;
    
    
    ///<summary>Finishes all the underlying resources.</summary>
    public void end();
    
    
  }


}
