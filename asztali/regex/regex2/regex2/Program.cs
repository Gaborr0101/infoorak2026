using System.Text.RegularExpressions;
namespace regex2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            File.ReadAllLines("romeoesjulia.txt");
            Regex nevek = new Regex("^[A-Z]+[,\t\n]");
           var eredmeny= nevek.Matches("romeoesjulia.txt");
            Console.WriteLine(eredmeny[0]);

        }
    }
}
