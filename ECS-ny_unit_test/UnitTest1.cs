using ECS_ny;
using NUnit.Framework;

namespace ECS_ny_unit_test
{
    public class Tests
    {
        public class FakeSensor : ITempSensor
        {
            public int FakeTemperature { public set; public get; }

        public int GetTemp()
        {
            return FakeTemperature;
        }

        public bool RunSelfTest()
        {
            return true;
        }

        }

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


        private ECS uut;
        private ITempSensor sensor;
        private IHeater heater;
        [SetUp]
        public void Setup()
        {
            sensor = new FakeSensor();
            heater = new FakeHeater();

            uut = new ECS(20, sensor, heater);
        }

        [TestCase(0)]
        public void GetCurTemp_TempIs_ResultMatches(int a)
        {
            sensor.FakeTemperature=a
            Assert.That(uut.GetCurTemp(), Is.);
        }
    }
}