using ECS_ny;

namespace ECS_ny_unit_test
{
    public partial class Tests
    {
        public class FakeHeater : IHeater
        {
            public bool State { set; get; }

            public void TurnOn()
            {
                State = true;
            }

            public void TurnOff()
            {
                State = false;
            }

            public bool RunSelfTest()
            {
                return State;
            }
        }
    }
}