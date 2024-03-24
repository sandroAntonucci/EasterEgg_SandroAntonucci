using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace EasterEgg
{
    
    public class Program
    {
        public static void Main()
        {

            const int Zero = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5;

            const string MsgMenu = "Que vols fer? \n\n 1.- Crear un personatge \n 2.- Mostrar personatges \n 3.- Lluitar \n 4.- Actualitzar dades d'un personatge \n 5.- Sortir\n\nAcció: ";
            const string MsgEndGame = "Adeu!";
            const string MsgInvalidInput = "Aquesta acció no és vàlida";
            const string MsgCharUpdate = "Escriu el nom del personatge a actualitzar (exit per sortir)";
            const string MsgFighter = "Amb quin personatge vols lluitar (introdueix el seu nom, escriu exit per sortir)?";
            const string MsgOponent = "Contra quin personatge vols lluitar (introdueix el seu nom, escriu exit per sortir)?";
            const string MsgFightWon = "Has guanyat!";
            const string MsgAttributeUpdated = "L'atribut ha sigut actualitzat.";
            const string MsgFightLost = "Has perdut :(";
            const string MsgCharNotExists = "Aquest personatge no existeix.";
            const string MsgName = "Nom del personatge (al menys una lletra): ";
            const string MsgLevel = "Nivell del personatge (major que 0): ";
            const string MsgHP = "Punts de vida (major que 0): ";
            const string MsgAttack = "Atac del personatge (major que 0): ";
            const string MsgDefense = "Defensa del personatge (major que 0): ";
            const string MsgOk = "Personatge creat amb éxit!";
            const string MsgAttributeToUpdate = "Introdueix el atribut a actualitzar: \n\n1. Nivell \n2. Vida\n3. Atac\n4. Defensa\n\nAcció: ";
            const string Exit = "exit";
            const string MsgRivalsAreTheSame = "Els personatges no poden ser els mateixos";

            int option, level = 0, HP = 0, attack = 0, defense = 0;

            string name = "", charactersPath = @"../../../characters.xml", fighter = "", oponent = "", charToUpdate = "";

            bool exitGame = false, validFighter = false, validOponent = false, exitOption = false;

            Character fighterChar = new Character();
            Character oponentChar = new Character();

            List<Character> characters = new List<Character>();

            if (File.Exists(charactersPath))
            {
                characters = RPGMethods.ReadCharactersFromXML(charactersPath);
            }
            

            while (!exitGame)
            {

                Console.WriteLine(MsgMenu);
                option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {

                    // Creació del personatge
                    case One:

                        // Nom (no pot ser exit)
                        do
                        {

                            Console.Write(MsgName);
                            name = Console.ReadLine();

                        } while (RPGMethods.CheckCharExists(name, characters) || name.Length == Zero || name == Exit);

                        // Nivell
                        do
                        {
                            Console.Write(MsgLevel);
                            level = Convert.ToInt32(Console.ReadLine());
                        } while (level <= Zero);

                        // Vida
                        do
                        {
                            Console.Write(MsgHP);
                            HP = Convert.ToInt32(Console.ReadLine());
                        } while (HP <= Zero);


                        // Atac
                        do
                        {
                            Console.Write(MsgAttack);
                            attack = Convert.ToInt32(Console.ReadLine());
                        } while (attack <= Zero);


                        // Defensa
                        do
                        {
                            Console.Write(MsgDefense);
                            defense = Convert.ToInt32(Console.ReadLine());
                        } while (defense <= Zero);
                        

                        Character newChar = new Character(name, level, HP, attack, defense);
                        characters.Add(newChar);

                        RPGMethods.WriteCharactersToXML(characters, charactersPath);
                        Console.WriteLine(MsgOk);

                        break;

                    // Mostrar personatges
                    case Two:
                        RPGMethods.ShowCharacters(characters);
                        break;

                    // Lluitar
                    case Three:

                        // S'escull personatge
                        do
                        {
                            Console.WriteLine(MsgFighter);

                            fighter = Console.ReadLine();

                            if (fighter == Exit) exitOption = true;

                            // No fa servir checkCharExists ja que hem de guardar el personatge
                            foreach (var character in characters)
                            {
                                if (character.Name == fighter)
                                {
                                    validFighter = true;
                                    fighterChar = character;
                                }
                            }

                            if (!validFighter && !exitOption) Console.WriteLine(MsgCharNotExists);

                        } while (!validFighter && !exitOption);

                        validFighter = false;


                        // S'escull oponent si no s'ha sortit de la opció

                        if (!exitOption)
                        {
                            do
                            {
                                Console.WriteLine(MsgOponent);

                                oponent = Console.ReadLine();

                                if (oponent == Exit) exitOption = true;

                                // No fa servir checkCharExists ja que hem de guardar el personatge 
                                foreach (var character in characters)
                                {
                                    if (character.Name == oponent)
                                    {
                                        validOponent = true;
                                        oponentChar = character;
                                    }
                                }

                                if (!validOponent && !exitOption) Console.WriteLine(MsgCharNotExists);

                            } while (!validOponent && !exitOption);
                        }
                        
                        validOponent = false;

                        if (!exitOption)
                        {
                            if(fighter != oponent)
                            {
                                Console.WriteLine(RPGMethods.Fight(fighterChar, oponentChar) ? MsgFightWon : MsgFightLost);
                            }
                            else
                            {
                                Console.WriteLine(MsgRivalsAreTheSame);
                            }
                        }

                        exitOption = false;

                        break;


                    // Actualitzar dades d'un personatge
                    case Four:

                        do
                        {

                            Console.WriteLine(MsgCharUpdate);
                            charToUpdate = Console.ReadLine();

                        } while (!RPGMethods.CheckCharExists(charToUpdate, characters) && charToUpdate != Exit);

                        // Si no s'ha sortit de la opció, actualitza l'atribut a escollir per l'usuari
                        if(charToUpdate != Exit)
                        {

                            Console.WriteLine(MsgAttributeToUpdate);
                            option = Convert.ToInt32(Console.ReadLine());
                            
                            // S'actualitza la informació al personatge escollit (si es vàlida)
                            foreach (var character in characters)
                            {
                                if (character.Name == charToUpdate)
                                {

                                    switch (option)
                                    {

                                        default:
                                            Console.WriteLine(MsgInvalidInput);
                                            break;

                                        // Nivell
                                        case One:
                                            do
                                            {
                                                Console.Write(MsgLevel);
                                                level = Convert.ToInt32(Console.ReadLine());
                                            } while (level <= Zero);
                                            character.Level = level;
                                            break;

                                        // Vida
                                        case Two:
                                            do
                                            {
                                                Console.Write(MsgHP);
                                                HP = Convert.ToInt32(Console.ReadLine());
                                            } while (HP <= Zero);
                                            character.HealthPoints = HP;
                                            break;

                                        // Atac
                                        case Three:
                                            do
                                            {
                                                Console.Write(MsgAttack);
                                                attack = Convert.ToInt32(Console.ReadLine());
                                            } while (attack <= Zero);
                                            character.Attack = attack;
                                            break;

                                        // Defensa
                                        case Four:
                                            do
                                            {
                                                Console.Write(MsgDefense);
                                                defense = Convert.ToInt32(Console.ReadLine());
                                            } while (defense <= Zero);
                                            character.Defense = defense;
                                            break;

                                    }

                                    // S'actualitza l'XML i s'informa a l'usuari
                                    RPGMethods.WriteCharactersToXML(characters, charactersPath);
                                    Console.WriteLine(MsgAttributeUpdated);

                                }
                            }
                        }

                        break;


                    // Sortir del joc
                    case Five:
                        exitGame = true;
                        Console.WriteLine(MsgEndGame);
                        break;

                }

            }
        }

    }
}