using System;
using System.Collections;
using System.Text;

namespace IntegerSet_Array
{
    public class IntegerSet : ICloneable, IEnumerable
    {
        // private member data
        private int[] elements;

        // c'tors
        public IntegerSet()
        {
            this.elements = new int[0];
        }

        public IntegerSet(int[] elements) : this()
        {
            for (int i = 0; i < elements.Length; i++)
                this.Insert(elements[i]);
        }

        // public properties
        public int Size
        {
            get
            {
                return this.elements.Length;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.elements.Length == 0;
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
            for (int i = 0; i < this.elements.Length; i++)
                if (this.elements[i] == n)
                    return true;

            return false;
        }

        public bool Insert(int n)
        {
            // element already exists
            if (Contains(n))
                return false;

            // allocate new buffer
            int[] tmp = new int[this.elements.Length + 1];

            // copy old buffer into new one
            for (int i = 0; i < this.elements.Length; i++)
                tmp[i] = this.elements[i];

            // insert new element at end of buffer
            tmp[this.elements.Length] = n;

            // switch to new buffer
            this.elements = tmp;

            return true;
        }

        public bool Remove(int n)
        {
            // element already exists
            if (!Contains(n))
                return false;

            // allocate new buffer
            int[] tmp = new int[this.elements.Length - 1];

            // copy old buffer into new one
            for (int i = 0, k = 0; i < this.elements.Length; i++)
            {
                if (this.elements[i] == n)
                    continue;

                tmp[k] = this.elements[i];
                k++;
            }

            // switch to new buffer
            this.elements = tmp;

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
            for (int i = 0; i < s2.elements.Length; i++)
                s.Insert(s2.elements[i]);

            return s;
        }

        public static IntegerSet operator-(IntegerSet s1, IntegerSet s2)
        {
            IntegerSet s = (IntegerSet)s1.Clone();
            for (int i = 0; i < s2.elements.Length; i++)
                s.Remove(s2.elements[i]);

            return s;
        }

        public static IntegerSet operator^(IntegerSet s1, IntegerSet s2)
        {
            IntegerSet s = new IntegerSet();
            for (int i = 0; i < s1.elements.Length; i++)
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
            if (this.elements.Length != s.elements.Length)
                return false;

            // compare both sets element per element
            for (int i = 0; i < this.elements.Length; i++)
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
            for (int i = 0; i < this.elements.Length; i++)
            {
                sb.Append(this.elements[i]);
                if (i < this.elements.Length - 1)
                    sb.Append(',');
            }
            sb.AppendFormat("{0}[{1}]", '}', this.elements.Length);
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return this.elements.GetHashCode();
        }

        // implementation of interface 'ICloneable'
        public Object Clone()
        {
            return new IntegerSet(this.elements);
        }

        // implementation of interface 'IEnumerable'
        public IEnumerator GetEnumerator()
        {
            return new IntegerSetEnumerator(this);
        }
    }

    class IntegerSetEnumerator : IEnumerator
    {
        private IntegerSet set;
        private int pos;

        public IntegerSetEnumerator(IntegerSet set)
        {
            this.set = set;
            this.pos = -1;
        }

        // implementation of interface 'IEnumerator'
        public Object Current
        {
            get
            {
                return this.set[this.pos];
            }
        }

        public bool MoveNext()
        {
            this.pos++;
            if (this.pos == this.set.Size)
            {
                this.Reset();
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Reset()
        {
            this.pos = -1;
        }
    }
}
