using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Oz.Bet
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider;

        public TestBase()
        {
            LoadEnvironmentVariables();

            var serviceCollection = new ServiceCollection();
            LogHelper.Init(serviceCollection);
            RegisterServices(serviceCollection);
            var globalProvider = serviceCollection.BuildServiceProvider(true);
            var scope = globalProvider.CreateScope();
            ServiceProvider = scope.ServiceProvider;

            ResolveCommonServices();
        }

        private void LoadEnvironmentVariables()
        {
            using (var file = File.OpenText("Properties\\launchSettings.json"))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);

                var variables = jObject
                    .GetValue("profiles")
                    //select a proper profile here
                    .SelectMany(profiles => profiles.Children())
                    .SelectMany(profile => profile.Children<JProperty>())
                    .Where(prop => prop.Name == "environmentVariables")
                    .SelectMany(prop => prop.Value.Children<JProperty>())
                    .ToList();

                foreach (var variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
            }
        }
        protected abstract void RegisterServices(ServiceCollection serviceCollection);
        protected virtual void ResolveCommonServices() { }

    }
}
