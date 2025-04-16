using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ShipRegister
{
    public class Ship
    {
        public static int IMOLength = 7;

        private string name;
        private char[] IMO = new char[IMOLength];
        private float length;
        private float width;

        public bool validateIMO()
        {
            for(int i=0; i < IMOLength; i++)
            {
                if (IMO[i] > '9' || IMO[i] < '0')
                    return false;
            }

            // check IMO's check digit 
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

   /* public class TankShip : Ship
    {

    }

    public class PassengerShip : Ship
    {

    }*/
}
    