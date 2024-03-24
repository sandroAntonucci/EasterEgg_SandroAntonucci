using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace EasterEgg
{
    public static class RPGMethods
    {

        const int Zero = 0;

        public static void ShowCharacters(List<Character> characters)
        {
            foreach (var character in characters)
            {
                Console.WriteLine($"Nom: {character.Name}, Nivell: {character.Level}, PV: {character.HealthPoints}, Atac: {character.Attack}, Defensa: {character.Defense}");
            }
        }

        public static void WriteCharactersToXML(List<Character> characters, string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));
            using (TextWriter writer = new StreamWriter(path))
            {
                serializer.Serialize(writer, characters);
            }
        }

        public static List<Character> ReadCharactersFromXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Character>));

            using (TextReader reader = new StreamReader(path))
            {
                return (List<Character>)serializer.Deserialize(reader);
            }
        }

        // Comprova si  el jugador guanya en una lluita amb un altre personatge (la lluita no la fa l'usuari ja que només pot atacar)
        public static bool Fight(Character fighter, Character oponent)
        {

            while (fighter.HealthPoints > Zero && oponent.HealthPoints > Zero)
            {

                fighter.HealthPoints -= oponent.Attack - fighter.Defense;
                oponent.HealthPoints -= fighter.Attack - oponent.Defense;

            }

            return oponent.HealthPoints <= Zero;

        }

        // Comprova si el personatge existeix
        public static bool CheckCharExists(string name, List<Character> characters)
        {

            foreach (var character in characters)
            {

                if (character.Name == name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

