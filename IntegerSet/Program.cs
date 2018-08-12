using System;

using IntegerSet_Array;
// using IntegerSet_ArrayEx;
// using IntegerSet_List;

class Program
{
    public static void Main()
    {
        TestingCtorsDtor();
        TestingMethods();
        TestingRemove();
        TestingInsert();
        TestingOperators();
        TestingArithmeticAssignmentOperators();
        TestingIndexOperator();
        TestingICloneable();
        TestingIEnumerable();
    }

    private static void TestingCtorsDtor()
    {
        Console.WriteLine("Testing c'tors / d'tor: ");

        IntegerSet s1 = new IntegerSet();
        Console.WriteLine("s1: {0}", s1);

        int[] elems1 = { -2, -1, 0, 1, 2, -1, -2 };
        IntegerSet s2 = new IntegerSet(elems1);
        Console.WriteLine("s2: {0}", s2);

        int[] elems2 = { -1, 1, 2, 3, 1, 2, 3, 1, 2, 3, -1 };
        IntegerSet s3 = new IntegerSet(elems2);
        Console.WriteLine("s3: {0}", s3);

        Console.WriteLine("s1.IsEmpty: {0}", s1.IsEmpty);
        Console.WriteLine("s2.IsEmpty: {0}", s2.IsEmpty);
        Console.WriteLine("s3.IsEmpty: {0}", s3.IsEmpty);
    }

    private static void TestingMethods()
    {
        Console.WriteLine("Testing methods: ");

        IntegerSet s = new IntegerSet();
        Console.WriteLine("s: {0}", s);

        // testing 'Insert'
        s.Insert(1);
        s.Insert(2);
        s.Insert(3);
        s.Insert(4);
        s.Insert(5);
        s.Insert(6);
        Console.WriteLine("s: {0}", s);

        // testing 'Remove'
        s.Remove(3);
        s.Remove(4);
        s.Remove(5);
        s.Remove(6);
        Console.WriteLine("s: {0}", s);

        s.Insert(10);
        s.Insert(11);
        s.Insert(12);

        s.Insert(13);
        s.Insert(14);
        s.Insert(15);
        s.Insert(16);
        s.Insert(17);
        s.Insert(18);
        s.Insert(19);
        s.Insert(20);
        s.Insert(21);
        s.Insert(22);
        s.Insert(23);
        Console.WriteLine("s: {0}", s);

        s.Insert(24);
        Console.WriteLine("s: {0}", s);

        s.Remove(11);
        Console.WriteLine("s: {0}", s);


        // testing 'Contains'
        Console.WriteLine("Contains(1): {0}", s.Contains(1));
        Console.WriteLine("Contains(5): {0}", s.Contains(5));
        Console.WriteLine("Contains(10): {0}", s.Contains(10));
    }

    private static void TestingRemove()
    {
        IntegerSet s = new IntegerSet();
        for (int i = 0; i < 20; i++)
            s.Insert(i);
        Console.WriteLine("s: {0}", s);

        s.Remove(5);
        s.Remove(1);
        s.Remove(8);
        Console.WriteLine("s: {0}", s);

        s.Remove(11);
        Console.WriteLine("s: {0}", s);

        s.Remove(16);
        Console.WriteLine("s: {0}", s);

        s.Remove(19);
        Console.WriteLine("s: {0}", s);
    }

    private static void TestingInsert()
    {
        IntegerSet s = new IntegerSet();
        for (int i = 0; i < 16; i++)
            s.Insert(i);
        Console.WriteLine("s: {0}", s);

        s.Insert(16);
        Console.WriteLine("s: {0}", s);
    }

    private static void TestingOperators()
    {
        // testing union set
        IntegerSet s1 = new IntegerSet();
        IntegerSet s2 = new IntegerSet();
        for (int i = 0; i < 7; i++)
            s1.Insert(i);
        for (int i = 2; i < 9; i++)
            s2.Insert(i);
        Console.WriteLine("s1: {0}", s1);
        Console.WriteLine("s2: {0}", s2);
        Console.WriteLine("s1+s2: {0}", s1 + s2);

        // testing intersection set
        Console.WriteLine("s1^s2: {0}", s1 ^ s2);

        // testing ==-operator
        Console.WriteLine("s1 == s2: {0}", s1 == s2);
        s2.Insert(0);
        s2.Insert(1);
        s2.Remove(7);
        s2.Remove(8);
        Console.WriteLine("s2: {0}", s2);
        Console.WriteLine("s1 == s2: {0}", s1 == s2);
        Console.WriteLine("s1 != s2: {0}", s1 != s2);

        // testing subset operator
        Console.WriteLine("s1: {0}", s1);
        Console.WriteLine("s2: {0}", s2);
        Console.WriteLine("s1 <= s2: {0}", s1 <= s2);
        s1.Insert(7);
        Console.WriteLine("s1: {0}", s1);
        Console.WriteLine("s1 <= s2: {0}", s1 <= s2);
    }

    private static void TestingArithmeticAssignmentOperators()
    {
        int[] elements = { 1, 2, 3 };
        IntegerSet s = new IntegerSet(elements);
        Console.WriteLine("s: {0}", s);
        s += 4;
        Console.WriteLine("s: {0}", s);
        s -= 1;
        Console.WriteLine("s: {0}", s);
    }

    private static void TestingIndexOperator()
    {
        int[] elements = { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        IntegerSet s = new IntegerSet(elements);

        for (int i = 0; i < s.Size; i++)
            Console.WriteLine("Element at {0}: {1}", i, s[i]);
    }

    private static void TestingICloneable()
    {
        int[] elements = { 1, 2, 3, 4, 5 };
        IntegerSet s1 = new IntegerSet(elements);
        Console.WriteLine("s1: {0}", s1);

        IntegerSet s2 = (IntegerSet)s1.Clone();
        Console.WriteLine("s2: {0}", s2);
        s2.Remove(5);
        Console.WriteLine("s2: {0}", s2);
    }

    private static void TestingIEnumerable()
    {
        int[] elements = { 1, 2, 3, 4, 5 };
        IntegerSet set = new IntegerSet(elements);
        Console.WriteLine("s: {0}", set);

        foreach (int n in set)
        {
            Console.WriteLine("next element: {0}", n);
        }
    }
}
