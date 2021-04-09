using System;
using System.Collections.Generic;
using Huffman;
using Map;

namespace Huffman
{
    public class MinBinaryHeap
    {
        private ListArray<Symbol> list;
        public int heapSize => list.Count;

        public MinBinaryHeap()
        {
            list = new ListArray<Symbol>();
        }

        public MinBinaryHeap(Map<string, int> tableFrequency)
        {
            list = new ListArray<Symbol>();
            
            Map.Queue<string> keys = tableFrequency.GetKeys();

            while (!keys.IsEmpty())
            {
                var t = keys.Pop();

                Symbol symbol = new Symbol() {Character = t, Code = "", Frequency = tableFrequency.Find(t)};
                
                Insert(symbol);
            }
        }

        public void Insert(Symbol symbol)
        {
            list.Add(symbol);
            
            int i = heapSize - 1;
            
            int parent = (i - 1) / 2;

            while (i > 0 && list[parent].Frequency > list[i].Frequency)
            {
                var temp = list[i];
                list[i] = list[parent];
                list[parent] = temp;
                
                i = parent;
                parent = (i - 1) / 2;
            }
        }

        public Symbol RemoveMin()
        {
            if (list.Count == 0)
            {
                throw new Exception("There are no more elements");
            }
            
            var result = list[0];

            list[0] = list[heapSize - 1];

            list[heapSize - 1] = result;

            list.RemoveAt(heapSize - 1);
            
            int i = 0;
            
            while (true)
            {
                int leftChild = 2 * i + 1;
                int rightChild = 2 * i + 2;
                int largestChild = i;
                
                if (leftChild < heapSize && list[leftChild].Frequency < list[largestChild].Frequency) 
                {
                    largestChild = leftChild;
                }

                if (rightChild < heapSize && list[rightChild].Frequency < list[largestChild].Frequency)
                {
                    largestChild = rightChild;
                }

                if (largestChild == i) 
                {
                    break;
                }

                var temp = list[i];
                list[i] = list[largestChild];
                list[largestChild] = temp;
                
                i = largestChild;
            }
            
            return result;
        }
    }
}