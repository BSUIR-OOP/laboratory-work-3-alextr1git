using System;

namespace SerializerLab.Classes
{
    public class Cat : Animal
    {
        public override void AnimalEat()
        {
            Console.WriteLine($"A cat eats meat");
        }
    }
}