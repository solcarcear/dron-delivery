using drone_delivery.Dto;


namespace drone_delivery.Services
{
    public interface IDelivery
    {


        public void LoadData();

        public IEnumerable<DroneSchedule> GenerateSchedule();
        
    }
}
