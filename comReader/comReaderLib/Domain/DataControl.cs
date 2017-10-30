using System;
using System.ComponentModel.DataAnnotations;

namespace comReaderLib.Domain
{
    public class DataControl
    {
        [Key]
        public int DataId { get; set; }
        public string NumberDevice { get; set; }
        public string NumberCard { get; set; }
        public DateTime Time { get; set; }
    }
}
