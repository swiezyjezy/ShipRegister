// See https://aka.ms/new-console-template for more information
using System;
using ShipRegister;

Register register = new();

try
{
    PassengerShip ship = new PassengerShip("skibidi", "90745729".ToCharArray(0, 7), 125.5f, 80.5f);
    register.addVessel(ship);
}
catch (Exception e)
{
    Console.WriteLine($"exception thrown: {e.GetType().Name} - {e.Message}");
}