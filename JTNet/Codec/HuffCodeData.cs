using System;

namespace JTNet.Codec
{
  ///<summary>The data associated to each nodes.</summary>
  public class HuffCodeData {
	
   public int symbol;
   public int codeLen;
   public long bitCode;
   public int assValue;
   public int index;

    public HuffCodeData(int symbol, int assValue) {
        this.symbol = symbol;
        this.assValue = assValue;
    }

    public HuffCodeData(int symbol, int assValue, int codeLen, long bitCode) {
        this.symbol = symbol;
        this.assValue = assValue;
        this.codeLen = codeLen;
        this.bitCode = bitCode;
    }

    public int getSymbol() {
        return symbol;
    }

    public void setSymbol(int symbol) {
        this.symbol = symbol;
    }

    public long getCodeLen() {
        return Convert.ToInt64(codeLen);
    }

    public void setCodeLen(int codeLen) {
        this.codeLen = codeLen;
    }

    public long getBitCode() {
        return bitCode;
    }

    public void setBitCode(long bitCode) {
        this.bitCode = bitCode;
    }

    public String codeToString() {
        String tmp = Convert.ToString(bitCode,2);

        if (tmp.Length != codeLen) {
            // Add n "0" at the beginning where
            // n = codeLen - tmp.length()
            long nb_0 = codeLen - tmp.Length;
            for (int i = 0; i < nb_0; i++)
                tmp = "0" + tmp;
        }

        return tmp;

    }
    
    public override String ToString() {
        return String.Format("Code: {0} => Symbol: {1} ({2}) {3}", codeToString(), symbol.ToString(), assValue.ToString(), index.ToString());
    }

    public int getAssValue() {
        return assValue;
    }

    public void setAssValue(int assValue) {
        this.assValue = assValue;
    }

}

}
