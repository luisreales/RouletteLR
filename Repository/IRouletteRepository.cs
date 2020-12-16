using RouletteLR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteLR.Repository
{
    public interface IRouletteRepository
    {
        public List<Roulette> GetListRoulettes();
        public Roulette GetRouletteById(string Id);
        public Roulette SaveCacheRoullete(Roulette roulette);        

    }

   
}
