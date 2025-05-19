// Models/Game.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace WordleGameProj.Models
{
    public class Game   // must be public
    {
        [Key] public int GameId { get; set; }
        public string CorrectWord { get; set; }
        public int AttemptsUsed { get; set; }
        public DateTime PlayedOn { get; set; }
        public int Score { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
