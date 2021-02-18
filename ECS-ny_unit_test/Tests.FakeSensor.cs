using ECS_ny;

namespace ECS_ny_unit_test
{
    public partial class Tests
    {
        public class FakeSensor : ITempSensor
        {
            public int FakeTemperature {  set; get; }

            public int GetTemp()
            {
                return FakeTemperature;
            }

            public bool RunSelfTest()
            {
                if (FakeTemperature == 0) return false;
                else return true;
            }

        }
    }
}