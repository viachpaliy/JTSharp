using System;
using System.Collections.Generic;

namespace JTNet.Codec
{
  ///<summary>Used to assign codes to the nodes of the Huffman tree.</summary>
  public class HuffCodecContext {

    int length; // Used to tally up total encoded code length
    long code; // Code under construction
    int codeLength; // Length of Huffman code currently under construction.
    Dictionary<String, HuffCodeData> codes;

    public HuffCodecContext() {
        codes = new Dictionary<String, HuffCodeData>();
    }

    public void leftShift() {
        code = code << 1;
    }

    public void rightShift() {
        code = code >> 1;
    }

    public void bitOr(int value) {
        code = code | value;
    }

    public void incLength() {
        codeLength++;
    }

    public void decLength() {
        codeLength--;
    }

    public long getCode() {
        return code;
    }

    public int getCodeLen() {
        return codeLength;
    }

    public void add(HuffCodeData data) {
        codes.Add(data.codeToString(), data);
    }
    
    public void makeCanonical (){ 
        //long code = 0;
        List <HuffCodeData> e = new List<HuffCodeData>(codes.Values);
        e.Sort(CompareHuffCodeData);
        
        code = e[0].bitCode;
        //int length = e.get(0).codeLen;
        for (int i=0;i<e.Count;i++){
            HuffCodeData d1 = e[i];

        
//            String tmp = Long.toBinaryString(code);
//            if (tmp.length() != d.codeLen) {
//                long nb_0 = d.codeLen - tmp.length();
//                for (int k = 0; k < nb_0; k++)
//                    tmp = "0" + tmp;
//            }
//            System.out.println(tmp);

          //  int nextCL = (e.get(i+1<e.size()?i+1:i).codeLen);
            //code = (code  - 1 ) >> (nextCL  - ((int)d1.codeLen));
            if (i < e.Count -1 ) {
                HuffCodeData d2 = e[i+1];
                if (d1.index < d2.index && (Math.Abs(d1.assValue) == Math.Abs(d2.assValue))&& Math.Abs(d2.assValue) == 2) {
                    int t;
                    t = d1.assValue;
                    d1.assValue = d2.assValue;
                    d2.assValue = t;
                    t = d1.symbol;
                    d1.symbol = d2.symbol;
                    d2.symbol = t;
                    t = d1.index;
                    d1.index = d2.index;
                    d2.index = t;
                    
              //      System.out.println("swap");
                }
            }
            //System.out.println(d1);
        }
     }
     
     private static int CompareHuffCodeData(HuffCodeData o1, HuffCodeData o2) {
            int ret = (int)(o2.codeLen - o1.codeLen);
            if (ret == 0)
                 ret = (int)(o2.bitCode - o1.bitCode);
           return ret;
        }
        

    public void printCodes() {
        ///Enumeration<HuffCodeData> e = codes.elements();

        ///while (e.hasMoreElements())
            ///System.out.println(e.nextElement());
        foreach(var item in codes.Keys)
        {
        	Console.WriteLine(item);
        }
            
    }

}

}
