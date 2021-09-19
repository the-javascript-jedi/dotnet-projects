using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            PetStruct dog;
            //referring the enum data type-PetType
            dog.Type = PetType.Dog;
            dog.HasFur = true;
            dog.Legs = 4;

            //Instantiate a class
            PetClass duck=new PetClass();
            //referring the enum data type-PetType
            duck.Type = PetType.Duck;
            duck.HasFur = false;
            duck.Legs = 2;
            Console.WriteLine("A "+dog.Type+" has "+dog.Legs+" Legs");
            Console.WriteLine("A " +duck.Type + " has "+ duck.Legs + " Legs");
            Console.ReadLine();
        }       
    }
    //class is a reference type and needs to be instantiated before they can be used
    class PetClass
    {
        public int Legs;
        public PetType Type;
        public string Name;
        public bool HasFur;
    }
    //struct is a value type - we can declare it like any other type
    struct PetStruct
    {
        public int Legs;
        public PetType Type;
        public string Name;
        public bool HasFur;
    } 
    //enum - custom type
    enum PetType
    {
        Dog,
        Duck,
    }

}
