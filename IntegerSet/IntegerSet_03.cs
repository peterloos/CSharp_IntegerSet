using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IntegerSet_List
{
    class IntegerSet : ICloneable, IEnumerable
    {
        // private member data
        private List<int> elements;  // list of elements

        // c'tors
        public IntegerSet()
        {
            // allocate buffer
            this.elements = new List<int>();
        }

        public IntegerSet(int[] elements) : this()
        {
            for (int i = 0; i < elements.Length; i++)
                this.Insert(elements[i]);
        }

        private IntegerSet(List<int> elements) : this()
        {
            for (int i = 0; i < elements.Count; i++)
                this.Insert(elements[i]);
        }

        // public properties
        public int Size
        {
            get
            {
                return this.elements.Count;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return this.elements.Count == 0;
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
                if (index >= this.elements.Count)
                    throw new IndexOutOfRangeException();

                // return element
                return this.elements[index];
            }
        }

        // public methods
        public bool Contains(int n)
        {
            // search element
            if (this.elements.Contains(n))
                return true;

            return false;
        }

        public bool Insert(int n)
        {
            // element already exists
            if (Contains(n))
                return false;

            // insert new element
            this.elements.Add(n);
            return true;
        }

        public bool Remove(int n)
        {
            // element already exists
            if (!Contains(n))
                return false;

            // remove element
            this.elements.Remove(n);
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
            for (int i = 0; i < this.elements.Count; i++)
            {
                sb.Append(this.elements[i]);
                if (i < this.elements.Count - 1)
                    sb.Append(',');
            }
            sb.AppendFormat("{0}[{1}]", '}', this.elements.Count);
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
            for (int i = 0; i < this.elements.Count; i++)
                yield return this.elements[i];
        }
    }
}
