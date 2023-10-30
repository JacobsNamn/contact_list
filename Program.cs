﻿namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
        }

        static string lastFileName = "address.lis";

        public static void Main(string[] args)
        {
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            showHelp();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    // NYI!
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                else if (commandLine[0] == "load")
                {
                    load(commandLine);
                }
                else if (commandLine[0] == "save")
                {
                    save(commandLine);
                }
                else if (commandLine[0] == "new")
                {
                    newItem(commandLine);
                }
                else if (commandLine[0] == "help")
                {
                    showHelp();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
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
                    p.persname = attrs[0];
                    p.surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    p.phone = phones[0];
                    string[] addresses = attrs[3].Split(';');
                    p.address = addresses[0];
                    for (int ix = 0; ix < contactList.Length; ix++) {
                        if (contactList[ix] == null) {
                            contactList[ix] = p;
                            break;
                        }
                    }
                }
            }
        }

        static void save(string[] commandLine) {
            if (commandLine.Length < 2) {
                using (StreamWriter outfile = new StreamWriter(lastFileName)) {
                    foreach (Person p in contactList) {
                        if (p != null)
                            outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                    }
                }
            } else {
                // NYI!
                Console.WriteLine("Not yet implemented: save /file/");
            }
        }

        static void newItem(string[] commandLine) {
            if (commandLine.Length < 2) {
                Console.Write("personal name: ");
                string persname = Console.ReadLine();
                Console.Write("surname: ");
                string surname = Console.ReadLine();
                Console.Write("phone: ");
                string phone = Console.ReadLine();
            } else {
                // NYI!
                Console.WriteLine("Not yet implemented: new /person/");
            }
        }

    }
}
