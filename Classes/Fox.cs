using System;

namespace SerializerLab.Classes
{
    public class Fox : Animal
    {
        public override void AnimalEat()
        {
            Console.WriteLine($"Fox eats meat");
        }
    }
}