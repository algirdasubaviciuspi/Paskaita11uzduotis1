using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Paskaita11uzduotis1
{
    class Program
    {
        const string CFd = "..\\..\\..\\Duomenys1.txt";
        const string CFr = "..\\..\\..\\Rezultatai1.txt";
        static void Main(string[] args)
        {
            List<string> lines = new List<string>();  
            ReadData(CFd, ref lines);
            Dictionary<char, int> letters = new Dictionary<char, int>();
            LetterCounting(lines, ref letters);
            if (File.Exists(CFr))
                File.Delete(CFr);
            PrintLinesAndLetters(CFr, lines, letters);
        }
        static void ReadData(string file, ref List<string> lines)
        {               // nuskaito visus duomenis iš failo į string List masyvą
            if (File.Exists(file))
                using (StreamReader reader = new StreamReader(file))
                    lines = File.ReadAllLines(file).ToList();
        }
        static void LetterCounting(List<string> lines, ref Dictionary<char, int> letters)
        {               // iš string List masyvo išrenka raides į char Dictionary masyvą 
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char symb = lines[i][j];
                    if (Char.IsLetter(symb))
                        symb = Char.ToLower(symb);
                    else
                        continue;
                    if (letters.ContainsKey(symb))
                        letters[symb]++;
                    else letters.Add(symb, 1);
                }
            }
        }
        static void PrintLinesAndLetters(string file, List<string> lines, Dictionary<char, int> letters)
        {               // į failą išveda rez.lentelę
            using (var fr = File.AppendText(file))
            {
                foreach (string line in lines)
                    fr.WriteLine(line);
                if (letters.Count > 0)
                {
                    string tableHead = new string('-', 18) + '\n' +
                         String.Format("{0,6} {1,9}", "Raidė", "Skaičius") + '\n' +
                         new string('-', 18);
                    fr.WriteLine(tableHead);
                    foreach (var pair in letters)                                 
                        fr.WriteLine("{0,6} {1,9}", pair.Key, pair.Value);
                    
                    fr.WriteLine(new string('-', 18));
                }
                else
                {
                    fr.WriteLine("Raidžių nerasta");
                }               
            }
        }
    }
}
