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


    struct Tank
    {

        enum FuelType
        {
            Diesel,
            HeavyFuel
        };

        FuelType fuelType;

        // in liters
        uint tankCapacity;

        // in liters
        uint tankUsage;
    }

    public class TankShip : Ship
    {
        List<Tank> tanksOnVessel = new();

        public TankShip(string name, char[] IMO, float length, float width) : base(name, IMO, length, width)
        {
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

        void deletePassenger(int id)
        {

        }
        void editPassenger(int id) 
        {
            
        }

        public PassengerShip(string name, char[] IMO, float length, float width) : base(name, IMO, length, width)
        {
        }
    }
}