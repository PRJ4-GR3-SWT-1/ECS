using ECS_ny;
using NUnit.Framework;

namespace ECS_ny_unit_test
{
    public class Tests
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
        private FakeSensor sensor;
        private FakeHeater heater;
        [SetUp]
        public void Setup()
        {
            sensor = new FakeSensor();
            heater = new FakeHeater();

            uut = new ECS(20, sensor, heater);
        }

        [TestCase(0),Description("TestNul")]
        [TestCase(20)]
        [TestCase(-5)]
        public void GetCurTemp_TempIsInt_ResultMatches(int a)
        {
            
            sensor.FakeTemperature = a;
            Assert.That(uut.GetCurTemp(), Is.EqualTo(a));
        }

        //Test Run self test
        [Test]
        public void RunSelfTest_HeaterFalseSensorTrue_ResultFalse()
        {
            heater.State = true;
            sensor.FakeTemperature = 0;
            Assert.That(uut.RunSelfTest, Is.False);
        }

        [Test]
        public void RunSelfTest_HeaterFalseSensorFalse_ResultFalse()
        {
            heater.State = false;
            sensor.FakeTemperature = 0;
            Assert.That(uut.RunSelfTest, Is.False);
        }

        [Test]
        public void RunSelfTest_HeaterTrueSensorTrue_ResultTrue()
        {
            heater.State = true;
            sensor.FakeTemperature = 1;
            Assert.That(uut.RunSelfTest, Is.True);
        }

        [Test]
        public void RunSelfTest_HeaterTrueSensorFalse_ResultFalse()
        {
            heater.State = true;
            sensor.FakeTemperature = 0;
            Assert.That(uut.RunSelfTest, Is.False);
        }

        //Test set threshold
        [TestCase(20)]
        [TestCase(30)]
        [TestCase(50000)]
        public void SetThreshold_Set20_ResultTrue(int a)
        {
            uut.SetThreshold(a);
            Assert.That(uut.GetThreshold(), Is.EqualTo(a));
        }

        // Test Regulate
    }
}