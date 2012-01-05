using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ConsoleApplication7;

namespace Testing
{

    public abstract class Player : CardHandler, IEquatable<Player>
    {
        private readonly string _name;
        public event Action<Player> Quitted;
        private readonly List<bool?> _results = new List<bool?>();

        public string TheHand
        {
            get { return this.ToStringOfHand(); }
        }


        protected Player(string name)
        {
            _name = name;
        }

        public void Reset()
        {
            this.Clear();
        }

        public abstract PlayAction Play(Card dealersTopCard);

        int _wins;
        int _losses;
        int _pushes;

        public void Payout(int payout)
        {
            if(payout > 0)
                _results.Add(true);
            else if(payout < 0)
                _results.Add(false);
            else
                _results.Add(null);
            Utility.WriteLine("Payout of {0} to {1} with {2}", payout, this.Name, 
            this.Hand.Final);
            if (payout == 0)
            {
                //Console.Out.WriteLine("TIED");
                _pushes++;
            }
            else if (payout > 0)
            {
                //Console.Out.WriteLine("WON");
                _wins++;
            }
            else
            {
                //Console.Out.WriteLine("LOST");
                _losses++;
            }
            this.Reset();
        }

        public string Name { get { return this._name; } }

        public int Losses { get { return this._losses; } }


        protected void Quit()
        {
            Quitted(this);
        }

        public bool Equals(Player other)
        {
            return this == other;
        }

        public override string ToString()
        {
            return string.Format("{0} Wins({1}), Ties({2}), Losses({3}), ", this._name, this._wins, this._pushes, this._losses);
        }

    }
}