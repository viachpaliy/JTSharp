using System;
using System.IO;
using JTNet.Format;
using JTNet.Debug;
using System.Diagnostics;
using System.Collections.Generic;

namespace JTNet.Codec
{
  ///<summary> A Probability Context Table is a trimmed and scaled histogram of the 
  /// input values. It tallies the frequencies of the several most frequently 
  /// occurring values. It is central to the operation of the arithmetic CODEC, 
  /// and gives all theinformation necessary to reconstruct the Huffman codes 
  /// for the Huffman CODEC. p.227 </summary>
  public class Int32ProbCtxtTable {

    private BitReader reader;
    Int32ProbCtxts probCtxt;

    long nbSymbolBits;
    long nbOccCountBits;
    long nbNextContextBits;

    Int32ProbCtxtEntry[] entries;

    public Int32ProbCtxtEntry[] getEntries() {
        return entries;
    }

    public Int32ProbCtxtTable(Int32ProbCtxts probCtxt, BitReader reader) {
        this.reader = reader;
        this.probCtxt = probCtxt;
    }

    public int getTotalCount() {
        int totalCount = 0;

        foreach (Int32ProbCtxtEntry entrie in entries)
            totalCount +=(int)entrie.getOccCount();

        return totalCount;
    }

    public void read(Boolean isFirstTable, int codecType) { ///throws IOException {

        // Header of the table
        long tableEntryCount = reader.readU32(32);
        nbSymbolBits = reader.readU32(6);
        nbOccCountBits = reader.readU32(6);

        probCtxt.nbOccCountBits = nbOccCountBits;

        if (isFirstTable)
            probCtxt.setNbValueBits(reader.readU32(6));

        nbNextContextBits = reader.readU32(6);

        if (isFirstTable)
            probCtxt.setMinValue(reader.readU32(32));

        if (DebugInfo.debugCodec) {
            Console.WriteLine("Prob Context Table Entry Count: "
                    + tableEntryCount.ToString());
            Console.WriteLine("Number symbol bits: " + nbSymbolBits.ToString());
            Console.WriteLine("Number occurence count bits: " + nbOccCountBits.ToString());
            Console.WriteLine("Number value bits: "+ probCtxt.getNbValueBits().ToString());
            Console.WriteLine("Number context bits: " + nbNextContextBits.ToString());
            Console.WriteLine("Min value: " + probCtxt.getMinValue().ToString());
        }

        // Probability Context Table Entries

        entries = new Int32ProbCtxtEntry[(int) tableEntryCount];
        long cumCount = 0;

        for (int i = 0; i < tableEntryCount; i++) {

            long symbol = reader.readU32((int) nbSymbolBits) - 2;
            long occCount = reader.readU32((int) nbOccCountBits);
            long associatedValue = 0;

            if (codecType == Int32Compression.HUFFMAN_CODEC)
                associatedValue = reader.readU32((int) probCtxt
                        .getNbValueBits());
            else if (codecType == Int32Compression.ARITHMETIC_CODEC)
            	// For the first table the associated value is read
                if (isFirstTable) {
                    associatedValue = reader.readU32((int) probCtxt
                            .getNbValueBits()) + probCtxt.getMinValue();
                    probCtxt.assValues.Add(symbol, associatedValue);
                // For the second table we take the associated value of the 
                // symbol in the first table
                } else {
                	associatedValue = (long)probCtxt.assValues[symbol];
                }

            int nextContext = (int) reader.readU32((int) nbNextContextBits);

            entries[i] = new Int32ProbCtxtEntry(symbol, occCount, cumCount,
                    associatedValue, nextContext);
            cumCount += occCount;

            if (DebugInfo.debugCodec)
                Console.WriteLine(String.Format("{0} => Symbol: {1}, Occurence Count: {2}, Cum Count : {3}, Associated Value: {4}, Next Context: {5}",
                                        i.ToString(), symbol.ToString(), occCount.ToString(),
                                        entries[i].getCumCount().ToString(),
                                        associatedValue.ToString(), nextContext.ToString()));

        }

    }

    public long getNbValueBits() {
        return probCtxt.getNbValueBits();
    }

    public long getMinValue() {
        return probCtxt.getMinValue();
    }

    // Looks up the index of the context entry that falls just above
    // the accumulated count.
    public Int32ProbCtxtEntry lookupEntryByCumCount(long count) {
        long sum = entries[0].getOccCount();
        int idx = 0;

        while (count >= sum) {
            idx += 1;
            sum += entries[idx].getOccCount();
        }

        return entries[idx];
    }
}

}
