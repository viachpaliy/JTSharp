using System;

namespace JTNet.Types
{
  ///<summary> Classes that wraps an int vector, used to represent a Unsigned int32 vector; 
  ///this class will only allow access using the get method that will perform the required conversion on demand and return a long. 
  /// If the data needs to be accessed multiple times, call the convertAllData method; that will perform the long conversion once. 
  ///</summary>
  public class U32Vector {

    
    private int[] content;
    private long[] contentLong;
    
    public U32Vector(int[] data) {
        content = data;
    }
    
    public long get(int index) {
        return content[index] & 0xFFFFFFFFL;
    }
    
    public int length() { return content.Length;}
    
    public long[] convertAllData () {
        if (contentLong==null){
            contentLong = new long[content.Length];
            for (int i=0;i< content.Length;i++){
                contentLong[i] = content[i] & 0xFFFFFFFFL;
            }
        }
        return contentLong;
    }
  }

}
