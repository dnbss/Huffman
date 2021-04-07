using System;

namespace Huffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter text: ");
            
            string text = Console.ReadLine();

            string encodedText = Huffman.Encode(text);

            Huffman.PrintTableCode();
            
            Console.WriteLine($"encoded text: {encodedText}");
            
            Console.WriteLine($"Decoded text: {Huffman.Decode(encodedText)}");
            
            Console.WriteLine($"Source text memory size: {text.Length * 8}");
            
            Console.WriteLine($"Encoded text memory size: {encodedText.Length}");
            
            Console.WriteLine($"Compression ratio: {Huffman.CompressionRatio(text, encodedText)}");
        }
    }
}