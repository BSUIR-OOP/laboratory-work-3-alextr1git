using System;
using SerializerLab.Classes;
using System.Collections.Generic;
using System.Reflection;

namespace SerializerLab.Serialization
{
    public class TextSerializer : ISerialization
    {
        public string Serialize(List<Animal> ListofAnimal)
        {
            string OutString = string.Empty;

            foreach(var animal in ListofAnimal)
            {
                OutString += "{";
                Type type = animal.GetType();

                OutString += $"'Type': {type},";

                foreach(var filed in type.GetFields())
                {

                    if(filed.GetType() == typeof(int) )
                        OutString += $" '{filed.Name}': {filed.GetValue(animal)},";

                    else
                        OutString += $" '{filed.Name}': \"{filed.GetValue(animal)}\",";
                } 

                foreach(var property in type.GetProperties())
                {

                    if(property.GetType() == typeof(string) )
                        OutString += $" '{property.Name}': \"{property.GetValue(animal)}\",";

                    else
                        OutString += $" '{property.Name}': {property.GetValue(animal)},";
                }

                OutString = OutString.Remove(OutString.Length - 1);
                OutString+="}\r\n";
            }

            return OutString;
        }

        public List<Animal> Deserialize(string str)
        {
            TDictionary textDictionary;
            List<Animal> ListofAnimal = new List<Animal>();

            string[] DividedString = str.Split('\n');

            foreach(var item in DividedString)
            {

                if(item == "")
                    break;

                textDictionary = new TDictionary(item);
                textDictionary.MakeDictionary();

                string tempString;

                textDictionary.dictionary.TryGetValue("Type", out tempString);

                Type type = Type.GetType(tempString);
                Animal pet = (Animal)Activator.CreateInstance(type);

                foreach(var field in type.GetFields())
                {
                    if(field.FieldType == typeof(string))
                    {
                        string fieldValue = textDictionary.dictionary[field.Name];

                        field.SetValue(pet, fieldValue);
                    }
                    else
                    {
                        int fieldValue =  Convert.ToInt16(textDictionary.dictionary[field.Name]);

                        field.SetValue(pet, fieldValue);
                    }
                }

                foreach(var property in type.GetProperties())
                {
                    if(property.PropertyType == typeof(string))
                    {
                        string propertyValue = textDictionary.dictionary[property.Name]; 

                        property.SetValue(pet, propertyValue);
                    }
                    else
                    {
                        int propertyValue =  Convert.ToInt16(textDictionary.dictionary[property.Name]);

                        property.SetValue(pet, propertyValue);
                    }
                }

                ListofAnimal.Add(pet);
            }

            return ListofAnimal;
        }
    }
}