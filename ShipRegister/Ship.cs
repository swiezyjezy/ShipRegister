using Microsoft.VisualBasic;
using ShipRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShipRegister
{
    public class Ship
    {
        public static int IMOLength = 7;

        public string name { get; }

        private char[] IMO = new char[IMOLength];
        public float length { get; }
        public float width { get; }

        public int getIMOIntValue()
        {
            int retValue = 0;
            for (int i = 0; i < IMOLength; i++)
            {
                retValue = retValue * 10 + IMO[i] - '0';
            }

            return retValue;
        }

        public bool IsImoValid()
        {
            for (int i = 0; i < IMOLength; i++)
            {
                if (IMO[i] > '9' || IMO[i] < '0')
                {
                    Console.WriteLine("Not allowed character in IMO");
                    return false;
                }
            }

            // check IMO's last digit 
            int checkDigit = 0;
            for (int i = 0; i < IMOLength - 1; i++)
            {
                int digit = IMO[i] - '0';
                checkDigit += digit * (IMOLength - i);
            }
            if(checkDigit % 10 != IMO[IMOLength - 1] - '0')
            {
                Console.WriteLine($"IMO check digit is not correct. Expected value: {checkDigit % 10}");
                return false;
            }
            else
            {
                return true;
            }
        }

        public Ship(string name, string IMO, float length, float width)
        {
            if(IMO.Length != IMOLength)
            {
                Console.WriteLine($"IMO has to contain {IMOLength} digits");
                throw new ArgumentException("IMO is not valid");
            }
            else
            {
                this.IMO = IMO.ToCharArray();
            }
            
            this.name = name;
            this.length = length;
            this.width = width;

            if (!IsImoValid())
            {
                throw new ArgumentException("IMO is not valid");
            }
            if( length <= 0)
            {
                throw new ArgumentException("length has to be greater than 0");
            }
            if (width <= 0)
            {
                throw new ArgumentException("width has to be greater than 0");
            }
        }
    }

    public class Person
    {
        public string name { get; }
        public string surname { get; }

        public Person(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

    }

    public class Passenger : Person
    {
        public uint ID { get; }


        public Passenger(uint id, string name, string surname) : base(name, surname)
        {
            this.ID = id;
        }
    }
    public enum FuelType
    {
        Diesel,
        HeavyFuel
    };

    public class Tank
    {
        FuelType fuelType;

        // in liters
        uint tankCapacity;

        // in liters
        uint fuelLevel;

        public Tank(uint capacity)
        {
            tankCapacity = capacity;
            fuelLevel = 0;
        }


        public void AddFuelToTank(uint fuelQuantity, FuelType type)
        {
            if (fuelQuantity <= 0)
            {
                Console.WriteLine($"fuelQuantity has to be greater than 0");
                return;
            }
            if (fuelLevel != 0 && type != fuelType)
            {
                Console.WriteLine($"Tank already contains fuel of diffrent type: {fuelType}");
                return;
            }
            if (tankCapacity < fuelLevel + fuelQuantity)
            {
                Console.WriteLine($"Tank cannot fit that amount of fuel. Remaining capacity is: {tankCapacity - fuelLevel}");
                return;
            }

            fuelLevel += fuelQuantity;
            fuelType = type;
            Console.WriteLine($"Fuel succesfully added. Current fuel level in tank is: {fuelLevel}");
        }

        public void RemoveFuelFromTank(uint fuelQuantity)
        {
            if (fuelQuantity <= 0)
            {
                Console.WriteLine($"fuelQuantity has to be greater than 0");
                return;
            }
            if (fuelQuantity > fuelLevel)
            {
                Console.WriteLine($"fuelQuantity is greater than current fuel level in tank. Tank has been emptied");
                fuelLevel = 0;
                return;
            }
            fuelLevel -= fuelQuantity;
            Console.WriteLine($"Fuel succesfully removed from tank. Current fuel level in tank is: {fuelLevel}");
        }
    }
    public class TankShip : Ship
    {
        private List<Tank> tanksOnVessel;

        public Tank GetTankAtIndex(int index)
        {
            if (index < 0 || index >= tanksOnVessel.Count)
                throw new IndexOutOfRangeException($"Invalid tank index: {index}");
            return tanksOnVessel[index];
        }
        public TankShip(string name, string IMO, float length, float width) : base(name, IMO, length, width)
        {
            tanksOnVessel = new List<Tank>(4);
            for(int i = 0; i < 8; i++)
            {
                tanksOnVessel.Add(new Tank(5000000));
            }
        }

        public TankShip(string name, string IMO, float length, float width, int tanksQuantity) : base(name, IMO, length, width)
        {
            if (tanksQuantity <= 0)
                throw new ArgumentException("tanksQuantity has to be number greater than 0");

            tanksOnVessel = new List<Tank>(tanksQuantity);
            for(int i = 0; i < tanksQuantity; i++)
            {
                tanksOnVessel.Add(new Tank(5000000));
            }
        }
        public TankShip(string name, string IMO, float length, float width, int tanksQuantity, uint [] tanksCapacity) : base(name, IMO, length, width)
        {
            if (tanksQuantity <= 0)
                throw new ArgumentException("tanksQuantity has to be number greater than 0");

            if (tanksCapacity == null)
                throw new ArgumentNullException(nameof(tanksCapacity));

            if (tanksCapacity.Length != tanksQuantity)
                throw new ArgumentException("tanksCapacity list length doesnt match up with tanksQuantity number");

            if (tanksCapacity.Any(Capacity => Capacity == 0))
                throw new ArgumentException("Tank capacity cannot be equal to 0");

            tanksOnVessel = new List<Tank>(tanksQuantity);
            for (int i = 0; i < tanksQuantity; i++)
            {
                tanksOnVessel.Add(new Tank(tanksCapacity[i]));
            }
        }
    }

    public class PassengerShip : Ship
    {
        Dictionary<uint, Passenger> passengers = new();
        uint passengersCounter = 0;

        public void AddPassenger(string name, string surname)
        {
            var newPassenger = new Passenger(passengersCounter, name, surname);
            passengers.Add(newPassenger.ID, newPassenger);
            Console.WriteLine($"Passenger: {newPassenger.name} {newPassenger.surname} with id: {newPassenger.ID} has been added");
            passengersCounter++;
        }

        public void DeletePassenger(uint id)
        {
            var removedPassenger = passengers.GetValueOrDefault(id);
            if (passengers.Remove(id))
            {
                Console.WriteLine($"Passenger: {removedPassenger.name} {removedPassenger.surname} with id: {removedPassenger.ID} has been removed");
            }
            else
            {
                Console.WriteLine($"Couldnt find passenger with id: {id}");
            }
        }

        public PassengerShip(string name, string IMO, float length, float width) : base(name, IMO, length, width) { }
    }
}