using System;

namespace JTNet.Codec
{
  ///<summary> C# instance of InflaterFactory
  /// Allows you to inject different Inflater implementations in runtime.
  ///</summary>
  public class InflaterFactory : IInflaterFactory {
    
    private static IInflaterFactory factory;
    
    public static void setInflaterFactory(IInflaterFactory factory) {
        InflaterFactory.factory = factory;
    }
    
    public static IInflaterFactory getInflaterFactoryInstance() {
        if(InflaterFactory.factory == null)
            InflaterFactory.factory = new InflaterFactory();
        return factory;
    }
    
    protected InflaterFactory() { }
    
    public IInflater createInflater() {
        return new InternalInflater();
    }
        
  }

}
