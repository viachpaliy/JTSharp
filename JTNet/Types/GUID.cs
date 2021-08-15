using System;
using System.Text;
using Microsoft.Extensions.Primitives;

namespace JTNet.Types
{
  ///<summary>GUID are 16 bytes numbers used as unique identifiers in the JT system.</summary>
  public class GUID {

    ///<summary>Special GUID indicating the end of elements in a section of the Logical Scene graph.</summary>
    public static readonly GUID END_OF_ELEMENTS = fromString("{FFFFFFFF-FFFF-FFFF-FF-FF-FF-FF-FF-FF-FF-FF}");

    public static readonly int LENGTH = 16;
    public long w1;
    public int w2;
    public int w3;
    public int w4;
    public int w5;
    public int w6;
    public int w7;
    public int w8;
    public int w9;
    public int w10;
    public int w11;

        
    public override bool Equals(Object obj) {
        if (obj.GetType().IsInstanceOfType(typeof(String)))
            try {
                obj = fromString((String) obj);
            } catch (Exception e) {
                return false;
            }
        if (!(obj.GetType().IsInstanceOfType(typeof(GUID))))
            return false;
        GUID g = (GUID) obj;
        if (g.w1 == w1 && g.w2 == w2 && g.w3 == w3 && g.w4 == w4 && g.w5 == w5
                && g.w6 == w6 && g.w7 == w7 && g.w8 == w8 && g.w9 == w9
                && g.w10 == w10 && g.w11 == w11)
            return true;
        return false;
    }

   
    public override String ToString() {
        StringBuilder sb = new StringBuilder();
        sb.Append("{" + Convert.ToString(w1,16).ToUpper() + "-"
                + Convert.ToString(w2,16).ToUpper() + "-"
                + Convert.ToString(w3,16).ToUpper() + "-"
                + Convert.ToString(w4,16).ToUpper() + "-"
                + Convert.ToString(w5,16).ToUpper() + "-"
                + Convert.ToString(w6,16).ToUpper() + "-"
                + Convert.ToString(w7,16).ToUpper() + "-"
                + Convert.ToString(w8,16).ToUpper() + "-"
                + Convert.ToString(w9,16).ToUpper() + "-"
                + Convert.ToString(w10,16).ToUpper() + "-"
                + Convert.ToString(w11,16).ToUpper() + "}");

        return sb.ToString();
    }

    ///<summary>Converts a string representation of a GUID to a GUID.
    /// The String is expressed in the format {FFFFFFFF-FFFF-FFFF-FF-FF-FF-FF-FF-FF-FF-FF} with hexadecimal values.</summary>
    ///<param> s the String.</param>
    ///<returns> the corresponding GUID.</returns>
    public static GUID fromString(String s) {
        GUID g = new GUID();
        if (!s.StartsWith('{'))
            throw new ArgumentException();
	string[] st = s.Substring(1, s.Length - 2).Split('-');            
        if (st.Length != 11)
          throw new ArgumentException();
        g.w1 = Convert.ToInt64(st[0],(16));
        g.w2 = Convert.ToInt32(st[1], 16);
        g.w3 = Convert.ToInt32(st[2], 16);
        g.w4 = Convert.ToInt32(st[3], 16);
        g.w5 = Convert.ToInt32(st[4], 16);
        g.w6 = Convert.ToInt32(st[5], 16);
        g.w7 = Convert.ToInt32(st[6], 16);
        g.w8 = Convert.ToInt32(st[7], 16);
        g.w9 = Convert.ToInt32(st[8], 16);
        g.w10 = Convert.ToInt32(st[9], 16);
        g.w11 = Convert.ToInt32(st[10], 16);
        return g;
    }

    
    public override int GetHashCode() {
        return (int) (w1 + w2 + w3 + w4 + w5 + w6 + w7 + w8 + w9 + w10);

    }

 

}
}
