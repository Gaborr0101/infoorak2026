using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace godrok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] sorok = File.ReadAllLines("melyseg-godrok.txt");
            List<Adat> adatok = new List<Adat>();


            for (int i = 0; i < sorok.Length; i++)
            {
                adatok.Add(new Adat(int.Parse(sorok[i])));
            }

            //sorok.ToList().ForEach(x => adatok.Add(new Adat(int.Parse(x))));
            Console.WriteLine("1.Feladat");
            Console.WriteLine("A fájl adatainak száma:" + adatok.Count);

            Console.WriteLine("2.Feladat");
            //Olvasson be egy távolságértéket, majd írja a képernyőre, hogy milyen mélyen van a gödör
            //alja azon a helyen!Ezt a távolságértéket használja majd a 6.feladat megoldása során is!

            Console.Write("Adjon meg egy távolságértéket!");
            int tavolsag = int.Parse(Console.ReadLine());
            Console.WriteLine("Ezen a helyen a felszín " + adatok[tavolsag - 1].melyseg + " méter mélyen van.");

            Console.WriteLine("3.Feladat");
            //Határozza meg, hogy a felszín hány százaléka maradt érintetlen és jelenítse meg 2 tizedes pontossággal!
            double szazalek = adatok.Where(x => x.melyseg == 0).Count() / (double)adatok.Count * 100;
            int erintetlen = 0;
            //foreach (var item in adatok)
            //{
            //    if (item.melyseg == 0)
            //    {
            //        erintetlen++;
            //    }
            //}
            //double szazalek = (double)erintetlen / adatok.Count * 100;
            Console.WriteLine("Az érintetlen terület aránya  " + szazalek.ToString("F2") + "%.");

            Console.WriteLine("4.Feladat");
            // Írja ki a godrok.txt fájlba a gödrök leírását, azaz azokat a számsorokat, amelyek egy-egy
            //gödör méterenkénti mélységét adják meg! Minden gödör leírása külön sorba kerüljön!Az
            //állomány pontosan a gödrök számával egyező számú sort tartalmazzon!

            List<Adat> godrok = new List<Adat>();
            foreach (var item in adatok)
            {
                if (item.melyseg > 0)
                {
                    godrok.Add(item);
                }
            }
            List<string> kimenet = new List<string>();
            foreach (var item in godrok)
            {
                kimenet.Add(item.melyseg.ToString());
            }
            File.WriteAllLines("godrok.txt", kimenet);


            //Határozza meg a gödrök számát és írja a képernyőre!

            Console.WriteLine("5.Feladat");
            int godrokSzama = 0;
            bool inGodor = false;
            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].melyseg > 0 && !inGodor)
                {
                    godrokSzama++;
                    inGodor = true;
                }
                else if (adatok[i].melyseg == 0 && inGodor)
                {
                    inGodor = false;
                }
            }
            Console.WriteLine("A gödrök száma: " + godrokSzama);



            Console.WriteLine("6.Feladat");

            //Ha a 2. feladatban beolvasott helyen nincs gödör, akkor „Az adott helyen nincs gödör.”
            //üzenetet jelenítse meg, ha ott gödör található, akkor határozza meg, hogy
            //(a feladat) mi a gödör kezdő és végpontja!A meghatározott értékeket írja a képernyőre!
            //(Ha nem tudja meghatározni, használja a további részfeladatoknál a 7 és 22
            //értéket, mint a kezdő és a végpont helyét)
            //(b feladat) a legmélyebb pontja felé mindkét irányból folyamatosan mélyül-e! Azaz a gödör
            //az egyik szélétől monoton mélyül egy pontig, és onnantól monoton emelkedik a
            //másik széléig. Az eredménytől függően írja ki a képernyőre a „Nem mélyül
            //folyamatosan.” vagy a „Folyamatosan mélyül.” mondatot!
            //(c feladat) mekkora a legnagyobb mélysége!A meghatározott értéket írja a képernyőre!
            //(d feladat) mekkora a térfogata, ha szélessége minden helyen 10 méternyi! A meghatározott
            //értéket írja a képernyőre!
            //(e feladat) a félkész csatorna esőben jelentős mennyiségű vizet fogad be.Egy gödör annyi
            //vizet képes befogadni anélkül, hogy egy nagyobb szélvihar hatására se öntsön
            //ki, amennyi esetén a víz felszíne legalább 1 méter mélyen van a külső felszínhez
            //épest.Írja a képernyőre ezt a vízmennyiséget!

            //a feladat
            Console.WriteLine("a feladat");
            int kezdopont = -1;
            int vegpont = -1;
            if (adatok[tavolsag - 1].melyseg == 0)
            {
                Console.WriteLine("Az adott helyen nincs gödör.");
            }
            else
            {
                for (int i = tavolsag - 1; i >= 0; i--)
                {
                    if (adatok[i].melyseg == 0)
                    {
                        kezdopont = i + 1;
                        break;
                    }
                }
                for (int i = tavolsag - 1; i < adatok.Count; i++)
                {
                    if (adatok[i].melyseg == 0)
                    {
                        vegpont = i - 1;
                        break;
                    }
                }
                Console.WriteLine($"A gödör kezdete: {kezdopont + 1}méter, a gödör vége: {vegpont + 1}méter.");

            //b feladat
            Console.WriteLine("b feladat");
            int legmelyebb = kezdopont;
            for (int i = kezdopont; i <= vegpont; i++)
              {
                 if (adatok[i].melyseg > adatok[legmelyebb].melyseg)
                 {
                   legmelyebb = i;
                 }
              }
            bool folyamatosanMelyul = true;
            for (int i = kezdopont; i < legmelyebb; i++)
                {
                  if (adatok[i].melyseg >= adatok[i + 1].melyseg)
                   {
                      folyamatosanMelyul = false;
                      break;
                   }
                }
            for (int i = legmelyebb; i < vegpont; i++)
                {
                  if (adatok[i].melyseg <= adatok[i + 1].melyseg)
                    {
                      folyamatosanMelyul = false;
                      break;
                    }
                }
                if (folyamatosanMelyul)
                {
                    Console.WriteLine("Folyamatosan mélyül.");
                }
                else
                {
                    Console.WriteLine("Nem mélyül folyamatosan.");
                }
                //c feladat
                Console.WriteLine("c feladat");
                Console.WriteLine($"A legnagyobb mélysége: {adatok[legmelyebb].melyseg} méter.");

                //d feladat
                Console.WriteLine("d feladat");
                int terfogat = 0;
                for (int i = kezdopont; i <= vegpont; i++)
                {
                    terfogat += adatok[i].melyseg * 10;
                }
                Console.WriteLine($"A^térfogata: {terfogat} m^3.");

                //e feladat
                Console.WriteLine("e feladat");
               
                int vizMennyiseg = 0;
                for (int i = kezdopont; i <= vegpont; i++)
                {
                    vizMennyiseg += (adatok[i].melyseg - 1) * 10;
                }
                Console.WriteLine($"A befogadott vízmennyiség: {vizMennyiseg} m^3.");


            }
        }
    }
}
