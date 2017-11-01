using System;
using System.ComponentModel.DataAnnotations;

namespace comReaderLib.Domain
{
    public class CheckPointEntry
    {
        public int Id { get; set; }
        public string DeviceNumber { get; set; }
        public string CardNumber { get; set; }
        public DateTime CheckDate { get; set; }
    }
}
