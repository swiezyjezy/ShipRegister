using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipRegister
{
    internal class Register
    {
        private Dictionary<int, Ship> vessels = new();

        public void addVessel(Ship ship)
        {
            vessels.Add(ship.getIMOIntValue(), ship);
            Console.WriteLine($"vessel {ship.getName()} was added to register");
        }
    }
}
