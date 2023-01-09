namespace N5Challenge.Transverse.Dto
{
    public class KafkaOptionsDto
    {
        public string KafkaServer { get; set; }
        public string TopicName { get; set; }

        public int CancellationDelayMaxMs { get; set; }
    }
}
