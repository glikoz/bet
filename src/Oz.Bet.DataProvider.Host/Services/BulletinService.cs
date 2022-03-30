using BetProto;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Oz.Bet.DataProvider.Host.Services
{
    public class BulletinService : Bulletin.BulletinBase
    {
        private readonly ILogger<BulletinService> logger;
        private readonly DataGenerator dataGenerator;

        public BulletinService(ILogger<BulletinService> logger, DataGenerator dataGenerator)
        {
            this.logger = logger;
            this.dataGenerator = dataGenerator;
        }

        public override async Task SubscribeBulletinStream(Empty request, IServerStreamWriter<BulletinResult> responseStream, ServerCallContext context)
        {
            int i = 0;
            while (true)
            {
                try
                {
                    await Task.Delay(1000);
                    dataGenerator.ChangeOddsRandomly();
                    logger.LogInformation("GENERATOR ");
                    await responseStream.WriteAsync(Mapper.Map(dataGenerator.Bulletin));
                    i++;
                }
                catch (Exception)
                {
                    await responseStream.WriteAsync(Mapper.Map(dataGenerator.Bulletin));
                }
            }
        }

        public override async Task<BulletinResult> GetBulletin(Empty request, ServerCallContext context)
        {
            return Mapper.Map(dataGenerator.Bulletin);
        }
    }
}
