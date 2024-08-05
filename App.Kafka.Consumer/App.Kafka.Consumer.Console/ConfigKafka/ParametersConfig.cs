using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Kafka.Consumer.Console.ConfigKafka
{
    public static class ParametersConfig
    {
        public const string BOOTSTRAP_SERVER = "localhost:9092";
        public const string TOPIC_NAME = "topic1";
        public const string GROUP_ID = "Group 1";
    }
}
