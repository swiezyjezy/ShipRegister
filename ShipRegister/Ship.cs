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

        private string name;
        private char[] IMO = new char[IMOLength];
        private float length;
        private float width;

        public int getIMOIntValue()
        {
            int retValue = 0;
            for (int i = 0; i < IMOLength; i++)
            {
                retValue = retValue * 10 + IMO[i] - '0';
            }

            return retValue;
        }

        public string getName()
        {
            return name;
        }
        public float getLength()
        {
            return length;
        }
        public float getWidth()
        {
            return width;
        }

        public bool IsImoValid()
        {
            for (int i = 0; i < IMOLength; i++)
            {
                if (IMO[i] > '9' || IMO[i] < '0')
                    return false;
            }

            // check IMO's last digit 
            int checkDigit = 0;
            for (int i = 0; i < IMOLength - 1; i++)
            {
                int digit = IMO[i] - '0';
                checkDigit += digit * (IMOLength - i);
            }
            return checkDigit % 10 == IMO[IMOLength - 1] - '0';
        }

        public Ship(string name, char[] IMO, float length, float width)
        {
            this.IMO = IMO;
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
        private string name;
        private string surname;

        public string GetName()
        {
            return name;
        }
        public string GetSurname()
        {
            return surname;
        }
        public Person(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

    }

    public class Passenger : Person
    {
        private uint ID;

        public uint getID()
        {
            return ID;
        }

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
            return tanksOnVessel[index];
        }
        public TankShip(string name, char[] IMO, float length, float width) : base(name, IMO, length, width)
        {
            tanksOnVessel = new List<Tank>(4);
            for(int i = 0; i < 8; i++)
            {
                tanksOnVessel.Add(new Tank(5000000));
            }
        }

        public TankShip(string name, char[] IMO, float length, float width, int tanksQuantity) : base(name, IMO, length, width)
        {
            if (tanksQuantity <= 0)
                throw new ArgumentException("tanksQuantity has to be number greater than 0");

            tanksOnVessel = new List<Tank>(tanksQuantity);
            for(int i = 0; i < tanksQuantity; i++)
            {
                tanksOnVessel.Add(new Tank(5000000));
            }
        }
        public TankShip(string name, char[] IMO, float length, float width, int tanksQuantity, uint [] tanksCapacity) : base(name, IMO, length, width)
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

        void addPassenger(string name, string surname)
        {
            var newPassenger = new Passenger(passengersCounter, name, surname);
            passengers.Add(newPassenger.getID(), newPassenger);
            Console.WriteLine($"Passenger: {newPassenger.GetName()} {newPassenger.GetSurname()} with id: {newPassenger.getID()} has been added");
            passengersCounter++;
        }

        void deletePassenger(uint id)
        {
            if (passengers.Remove(id))
            {
                Console.WriteLine($"Passenger: {passengers.GetValueOrDefault(id).GetName()} {passengers.GetValueOrDefault(id).GetSurname()} with id: {id} has been removed");
            }
            {
                Console.WriteLine($"Couldnt find passenger with id: {id}");
            }
        }

        public PassengerShip(string name, char[] IMO, float length, float width) : base(name, IMO, length, width) { }
    }
}