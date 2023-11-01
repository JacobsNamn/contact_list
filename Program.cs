namespace dtp6_contacts
{
    class MainClass
    {
        static List<Person> contactList = new List<Person>();
        class Person
        {
            public string PersName { get; set; }
            public string Surname { get; set; }
            public string[] Phones { get; set; }
            public string[] Addresses { get; set; }
            public string BirthDate { get; set; }
        }

        static string lastFileName = "address.lis";

        public static void Main(string[] args)
        {
            string[] commandLine;

            Console.WriteLine("Hello and welcome to the contact list");
            showHelp();

            do // Program loop
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                string command = commandLine[0].ToLower();
                if (command == "quit")
                {
                    // Tomt!
                }
                else if (command == "load") { load(commandLine); }
                else if (command == "save") { save(commandLine); }
                else if (command == "new") { newItem(commandLine); }
                else if (command == "help") { showHelp(); }
                else {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0].ToLower() != "quit");
        }


        static void showHelp() {
            Console.WriteLine("Available commands:" +
                "\n  delete       - emtpy the contact list" +
                "\n  delete /persname/ /surname/ - delete a person" +
                "\n  load        - load contact list data from the file address.lis" +
                "\n  load /file/ - load contact list data from the file" +
                "\n  new        - create new person" +
                "\n  new /persname/ /surname/ - create new person with personal name and surname" +
                "\n  quit        - quit the program" +
                "\n  save         - save contact list data to the file previously loaded" +
                "\n  save /file/ - save contact list data to the file" +
                "\n"
                );
        }

        static void load(string[] commandLine) {
            if (commandLine.Length > 1) lastFileName = commandLine[1];

            using (StreamReader infile = new StreamReader(lastFileName)) {
                string line;
                while ((line = infile.ReadLine()) != null) {
                    Console.WriteLine(line);
                    string[] attrs = line.Split('|');
                    Person p = new Person();
                    p.PersName = attrs[0]; p.Surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    p.Phones = phones;
                    string[] addresses = attrs[3].Split(';');
                    p.Addresses = addresses;

                    contactList.Add(p);

                }
            }
        }

        static void save(string[] commandLine) {
            string file = lastFileName;
            if (commandLine.Length >= 2) {
                file = commandLine[2];
            }
            using (StreamWriter outfile = new StreamWriter(file)) {
                foreach (Person p in contactList) {
                    if (p != null)
                        outfile.WriteLine($"{p.PersName}|{p.Surname}|{String.Join(";", p.Phones)}|{String.Join("|", p.Addresses)}|{p.BirthDate}");
                }
            }
        }

        static void newItem(string[] commandLine) {

            if (commandLine.Length < 2) {
                string ReadWrite(string str) {
                    Console.Write(str);
                    return Console.ReadLine();
                }

                string persname = ReadWrite("personal name: ");
                string surname = ReadWrite("surname: ");
                string phone = ReadWrite("phone: "); // To-do: Split this, since user can have multiple phone numbers.

            } else {
                // NYI!
                Console.WriteLine("Not yet implemented: new /person/");
            }
        }

    }
}
