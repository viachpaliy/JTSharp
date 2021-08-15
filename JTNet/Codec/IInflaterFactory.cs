using System;

namespace JTNet.Codec
{
  ///<summary> Interface for factories of inflaters.</summary>
  public interface IInflaterFactory {
    
    public IInflater createInflater();
    
  }

}
