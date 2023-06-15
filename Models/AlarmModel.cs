using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteTV.Models
{
    public class AlarmModel
    {
        public string name { get; set;}
        public TimeOnly setTime { get; set;}
        public List<string> playmedia { get; set;}
        public bool shuffleMedia { get; set;}
        public bool isActive { get; set;} 
    }
}