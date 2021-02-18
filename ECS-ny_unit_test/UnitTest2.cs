using ECS_ny;
using NUnit.Framework;

namespace ECS_ny_unit_test
{
    public partial class Tests
    {
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

        //Test Regulate
        [TestCase(20,30,true)]
        [TestCase(30, 20, false)]
        [TestCase(30, 30, false)]
        public void Regulate_Regulate30Temp20_HeaterIsOn(int a,int b, bool c)
        {
            sensor.FakeTemperature = a;
            uut.SetThreshold(b);
            uut.Regulate();
            Assert.That(heater.State, Is.EqualTo(c));
        }



    }
}