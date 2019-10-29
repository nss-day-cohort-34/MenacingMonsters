using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenacingMonsters.Models
{
    public class Monster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EyeCount { get; set; }
        public string CatchPhrase { get; set; }
    }
}
