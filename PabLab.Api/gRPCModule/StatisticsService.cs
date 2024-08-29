using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Web;

namespace PabLab.WebAPI.gRPCModule
{

    public class StatisticsService : Statistics.StatisticsBase
    {
        int currentCount;

        public StatisticsService(CounterClass counter)
        {
            currentCount = counter.Count;
        }
        public override Task<GetStatisticsResponse> GetStatistics(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetStatisticsResponse
            {
                Counter = currentCount
            });
        }
    }
}