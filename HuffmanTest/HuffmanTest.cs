using System;
using NUnit.Framework;


namespace HuffmanTest
{
    public class HuffmanTests
    {
        [Test]
        public void Encode_SimpleText_CorrectEncode()
        {
            string text = "it is test string";

            string actual = Huffman.Huffman.Encode(text);

            string expected = "111101101110011010010000101100010011111101100101";
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Decode_CorrectCode_CorrectDecode()
        {
            string text = "Hello World!";
            
            string encodedText = Huffman.Huffman.Encode(text);

            string actual = Huffman.Huffman.Decode(encodedText);
            
            string expected = text;
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Encode_EmptyText_EmptyEncodedText()
        {
            string text = "";

            string actual = Huffman.Huffman.Encode(text);

            string expected = "";
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Decode_InvalidCode_ExceptionExpected()
        {
            Huffman.Huffman.Encode("it is test string");
            
            string encodedText = "11110110111001101001000010110001001111110110010101"; //incorrect encoded text
            
            try
            {
                string actual = Huffman.Huffman.Decode(encodedText);
                
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Invalid encoded text entered", e.Message);
            }
        }
    }
}