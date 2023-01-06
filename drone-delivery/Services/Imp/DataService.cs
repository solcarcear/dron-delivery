using drone_delivery.Dto;


namespace drone_delivery.Services.Imp
{
    public class DataService : IData
    {
        public (IEnumerable<Dron>, IEnumerable<Location>) LoadData(string dataPath)
        {
            try
            {
                int counter = 0;
                var drons = new List<Dron>();
                var locations = new List<Location>();

                // Read the file line by line.  
                foreach (string line in File.ReadLines(dataPath))
                {
                    if (counter == 0)
                    {
                        drons = ParseDatadrons(line.Split(',')).ToList();
                        counter++;
                        continue;
                    }

                    locations.Add(ParseDataLocations(line.Split(',')));
                    counter++;
                }
                if (drons.Count() > 100)
                {
                    throw new Exception("The maximum number of drones in a squad is 100");
                }

                return (drons, locations);
            }
            catch (Exception ex)
            {

                throw new Exception("The data format is incorrect, check the data file",ex);
            }
        }
        private IEnumerable<Dron> ParseDatadrons(string[] datadrons)
        {
            var result = new List<Dron>();
            for(int i=0; i < datadrons.Length; i+=2)
            {
                result.Add(new Dron
                {
                    Name = datadrons[i],
                    LimitWeight = int.Parse(datadrons[i + 1])
                });
            }
            return result;
        }
        private Location ParseDataLocations(string[] dataLocations)
        {
            return new Location
            {
                Name= dataLocations[0],
                Weight= int.Parse(dataLocations[1]),
            };
        }
    }

}
