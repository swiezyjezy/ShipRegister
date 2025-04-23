// See https://aka.ms/new-console-template for more information
using System;
using ShipRegister;

Register register = new();

try
{
    register.AddVessel(new PassengerShip("passenger1", "9074638", 125.5f, 80.5f));
    register.AddVessel(new TankShip("tanker1", "1026831", 125.5f, 80.5f));
    register.AddVessel(new PassengerShip("passenger2", "2948737", 125.5f, 80.5f));
    register.AddVessel(new TankShip("tanker2", "4935473", 125.5f, 80.5f, 8, [20000,10000,5000,40000,60000,1000,40000,20000]));

    var tanker = register.GetVessel(4935473);
    if (tanker is TankShip tankShip)
    {
        tankShip.GetTankAtIndex(1).AddFuelToTank(5000, FuelType.Diesel);
        tankShip.GetTankAtIndex(1).AddFuelToTank(2000, FuelType.Diesel);
        tankShip.GetTankAtIndex(1).RemoveFuelFromTank(3000);
        tankShip.GetTankAtIndex(2).RemoveFuelFromTank(3000);
    }

    var passengerShip = register.GetVessel(2948737);
    if (passengerShip is PassengerShip tempPassengerShip)
    {
        tempPassengerShip.AddPassenger("michal", "nowak");
        tempPassengerShip.AddPassenger("ewa", "nowak");
        tempPassengerShip.AddPassenger("monika", "glowacka");
        tempPassengerShip.AddPassenger("aleksandra", "kowalska");
        tempPassengerShip.AddPassenger("franciszek", "kowalski");

        tempPassengerShip.DeletePassenger(5);
    }

}
catch (Exception e)
{
    Console.WriteLine($"exception thrown: {e.GetType().Name} - {e.Message}");
}