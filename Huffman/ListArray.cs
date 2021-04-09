using System;

namespace Huffman
{
    public class ListArray<T>
    {
        private T[] items;

        public int Count { get; private set; }

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return items[index];
            }

            set
            {
                if (index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                items[index] = value;
            }
        }

        public int Capacity
        {
            get => items.Length;

            private set
            {
                if (value != items.Length)
                {
                    if (value > 0)
                    {
                        T[] newItems = new T[value];
                        if (Count > 0)
                        {
                            Array.Copy(items, newItems, Count);
                        }
                        items = newItems;
                    }
                    else
                    {
                        items = new T[0];
                    }
                }
            }
        }

        public ListArray()
        {
            items = new T[0];

            Count = items.Length;
        }

        public void Add(T item)
        {
            //T[] array = items;

            int size = Count;

            if (size < items.Length)
            {
                Count++;
                
                items[size] = item;
            }
            else
            {
                AddWithResize(item);
            }
        }

        private void AddWithResize(T item)
        {
            int size = Count;
            
            int newCapacity = items.Length == 0 ? 5 : items.Length * 2;
            
            if (newCapacity < size + 1) newCapacity = size + 1;
                
            Capacity = newCapacity;

            Count++;

            items[size] = item;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Count--;
            if (index < Count)
            {
                Array.Copy(items, index + 1, items, index, Count - index);
            }
            
        }
        
        private void EnsureCapacity(int min)
        {
            if (items.Length < min)
            {
                int newCapacity = items.Length == 0 ? 5 : items.Length * 2;
                
                //if ((uint)newCapacity > Array.MaxArrayLength) newCapacity = Array.MaxArrayLength;
                
                if (newCapacity < min) newCapacity = min;
                
                Capacity = newCapacity;
            }
        }
    }
}