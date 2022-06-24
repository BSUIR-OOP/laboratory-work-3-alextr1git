using System;

namespace SerializerLab.Classes
{
    public class Wolf : Animal
    {
        public override void AnimalEat()
        {
            Console.WriteLine($"Wolf eats meat");
        }
    }
}