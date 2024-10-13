using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program_3
{
    internal class Class1
    {
        void Main()
        {
            // Create a character and an action handler
            Character hero = new Character("Hero");
            SwordmanActions swordmanActions = new();
         //   MageActions mageActions = new();

            // Perform actions using the PerformAction method with different delegates
            hero.PerformAction(swordmanActions.Attack, "attack");
           // hero.PerformAction(mageActions.Defend, "defend");
        }

        // Define a delegate for character actions
        public delegate void CharacterAction(string action);

        public class Character
        {
            public string Name { get; private set; }

            public Character(string name)
            {
                Name = name;
            }

            // Method to perform an action
            public void PerformAction(CharacterAction actionDelegate, string action)
            {
                Console.WriteLine($"{Name} is about to {action}.");
                // Call the delegate method
                actionDelegate?.Invoke(action);
            }
        }

        // ActionHandler class
        public class SwordmanActions
        {
            public void Attack(string action)
            {
                if (action == "attack")
                {
                    Console.WriteLine("The character swings their sword!");
                }
            }

            public void Defend(string action)
            {
                if (action == "defend")
                {
                    Console.WriteLine("The character raises their shield!");
                }
            }
        }

        //// ActionHandler class
        //public class MageActions
        //{
        //    public void Attack(string action)
        //    {
        //        if (action == "attack")
        //        {
        //            Console.WriteLine("The character swings their sword!");
        //        }
        //    }

        //    public void Defend(string action)
        //    {
        //        if (action == "defend")
        //        {
        //            Console.WriteLine("The character raises their shield!");
        //        }
        //    }
        //}

    }
}
