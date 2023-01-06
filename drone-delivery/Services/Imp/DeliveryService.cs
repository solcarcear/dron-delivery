using drone_delivery.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_delivery.Services.Imp
{
    public class DeliveryService : IDelivery
    {
        private readonly IData _data;

        

        public DeliveryService()
        {
            _data = new DataService();
        }

        private IEnumerable<Dron> Drones= new List<Dron>();
        private IEnumerable<Location> Locations = new List<Location>();

    

        public void LoadData()
        {
            var data = _data.LoadData(@"Data\data.txt");
            Drones = data.Item1;
            Locations = data.Item2;
        }

        public IEnumerable<DroneSchedule> GenerateSchedule()
        {
            var result = new List<DroneSchedule>();

            var locationsLeft = new List<Location>(Locations);

            while (Drones.Count()>0 && locationsLeft.Count() > 0)
            {
                result = Drones.Select(drone =>
                {
                    var tripsForDrone = GetScheduledTripsForDrone(drone, result);

                    var locationsPicked = PickLocationsForCapacity(
                        drone.LimitWeight,
                        locationsLeft );

                    if (locationsPicked.Count()>0)
                    {
                        locationsLeft = locationsLeft.Where(
                          location => locationsPicked.All(picked => picked.Name != location.Name)
                        ).ToList();
                        tripsForDrone.Add(locationsPicked);
                    }

                    return new DroneSchedule
                    {
                        Drone = drone,
                        Trips = tripsForDrone
                    };

                }).ToList();
            }
            
            return result;
        }

        public void PrintSchedule(IEnumerable<DroneSchedule> droneSchedules)
        {
            var droneSchedulesArr = droneSchedules.ToArray();
            for (int i = 0; i < droneSchedulesArr.Count(); i++)
            {
                Console.WriteLine($"[Drone #{i + 1} {droneSchedulesArr[i].Drone.Name}]");
                Console.WriteLine();

                var droneTripsArr = droneSchedulesArr[i].Trips.ToArray();
                for (int h = 0; h < droneTripsArr.Count(); h++)
                {

                    Console.WriteLine($"  Trip #{h + 1}");
                    List<string> locationsStr = new List<string>();
                    var locationsArr = droneTripsArr[h].ToArray();
                    for (int j = 0; j < locationsArr.Count(); j++)
                    {
                        locationsStr.Add($"  [Location #{j + 1} {locationsArr[j].Name}]");
                    }
                    Console.WriteLine(string.Join(", ", locationsStr));
                    Console.WriteLine();
                }
            }
        }

        private List<IEnumerable<Location>> GetScheduledTripsForDrone(Dron drone, IEnumerable<DroneSchedule> schedule)
        {
            return schedule.Aggregate(new List<IEnumerable<Location>>(), (trips, droneSchedule) =>
            {

                return (drone == droneSchedule.Drone ? droneSchedule.Trips : trips).ToList();

            });
        }


        private List<Location> PickLocationsForCapacity(int capacity, IEnumerable<Location> locations)
        {
            return locations.Aggregate(new List<Location>(), (locationsPicked, currentLocation) =>
            {
                var currentLoad = CalculateLoadForLocations(locationsPicked);
                var remainingCapacity = capacity - currentLoad;
                if (currentLocation.Weight < remainingCapacity && locations.Count() > 1)
                {
                    var index = locations.ToList().IndexOf(currentLocation) + 1;
                    var found = PickLocationsForCapacity(
                      remainingCapacity - currentLocation.Weight,
                      locations.Skip(index));
                    if (found.Count()>0)
                    {
                        locationsPicked.Add(currentLocation);
                        locationsPicked.AddRange(found);
                        return locationsPicked;
                    }
                }
                else if (currentLocation.Weight <= remainingCapacity)
                {
                    locationsPicked.Add(currentLocation);
                }
                return locationsPicked;
            });

        }

        private int  CalculateLoadForLocations(IEnumerable<Location> locations)
        {
            return locations.Aggregate(0,(weight, loc) => {
                return weight + loc.Weight;
            });
        }
    }
}
