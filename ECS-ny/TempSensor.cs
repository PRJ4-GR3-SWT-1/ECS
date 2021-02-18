using System;

namespace ECS_ny
{
    public interface ITempSensor
    {
        int GetTemp();
        bool RunSelfTest();
    }
    public class TempSensor : ITempSensor
    {
        private Random gen = new Random();

        public int GetTemp()
        {
            return gen.Next(-5, 45);
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}