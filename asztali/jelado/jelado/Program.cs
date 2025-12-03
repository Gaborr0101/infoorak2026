
using static System.Net.Mime.MediaTypeNames;

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

            Console.WriteLine("4.feladat\nIdőtartam {0}",
                adatok.First().elteltido(adatok.Last()));

            //idotartam 18:52:40
            Console.WriteLine("5.feladat");
            //Határozza meg azt a legkisebb, a koordináta-rendszer tengelyeivel párhuzamos oldalú
            //téglalapot, amelyből nem lépett ki a jeladó!Adja meg a téglalap bal alsó és jobb felső
            //sarkának koordinátáit!
            int minx = adatok.Min(x => x.x);
            int maxx = adatok.Max(x => x.x);
            int miny = adatok.Min(y => y.y);
            int maxy = adatok.Max(y => y.y);
            Console.WriteLine($"Bal alsó: {minx} {miny}  Jobb felső: {maxx} {maxy}");

            //Írja a képernyőre, hogy mennyi volt a jeladó elmozdulásainak összege! Úgy tekintjük, hogy
            //a jeladó két pozíciója közötti elmozdulása a pozíciókat összekötő egyenes mentén történt.
            //Az összeget három tizedes pontossággal jelenítse meg! A kiírásnál a tizedesvessző és
            //tizedespont kiírása is elfogadott.Az i - edik és az i + 1 - edik pontok távolságát a
            //képlet segítségével határozhatja meg.

            double osszelmozdulas = 0;

            for (int i = 0; i < adatok.Count - 1; i++)
            {
                double tavolsag = Math.Sqrt(Math.Pow(adatok[i + 1].x - adatok[i].x, 2) + Math.Pow(adatok[i + 1].y - adatok[i].y, 2));
                osszelmozdulas += tavolsag;
            }

            Console.WriteLine($"Elmozdulás: {osszelmozdulas:F3} egység");




        }
    }
}