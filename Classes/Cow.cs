using System;

namespace SerializerLab.Classes
{
    public class Cow : Animal
    {
        public override void AnimalEat()
        {
            Console.WriteLine($"A cow eats grass");
        }
    }
}