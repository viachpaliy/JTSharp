using NUnit.Framework;
using JTNet.Types;
using JTNet.Format;
using System;
using System.IO;

namespace JTNet.UnitTests
{
    [TestFixture]	
    public class BitBufferTest
    {
    	[TestCase(0)]
    	[TestCase(1)]
    	[TestCase(2)]
    	[TestCase(3)]
    	[TestCase(4)]
    	[TestCase(5)]
    	[TestCase(6)]
    	[TestCase(7)]
    	[TestCase(8)]
    	[TestCase(9)]
        public void readAsLongTestN0(int i)
        {
          byte[] arr = new byte[10]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
          MemoryStream ms = new MemoryStream(arr);
          BitBuffer buffer = new BitBuffer(ms);
          Assert.AreEqual((long)i,buffer.readAsLong((long)i*8,8));
        }
        
        
    
    }
 }
