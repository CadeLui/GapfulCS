namespace Gapful;
class Program
{
    static bool notInteresting(int num)
    {
        /* I am picky about what numbers I want. This filters the dumb ones. */
        int numDiv = getGapDiv(num);
        return num<100 || (numDiv/10 == numDiv%10) || num%10 == 0;
    }
    static int getGapDiv(int num)
    {
        /* Finds what to divide a number by to test if its gapful */
        int div = num;
        while (div >= 10)
            div /= 10;
        return (div*10) + (num%10);
    }
    static bool testGapful(int num)
    {
        /* Tests if a number is gapful */
        int div = getGapDiv(num);
        if (notInteresting(num)) return false;
        return (float)num/div == num/div;
    }
    static int gapQuotient(int gapful)
    {
        /* Gets the result of dividing a gapful number by its gap */
        if (testGapful(gapful))
            return gapful/getGapDiv(gapful);
        return -1;
    }
    static List<int> findGapful(int limit)
    {
        /* Returns a list of all gapful numbers until a given limit */
        List<int> gapfuls = new List<int>();
        for (int i = 100; i <= limit; i++)
        {
            if (testGapful(i))
                gapfuls.Add(i);
        }
        return gapfuls;
    }
    static List<int> furtherGap(List<int> gapfuls)
    {
        /* 
        ** Takes a list of gapful numbers
        ** Divides those gapfuls by their gap, tests if quotient is gapful
        ** Returns list of all gapful quotients
        */
        List<int> furtherGapped = new List<int>();
        foreach (int i in gapfuls)
        {
            int quotient = gapQuotient(i);
            int doubleQuot = gapQuotient(quotient);
            if (notInteresting(doubleQuot)) continue;
            if (testGapful(quotient)) 
                furtherGapped.Add(quotient);
        }
        furtherGapped.Sort();
        IEnumerable<int> result = furtherGapped.Distinct();
        return result.ToList();
    }
    static void Main(string[] args)
    {
        /* Got bored, learned about gapful numbers, decided to go ham */
        List<int> gapfuls = findGapful(362880);
        while (gapfuls.Count() > 23353) gapfuls = furtherGap(gapfuls);
        foreach (int i in gapfuls)
        {
            Console.WriteLine(i.ToString() + " " + gapQuotient(i).ToString());
        }
        Console.Write(gapfuls.Count());
    }
}
