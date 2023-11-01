namespace dtp6_contacts
{
    class MainClass
    {
        static List<Person> contactList = new List<Person>();
        class Person
        {
            string persname, surname;
            string[] phones, addresses;
            string birthdate;

            public Person() { }
            public Person(string persname, string surname, string[] phones) {
                this.persname = persname; this.surname = surname; this.phones = phones;
            }
            public Person(string persname, string surname, string[] phones, string[] addresses) {
                this.persname = persname; this.surname = surname; this.phones = phones; this.addresses = addresses;
            }
            public Person(string persname, string surname, string[] phones, string[] addresses, string birthdate) {
                this.persname = persname; this.surname = surname; this.birthdate = birthdate; this.phones = phones; this.addresses = addresses; 
            }

            public string PersName { get { return persname; } }
            public string Surname { get {  return surname; } }
            public string BirthDate { get {  return birthdate; } }
            public string[] Phones { get { return phones; } }
            public string[] Addresses { get { return addresses; } }

            public string GetRaw() {
                return $"{persname}|{surname}|{String.Join(";", phones)}|{String.Join(";", addresses)}|{birthdate}";
            }


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
                    Person p = new Person(attrs[0], attrs[1], attrs[2].Split(';'), attrs[3].Split(";"));

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
                        outfile.WriteLine(p.GetRaw());
                }
            }
        }

        static void newItem(string[] commandLine) { // 'new' command

            if (commandLine.Length < 2) {
                string ReadWrite(string str) {
                    Console.Write(str);
                    return Console.ReadLine();
                }

                string persname = ReadWrite("personal name: ");
                string surname = ReadWrite("surname: ");
                string[] phones = ReadWrite("phone (separate multiple numbers with semi-colon): ").Split(";"); // To-do: Split this, since user can have multiple phone numbers.

                contactList.Add(new Person(persname, surname, phones));

            } else {
                contactList.Add(new Person());
            }
        }

    }
}
