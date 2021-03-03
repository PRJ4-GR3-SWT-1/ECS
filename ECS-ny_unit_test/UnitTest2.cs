using ECS_ny;
using NSubstitute;
using NUnit.Framework;

namespace ECS_ny_unit_test
{
    // Dette er en meget vigtig kommentar
    public partial class Tests
    {
        private ECS uut;
        private ITempSensor sensor;
        private IHeater heater;
        [SetUp]
        public void Setup()
        {
            sensor = Substitute.For<ITempSensor>();
            heater = Substitute.For<IHeater>();

            uut = new ECS(20, sensor, heater);
        }

        [TestCase(0),Description("TestNul")]
        [TestCase(20)]
        [TestCase(-5)]
        public void GetCurTemp_TempIsInt_ResultMatches(int a)
        {
            
            sensor.GetTemp().Returns(a);
            Assert.That(uut.GetCurTemp(), Is.EqualTo(a));
        }

        //Test Run self test
        [TestCase(false,true,false)]
        [TestCase(false, false, false)]
        [TestCase(true, true, true)]
        [TestCase(true, false, false)]
        public void RunSelfTest_HeaterBoolSensorBool_ResultCorrect(bool heaterState, bool sensorState, bool expectedResult)
        {
            heater.RunSelfTest().Returns(heaterState);
            sensor.RunSelfTest().Returns(sensorState);
            Assert.That(uut.RunSelfTest, Is.EqualTo(expectedResult));
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
        public void Regulate_Regulate30Temp20_HeaterIsOn(int temp,int thresh, bool expectedResult)
        {
            sensor.GetTemp().Returns(temp);
            uut.SetThreshold(thresh);
            uut.Regulate();
            if(expectedResult == true) heater.Received(1).TurnOn();
            else heater.Received(1).TurnOff();
        }



    }
}