
namespace Jelado
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Adat> adatok = new List<Adat>();

            string[] sorok = File.ReadAllLines("jel.txt");

            sorok.ToList().ForEach(sor => adatok.Add(new Adat(sor)));

            adatok = sorok.ToList().Select(x => new Adat(x)).ToList();

            Console.WriteLine("2.feladat:");
            Console.WriteLine($"Adja meg a jel sorszámát!");

            int beszam = Convert.ToInt32(Console.ReadLine());

            Adat kivalasztott = adatok[beszam - 1];
            Console.WriteLine($"x={kivalasztott.x} y={kivalasztott.y}");

            Console.WriteLine("4.feladat\n Időtartam {1}",
                adatok[0].elteltmp(adatok.Last()));

            //idotartam 18:52:40


           

        }
    }
}