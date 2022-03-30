using Microsoft.Extensions.DependencyInjection;
using Oz.Bet.DataProvider;
using Xunit;

namespace Oz.Bet.Test
{
    public class DataGeneratorTest : Test
    {
        [Fact]
        public async void generate_bet_data()
        {
            var dataGenerator = ServiceProvider.GetRequiredService<DataGenerator>();
            dataGenerator.GenerateGames();
            await BulletinWriteRepository.ReplaceAsync(dataGenerator.Bulletin);
        }

    }
}