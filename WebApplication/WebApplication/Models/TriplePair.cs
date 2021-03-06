using System;

namespace WebApplication.Models
{
    public class TriplePair
    {
        public TriplePair(DateTime dateTime, int index, int value)
        {
            this.Index = index;
            this.Value = value;
            this.DateTime = dateTime;
        }

        public DateTime DateTime { get; set; }
        public int Index { get; set; }
        public int Value { get; set; }
    }
}