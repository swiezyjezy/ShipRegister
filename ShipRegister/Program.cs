// See https://aka.ms/new-console-template for more information
using System;
using ShipRegister;

Console.WriteLine("Hello, World!");

Ship ship = new Ship("skibidi", "4332322".ToCharArray(0,7), 125.5f, 80.5f);

ship.validateIMO();