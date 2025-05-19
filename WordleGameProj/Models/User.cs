// Models/User.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordleGameProj.Models
{
    public class User   // must be public
    {
        [Key] public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int MaxStreak { get; set; }
        public int CurrentStreak { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
