namespace DevFreela.API.Services
{
    public interface IConfigService
    {
        int GetIncrement();
    }

    public class ConfigService : IConfigService
    {
        int total = 0;

        public int GetIncrement()
        {
            return total++;
        }
    }
}
