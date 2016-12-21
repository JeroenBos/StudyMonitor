using System;
using System.Runtime.Serialization;

namespace StudyMonitor.Service
{
    [DataContract]
    public class TaskTimeSpanService
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime? End { get; set; }

        [DataMember]
        public int TaskId { get; set; }
    }
}