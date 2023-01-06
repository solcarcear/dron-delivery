// See https://aka.ms/new-console-template for more information
using drone_delivery;
using drone_delivery.Dto;
using drone_delivery.Services.Imp;

var dronDelivery = new DeliveryService();

dronDelivery.LoadData();


var schedule = dronDelivery.GenerateSchedule();


dronDelivery.PrintSchedule(schedule);

Console.ReadLine();
