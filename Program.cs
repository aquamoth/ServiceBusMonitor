using CommandLine;
using Microsoft.Azure.ServiceBus.Management;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceBusMonitor
{
    class Program
    {
        static int Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args)
                .MapResult(
                    async options => await MainAsync(options),
                    _ => Task.FromResult(NagiosStatus.Unknown)
                );

            try
            {
                return (int)result.Result;
            }
            catch (AggregateException ex)
            {
                Console.Error.WriteLine($"{NagiosStatus.Unknown} Runtime exception: {string.Join(", ", ex.InnerExceptions.Select(x => x.Message))}");
                return (int)NagiosStatus.Unknown;
            }
        }

        static async Task<NagiosStatus> MainAsync(Options options)
        {
            //if (string.IsNullOrWhiteSpace(options.ConnectionString))
            //{
            //    Console.WriteLine("Failed to parse command line");
            //    return (int)NagiosStatus.Unknown;
            //}

            var managementClient = new ManagementClient(options.ConnectionString);
            var result = await QueryServicebusAsync(managementClient, options.Queue);

            var message = $"Servicebus responded in {result.ResponseTime} ms";
            var statistics = $"queues={result.Queues},messages={result.Messages}";

            var statusCode = StatusCodeFrom(options, result.Messages);
            Console.WriteLine($"{statusCode} {message}|{statistics}");

            return statusCode;
        }

        private static async Task<ServiceBusResult> QueryServicebusAsync(ManagementClient managementClient, string queuePath)
        {
            int queueCount;
            long messageCount;

            var sw = Stopwatch.StartNew();
            if (string.IsNullOrWhiteSpace(queuePath))
            {
                var queues = await managementClient.GetQueuesRuntimeInfoAsync();
                queueCount = queues.Count();
                messageCount = queues.Sum(info => info.MessageCount);
            }
            else
            {
                var info = await managementClient.GetQueueRuntimeInfoAsync(queuePath);
                queueCount = 1;
                messageCount = info.MessageCount;
            }
            sw.Stop();

            return new ServiceBusResult
            {
                Messages = messageCount,
                Queues = queueCount,
                ResponseTime = sw.ElapsedMilliseconds
            };
        }

        private static NagiosStatus StatusCodeFrom(Options options, long messageCount)
        {
            if (messageCount >= options.Critical)
                return NagiosStatus.Critical;

            if (messageCount >= options.Warning)
                return NagiosStatus.Warning;

            return NagiosStatus.OK;
        }
    }
}
