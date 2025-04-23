using Microsoft.VisualBasic;
using ShipRegister;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
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

        public bool validateIMO()
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
            this.name = name;
            this.IMO = IMO;
            this.length = length;
            this.width = width;
        }
    }

    public class Person
    {
        private string name;
        private string surname;

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
    enum FuelType
    {
        Diesel,
        HeavyFuel
    };

    struct Tank
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
            Console.WriteLine($"Fuel succesfully added. Current usage of tank is: {fuelLevel}");
        }

        public void RemoveFuelFromTank(uint fuelQuantity)
        {
            if (fuelQuantity <= 0) 
            {
                Console.WriteLine($"fuelQuantity has to be greater than 0");
                return;
            }
            if(fuelQuantity > fuelLevel)
            {

                Console.WriteLine($"fuelQuantity is greater than fuel level in tank.");
                return;
            }
            fuelLevel -= fuelQuantity;

        }
    }

    public class TankShip : Ship
    {
        private List<Tank> tanksOnVessel;

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
            passengersCounter++;
        }

        void deletePassenger(uint id)
        {
            passengers.Remove(id);
        }
        void editPassenger(uint id) 
        {
            var editedPassenger = passengers.GetValueOrDefault(id);
        }

        public PassengerShip(string name, char[] IMO, float length, float width) : base(name, IMO, length, width)
        {
        }
    }
}