using System;
using System.Collections;
using System.Text;

namespace IntegerSet_ArrayEx
{
    class IntegerSet : ICloneable, IEnumerable
    {
        // private member data
        private int[] elements;  // array of elements
        private int number;      // current number of elements

        // c'tors
        public IntegerSet()
        {
            // allocate buffer
            this.number = 0;
            this.elements = new int[16];
        }

        public IntegerSet(int[] elements) : this()
        {
            for (int i = 0; i < elements.Length; i++)
                this.Insert(elements[i]);
        }

        private IntegerSet(int capacity)
        {
            this.number = 0;

            // compute appropriate buffer size
            int size = 16;
            while (size < capacity)
                size *= 2;

            // allocate buffer
            this.elements = new int[size];
        }

        // public properties
        public int Size
        {
            get
            {
                return this.number;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.number == 0;
            }
        }

        // indexer
        public int this[int index]
        {
            get
            {
                // check parameter
                if (index < 0)
                    throw new IndexOutOfRangeException();
                if (index >= this.elements.Length)
                    throw new IndexOutOfRangeException();

                // return element
                return this.elements[index];
            }
        }

        // public methods
        public bool Contains(int n)
        {
            // search element
            for (int i = 0; i < this.number; i++)
                if (this.elements[i] == n)
                    return true;

            return false;
        }

        public bool Insert(int n)
        {
            // element already exists
            if (Contains(n))
                return false;

            // if current buffer is full, allocate a new one
            if (this.elements.Length == this.number)
            {
                int[] tmp = new int[this.elements.Length * 2];

                // copy old buffer into new one
                for (int i = 0; i < this.number; i++)
                    tmp[i] = this.elements[i];

                // switch to new buffer
                this.elements = tmp;
            }

            // insert new element at end of buffer
            this.elements[this.number] = n;
            this.number++;

            return true;
        }

        public bool Remove(int n)
        {
            // element already exists
            if (!Contains(n))
                return false;

            // remove element
            for (int i = 0; i < this.number; i++)
            {
                if (this.elements[i] == n)
                {
                    // remove element - and copy last element into gap
                    this.elements[i] = this.elements[this.number - 1];
                    this.number--;
                }
            }

            // reduce current buffer, if necessary
            if ((this.elements.Length > 16) &&
                (2 * this.number <= this.elements.Length))
            {
                // allocate new buffer
                int[] tmp = new int[this.elements.Length / 2];

                // copy old buffer into new one
                for (int i = 0; i < this.number; i++)
                    tmp[i] = this.elements[i];

                // switch to new buffer
                this.elements = tmp;
            }

            return true;
        }

        // comparison operators
        public static bool operator==(IntegerSet s1, IntegerSet s2)
        {
            return s1.Equals(s2);
        }

        public static bool operator!=(IntegerSet s1, IntegerSet s2)
        {
            return !(s1 == s2);
        }

        public static bool operator<=(IntegerSet s1, IntegerSet s2)
        {
            // compare both sets element per element
            for (int i = 0; i < s1.Size; i++)
                if (!s2.Contains(s1[i]))
                    return false;

            return true;
        }

        public static bool operator>=(IntegerSet s1, IntegerSet s2)
        {
            return s2 <= s1;
        }

        public static bool operator<(IntegerSet s1, IntegerSet s2)
        {
            return (s1 <= s2) && (s1 != s2);
        }

        public static bool operator>(IntegerSet s1, IntegerSet s2)
        {
            return !(s1 <= s2);
        }

        // conversion operator
        public static implicit operator IntegerSet(int n)
        {
            IntegerSet s = new IntegerSet();
            s.Insert(n);
            return s;
        }

        // set theory specific operators
        public static IntegerSet operator+(IntegerSet s1, IntegerSet s2)
        {
            IntegerSet s = (IntegerSet)s1.Clone();
            for (int i = 0; i < s2.Size; i++)
                s.Insert(s2.elements[i]);

            return s;
        }

        public static IntegerSet operator-(IntegerSet s1, IntegerSet s2)
        {
            IntegerSet s = (IntegerSet)s1.Clone();
            for (int i = 0; i < s2.Size; i++)
                s.Remove(s2.elements[i]);

            return s;
        }

        public static IntegerSet operator^(IntegerSet s1, IntegerSet s2)
        {
            IntegerSet s = new IntegerSet();

            for (int i = 0; i < s1.Size; i++)
            {
                int n = s1.elements[i];
                if (s2.Contains(n))
                    s.Insert(n);
            }

            return s;
        }

        // contract with base class 'Object'
        public override bool Equals(Object o)
        {
            if (!(o is IntegerSet))
                return false;

            IntegerSet s = (IntegerSet)o;

            // compare sizes
            if (this.Size != s.Size)
                return false;

            // compare both sets element per element
            for (int i = 0; i < this.Size; i++)
            {
                if (s.Contains(this.elements[i]))
                    continue;

                return false;
            }

            return true;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            for (int i = 0; i < this.number; i++)
            {
                sb.Append(this.elements[i]);
                if (i < this.number - 1)
                    sb.Append(',');
            }
            sb.AppendFormat("{0}[{1},{2}]", '}',
                this.number, this.elements.Length);

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return this.elements.GetHashCode();
        }

        // implementation of interface 'ICloneable'
        public Object Clone()
        {
            IntegerSet s = new IntegerSet(this.elements.Length);

            for (int i = 0; i < this.Size; i++)
                s.Insert(this[i]);

            return s;
        }

        // implementation of interface 'IEnumerable'
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.number; i++)
                yield return this.elements[i];
        }
    }
}
