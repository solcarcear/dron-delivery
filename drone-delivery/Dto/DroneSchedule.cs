

namespace drone_delivery.Dto
{
    public class DroneSchedule
    {

        public Dron Drone { get; set; }

        public IEnumerable<IEnumerable<Location>> Trips { get; set; }

    }
}
