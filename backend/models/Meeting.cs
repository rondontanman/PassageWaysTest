using System;
using System.Collections.Generic;

namespace backend
{
    public class Meeting
    {
        public string Name { get; set; }
        public User User { get; set; }
        public List<RSVP> RSVPs { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime Date { get; set; }
        public double Length { get; set; }
    }
}