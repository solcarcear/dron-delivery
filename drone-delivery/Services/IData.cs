using drone_delivery.Dto;


namespace drone_delivery.Services
{
    public interface IData
    {
        public (IEnumerable<Dron>, IEnumerable<Location>) LoadData(string dataPath);
    }
}
