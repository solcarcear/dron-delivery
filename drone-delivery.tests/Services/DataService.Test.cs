using drone_delivery.Services;
using drone_delivery.Services.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_delivery.tests.Services
{
    public class DataServiceTest
    {

        private readonly IData _data;

        public DataServiceTest()
        {
            _data = new DataService();
        }

        [Fact]
        public void LoadData_Succes_Case()
        {
            //ARRANGE

            //ACT
            var tpldata= _data.LoadData(@"Data\test1.txt");

            //ASSERT

            Assert.Equal(8,tpldata.Item1.Count());
            Assert.Equal(15,tpldata.Item2.Count());

        }


        [Fact]
        public void LoadData_exceed_dron_limits_Case()
        {

            //ASSERT

            Assert.Throws<Exception>(() => _data.LoadData(@"Data\test1.txt"));

        }

        [Fact]
        public void LoadData_With_empty_lines_Case()
        {

            //ASSERT

            Assert.Throws<Exception>(() => _data.LoadData(@"Data\test2.txt"));

        }
    }
}
