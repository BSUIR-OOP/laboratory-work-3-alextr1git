using System.Collections.Generic;

namespace SerializerLab.Serialization
{
    public class TDictionary
    {
        public Dictionary<string, string> dictionary {get;}

        private string data;
        public TDictionary(string text)
        {
            dictionary = new Dictionary<string, string>();
            this.data = text;
        }
        public void MakeDictionary()
        {
            data = data.Replace("{", "");
            data = data.Replace("}", "");
            data = data.Replace("\r","");

            string[] itemArray = data.Split(',');

            foreach(var item in itemArray)
            {
                string[] s = item.Split(':');

                s[0] = s[0].Replace("'","");
                s[0] = s[0].Trim();
                s[1] = s[1].Trim();

                dictionary.Add(s[0], s[1]);
            }
        }
    }
}