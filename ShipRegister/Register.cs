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

        public void AddVessel(Ship ship)
        {
            int imo = ship.getIMOIntValue();
            if (vessels.ContainsKey(imo))
            {
                Console.WriteLine($"Vessel with IMO {imo} is already in the register.");
                return;
            }

            vessels.Add(ship.getIMOIntValue(), ship);
            Console.WriteLine($"vessel {ship.GetName()} was added to register");
        }

        public Ship? GetVessel(int key)
        {
            if (vessels.ContainsKey(key))
            {
                return vessels[key];
            }
            else return null;
        }

    }
}
