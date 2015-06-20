using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetUp.Domain
{
    public class Runner
    {
        [Key]
        public int Id { get; set; }
        [Index]
        public ApiType  ApiType { get; set; }
        [Index]
        public int? RefId { get; set; }
        public DateTime LastRun { get; set; }
        public DateTime Started { get; set; }
    }
}
