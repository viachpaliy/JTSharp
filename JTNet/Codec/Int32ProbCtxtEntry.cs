using System;

namespace JTNet.Codec
{
  ///<summary>An entry is a row of a table. p.229 </summary>
  public class Int32ProbCtxtEntry {

    long occCount; // Number of occurrences
    long cumCount; // Cumulative number of occurrences

    long associatedValue;
    long symbol; // Symbol
    int nextContext; // Next context if this symbol seen

    public Int32ProbCtxtEntry(long symbol, long occCount, long cumCount,
            long associatedValue, int nextContext) {
        this.symbol = symbol;
        this.occCount = occCount;
        this.cumCount = cumCount;
        this.associatedValue = associatedValue;
        this.nextContext = nextContext;
    }

    public long getAssociatedValue() {
        return associatedValue;
    }

    public long getSymbol() {
        return symbol;
    }

    public long getCumCount() {
        return cumCount;
    }

    public long getOccCount() {
        return occCount;
    }

    public int getNextContext() {
        return nextContext;
    }

    
    public override String ToString() {
        return String.Format("{0} - {1}({2}) - => {3} - {4}", symbol.ToString(), occCount.ToString(),
                cumCount.ToString(), associatedValue.ToString(), nextContext.ToString());
    }

  }

}
