using System;

namespace JTNet.Types
{
  ///<summary>A range expressed as two signed 32bit integers.</summary>
  public class Int32Range {

    public int min;
    public int max;

    
    public override String ToString() {
        return "Range [ " + min.ToString() + " , " + max.ToString() + " ]";
    }

  }

}
