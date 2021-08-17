using System;
using System.IO;
using JTNet.Format;
using JTNet.Debug;
using System.Diagnostics;
using System.Collections.Generic;

namespace JTNet.Codec
{
  ///<summary> Int32 Probability Contexts data collection is a list of Probability 
  /// Context Tables. The Int32 Probability Contexts data collection is only 
  /// present for Huffman and Arithmetic CODEC Types. p.226</summary>
  public class Int32ProbCtxts {

    private ByteReader reader;
    long nbValueBits, minValue;
    int contextTableCount = 0;
    public Int32ProbCtxtTable[] probContextTables = new Int32ProbCtxtTable[0];
    int[] outOfBandValues;

    public long nbOccCountBits;
    
    public Dictionary<Object,long> assValues = new Dictionary<Object, long>();



    public int[] getOutOfBandValues() {
        return outOfBandValues;
    }

    public void setOutOfBandValues(int[] outOfBandValues) {
        this.outOfBandValues = outOfBandValues;
    }

    public Int32ProbCtxtTable[] getProbContextTables() {
        return probContextTables;
    }

    public int getContextTableCount() {
        return probContextTables.Length;
    }

    public Int32ProbCtxts(ByteReader reader) {
        this.reader = reader;
        outOfBandValues = new int[0];
    }

    public long getNbValueBits() {
        return nbValueBits;
    }

    public void setNbValueBits(long nbValueBits) {
        this.nbValueBits = nbValueBits;
    }

    public long getMinValue() {
        return minValue;
    }

    public void setMinValue(long minValue) {
        this.minValue = minValue;
    }

    /*
     * totalCount – Refers to the sum of the “Occurrence Count” values for all
     * the symbols associated with a Probability Context.
     */
    public int getTotalCount() {
        int totalCount = 0;

        foreach (Int32ProbCtxtTable probContextTable in probContextTables)
            totalCount += probContextTable.getTotalCount();

        return totalCount;
    }

    // Returns the probability context for a given index
    public Int32ProbCtxtTable getContext(int ctxt) {
        return probContextTables[ctxt];
    }

    public void read(int codecType){ /// throws IOException {
        contextTableCount = reader.readU8();

        if (DebugInfo.debugCodec) {
            Console.WriteLine("\n == Probability Context ==\n");
            Console.WriteLine("Prob Context Table Count: " + contextTableCount.ToString());
        }

        BitReader bitReader = new BitReader(reader.inflated);

        probContextTables = new Int32ProbCtxtTable[contextTableCount];

        for (int i = 0; i < contextTableCount; i++) {
            probContextTables[i] = new Int32ProbCtxtTable(this, bitReader);
            probContextTables[i].read(i == 0, codecType);
        }

        // Alignments bits : See rev-D specification on page 228
        if (bitReader.getNbBitsLeft() > 0) {
            int aligmentsBits = bitReader.getBitBuf().readAsInt(
                    bitReader.getNbBitsLeft());
            if (aligmentsBits != 0)
                Console.WriteLine("Problem with alignments bits in the parsing of a probabilistic context table");
        }
    }

  }
}
