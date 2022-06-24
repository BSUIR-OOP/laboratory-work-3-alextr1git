using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SerializerLab.Classes;
using SerializerLab.Serialization;

namespace SerializerLab
{
    public partial class Serializer : Form
    {
        private Dictionary<string, Animal> Dictionary;

        private List<Animal> ListofAnimals;
        private ISerialization SerializationText;
        public Serializer()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SerializationText = new TextSerializer();

            ListofAnimals = new List<Animal>();

            Dictionary = new Dictionary<string, Animal>()
            {
                {"Cow", new Cow()},
                {"Cat", new Cat()},
                {"Dog", new Dog()},
                {"Fox", new Fox()},
                {"Wolf", new Wolf()}
            };
            foreach(var every in Dictionary.Keys)
            {
                cbType.Items.Add(every);
            }
        }

        private void buttonSerialize_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = saveFileDialog1.FileName;
            string result = SerializationText.Serialize(ListofAnimals);

            System.IO.File.WriteAllText(filename, result);
        }

        private void buttonDeserialize_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string NameofFile = openFileDialog1.FileName;
            string FileData = System.IO.File.ReadAllText(NameofFile);

            ListofAnimals.AddRange(SerializationText.Deserialize(FileData));
            lbList.Items.Clear();

            ListShow();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Type type = Dictionary.GetValueOrDefault(cbType.Text).GetType();
            Animal animal = (Animal)Activator.CreateInstance(type);

            animal.Name = textBoxName.Text;
            animal.Age = Convert.ToInt16(textBoxAge.Text);
            animal.Color = textBoxColor.Text;

            ListofAnimals.Add(animal);
            lbList.Items.Add($"Type: {cbType.Text}, Name: {textBoxName.Text}, Age: {textBoxAge.Text}, Color: {textBoxColor.Text}");

            Clear();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int id = lbList.SelectedIndex;

            lbList.Items.RemoveAt(id);
            ListofAnimals.RemoveAt(id);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int id = lbList.SelectedIndex;

            ListofAnimals[id].Name = textBoxName.Text;
            ListofAnimals[id].Age = Convert.ToInt16(textBoxAge.Text);
            ListofAnimals[id].Color = textBoxColor.Text;

            lbList.Items.Insert(id+1, $"Type: {cbType.Text}, Name: {textBoxName.Text}, Age: {textBoxAge.Text}, Color: {textBoxColor.Text}");
            lbList.Items.RemoveAt(id);
        }

        private void Clear()
        {
            textBoxAge.Text = string.Empty;
            textBoxName.Text = string.Empty;
            textBoxColor.Text = string.Empty;

            cbType.Text = string.Empty;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = lbList.SelectedIndex;

            if(id != -1)
            {
                string Rawstring = lbList.SelectedItem.ToString();
                string[] Dividedstring = Rawstring.Split(',');
                cbType.Text = Dividedstring[0].Substring(Dividedstring[0].IndexOf("Type: ")+6);
                textBoxName.Text = Dividedstring[1].Substring(Dividedstring[1].IndexOf("Name: ")+6);
                textBoxAge.Text = Dividedstring[2].Substring(Dividedstring[2].IndexOf("Age: ")+5);
                textBoxColor.Text = Dividedstring[3].Substring(Dividedstring[3].IndexOf("Color: ")+7);
            }
        }

        private void ListShow()
        {
            foreach(var animal in ListofAnimals)
            {
                lbList.Items.Add($"Type: {animal.GetType().Name}, Name: {animal.Name}, Age: {animal.Age}, Color: {animal.Color}");
            }
        }
    }
}
