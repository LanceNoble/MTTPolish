using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTTPolish.GameStuff
{
    internal class Summoner
    {
        private int hp;
        private int mp;
        private static Summoner instance;

        private Summoner()
        {

        }

        public static Summoner Instance 
        {
            get
            {
                if (instance == null)
                    instance = new Summoner();
                return instance;
            }
            set { Instance = value; }
        }
    }
}
