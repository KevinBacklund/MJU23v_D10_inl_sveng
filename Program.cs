﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    break;
                }
                else if (command == "load")
                {
                    load(defaultFile, argument);
                }
                else if (command == "list")
                {
                    list();
                }
                else if (command == "new")
                {
                    new_gloss(argument);
                }
                else if (command == "delete")
                {
                    delete(argument);
                }
                else if (command == "translate")
                {
                    translate(argument);
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void list()
        {
            try
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                }
            }
            catch (NullReferenceException) {Console.WriteLine("No list, use load first");}
        }

        private static void translate(string[] argument)
        {
            if (argument.Length == 2)
            {
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == argument[1])
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == argument[1])
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
            else if (argument.Length == 1)
            {
                Console.WriteLine("Write word to be translated: ");
                string word_to_be_translated = Console.ReadLine();
                foreach (SweEngGloss gloss in dictionary)
                {
                    if (gloss.word_swe == word_to_be_translated)
                        Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                    if (gloss.word_eng == word_to_be_translated)
                        Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                }
            }
        }

        private static void delete(string[] argument)
        {
            if (argument.Length == 3)
            {
                int index = -1;
                for (int i = 0; i < dictionary.Count; i++)
                {
                    SweEngGloss gloss = dictionary[i];
                    if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                        index = i;
                }
                dictionary.RemoveAt(index);
            }
            else if (argument.Length == 1) //FIXME om Argument.Length == 2
            {
                Console.WriteLine("Write word in Swedish: ");
                string swe = Console.ReadLine();
                Console.Write("Write word in English: ");
                string eng = Console.ReadLine();
                int index = -1;
                try
                {
                    for (int i = 0; i < dictionary.Count; i++)
                    {
                        SweEngGloss gloss = dictionary[i];
                        if (gloss.word_swe == swe && gloss.word_eng == eng)
                            index = i;
                    }
                    dictionary.RemoveAt(index);
                }
                catch (NullReferenceException) { Console.WriteLine("No list, use load first"); }
            }
        }

        private static void new_gloss(string[] argument)
        {
            try
            {


                if (argument.Length == 3)
                {
                    dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                }
                else if (argument.Length == 1) //FIXME om Argument.Length == 2 
                {
                    Console.WriteLine("Write word in Swedish: ");
                    string swe = Console.ReadLine();
                    Console.Write("Write word in English: ");
                    string eng = Console.ReadLine();
                    dictionary.Add(new SweEngGloss(swe, eng));
                }
            }
            catch (NullReferenceException) {Console.WriteLine("No list, use load first");}
        }

        private static void load(string defaultFile, string[] argument)
        {
            if (argument.Length == 2)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(argument[1]))
                    {
                        dictionary = new List<SweEngGloss>(); // Empty it!
                        string line = sr.ReadLine();
                        while (line != null)
                        {
                            SweEngGloss gloss = new SweEngGloss(line);
                            dictionary.Add(gloss);
                            line = sr.ReadLine();
                        }
                    }
                }
                catch (FileNotFoundException) {Console.WriteLine("File not found") ;}
            }
            else if (argument.Length == 1)
            {
                using (StreamReader sr = new StreamReader(defaultFile))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
        }
    }
}