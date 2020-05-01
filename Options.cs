using CommandLine;

namespace ServiceBusMonitor
{
    public class Options
    {
        [Value(0, HelpText = "Servicebus connectionstring", Required = true)]
        public string ConnectionString { get; set; }

        [Option(HelpText = "Queue to count messages for. Leave empty to aggregate all queues.")]
        public string Queue { get; set; }

        [Option(HelpText = "Queue length warning level.")]
        public int Warning { get; set; } = int.MaxValue;

        [Option(HelpText = "Queue length critical level.")]
        public int Critical { get; set; } = int.MaxValue;
    }
}
