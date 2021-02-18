namespace ECS_ny
{
    public class Application
    {
        public static void Main(string[] args)
        {
            var sensor = new TempSensor();
            var heater = new Heater();
            var ecs = new ECS(28, sensor, heater);

            ecs.Regulate();

            ecs.SetThreshold(20);

            ecs.Regulate();
        }
    } 
}
