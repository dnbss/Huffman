using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices.ComTypes;
using Map;

namespace Huffman
{
    public static class Huffman
    {
        private static Map<string, int> tableFrequency;

        private static Map<string, string> tableCode;
        
        private static MinBinaryHeap minBinaryHeap;

        private static MaxBinaryHeap maxBinaryHeap;

        public static string Encode(string text)
        {
            CreateTableFrequencyAndCode(text);

            MinBinaryHeap minBinaryHeap = new MinBinaryHeap(tableFrequency);

            MaxBinaryHeap maxBinaryHeap = new MaxBinaryHeap();
            
            Queue<string> keys = tableFrequency.GetKeys();

            while (true)
            {
                Symbol min1 = null;
                
                Symbol min2 = null;

                try
                {
                    //FindTwoMin(out min1, out min2);

                    min1 = minBinaryHeap.RemoveMin();

                    min2 = minBinaryHeap.RemoveMin();
                }
                catch (Exception e)
                {
                    
                    break;
                }

                /*AddSymbolInCode(min1, "0");

                AddSymbolInCode(min2, "1");

                tableFrequency.Insert(min1 + min2, tableFrequency.Find(min1) + tableFrequency.Find(min2));

                tableFrequency.Remove(min1);

                tableFrequency.Remove(min2);*/
                
                min1.Code = "0";
                maxBinaryHeap.Insert(min1);

                min2.Code = "1";
                maxBinaryHeap.Insert(min2);
                
                minBinaryHeap.Insert(new Symbol() { Character = min1.Character + min2.Character, Code = "", Frequency = min1.Frequency + min2.Frequency });
                
                //maxBinaryHeap.Insert(new Symbol() {Character = min1.Character + min2.Character, Code = "", Frequency = min1.Frequency + min2.Frequency});
            }

            AddSymbolInCode(maxBinaryHeap);
            
            string encodedText = "";
            
            for (int i = 0; i < text.Length; i++)
            {
                encodedText += tableCode.Find(text[i].ToString());
            }

            return encodedText;
        }

        public static string Decode(string encodedText)
        {
            string decodedText = "";
            
            while (encodedText.Length != 0)
            {
                Queue<string> keys = tableCode.GetKeys();

                bool symbolFound = false;
                
                while (!keys.IsEmpty() && !symbolFound)
                {
                    string symbol = keys.Pop();
                    
                    string code = tableCode.Find(symbol);

                    if (code.Length <= encodedText.Length && encodedText.Substring(0, code.Length) == code)
                    {
                        encodedText = encodedText.Remove(0, code.Length);

                        decodedText += symbol;
                        
                        symbolFound = true;
                    }
                }

                if (!symbolFound)
                {
                    throw new Exception("Invalid encoded text entered");
                }
            }

            return decodedText;
        }
        
        private static void CreateTableFrequencyAndCode(string text)
        {
            tableFrequency = new Map<string, int>();

            tableCode = new Map<string, string>();

            for (int i = 0; i < text.Length; i++)
            {
                try
                {
                    tableFrequency.FindNode(text[i].ToString()).data++;
                }
                catch (Exception e)
                {
                    tableFrequency.Insert(text[i].ToString(), 1);
                    
                    tableCode.Insert(text[i].ToString(), "");
                }
            }
        }

        private static void FindTwoMin(out string min1, out string min2 )
        {
            Queue<string> keys = tableFrequency.GetKeys();

            Queue<int> frequencies = tableFrequency.GetValues();

            min1 = keys.Pop();

            if (keys.IsEmpty())
            {
                throw new Exception("There are fewer than two keys in the list");
            }
            
            min2 = keys.Pop();

            if (tableFrequency.Find(min1) > tableFrequency.Find(min2))
            {
                var t = min1;

                min1 = min2;

                min2 = t;
            }

            while (!keys.IsEmpty())
            {
                var t = keys.Pop();
                
                if (tableFrequency.Find(t) < tableFrequency.Find(min1))
                {
                    min2 = min1;

                    min1 = t;
                }
                else if (tableFrequency.Find(t) >= tableFrequency.Find(min1) && tableFrequency.Find(t) < tableFrequency.Find(min2))
                {
                    min2 = t;
                }
            }
        }

        private static void AddSymbolInCode(MaxBinaryHeap maxBinaryHeap)
        {
            while (true)
            {
                Symbol temp = new Symbol();

                try
                {
                    temp = maxBinaryHeap.RemoveMax();
                }
                catch (Exception e)
                {
                    break;
                }

                for (int i = 0; i < temp.Character.Length; i++)
                {
                    tableCode.FindNode(temp.Character[i].ToString()).data += temp.Code;
                    
                }

                
            }

        }

        public static double CompressionRatio(string text, string encodedText)
        {
            return (double )text.Length * 8 / encodedText.Length;
        }

        public static void PrintTableCode()
        {
            tableCode.Print();
        }
        
        
    }
}