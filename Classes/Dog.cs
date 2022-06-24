using System;

namespace SerializerLab.Classes
{
    public class Dog : Animal
    {
        public override void AnimalEat()
        {
            Console.WriteLine($"A dog eats meat");
        }
    }
}