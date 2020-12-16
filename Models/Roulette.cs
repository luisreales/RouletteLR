using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteLR.Models
{
    [Serializable]
    public class Roulette
    {
        public string Id { get; set; }

        public bool IsOpenBoard { get; set; } = false;

        public DateTime? OpenedAt { get; set; }

        public DateTime? ClosedAt { get; set; }

        public IDictionary<string, double>[] BoardList { get; set; } = new IDictionary<string, double>[36];
        
        public double TotalBetBoard { get; set; }
        public double TotalMoneyWinByUser { get; set; }
        public double NumberWinBoard { get; set; }

        public Roulette()
        {
            this.Init();
        }

        private void Init()
        {
            for (int i = 0; i < BoardList.Length; i++)
            {
                BoardList[i] = new Dictionary<string, double>();
            }
        }
    }
}
