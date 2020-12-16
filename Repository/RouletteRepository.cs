using EasyCaching.Core;
using RouletteLR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteLR.Repository
{
    public class RouletteRepository : IRouletteRepository
    {
        private const string KEYTABLEREDIS = "TBLROULETTE";
        private IEasyCachingProviderFactory cachingProviderFactory;
        private IEasyCachingProvider cachingProvider;

        public RouletteRepository(IEasyCachingProviderFactory _cachingProviderFactory)
        {
            this.cachingProviderFactory = _cachingProviderFactory;
            this.cachingProvider = this.cachingProviderFactory.GetCachingProvider("rouletteconfig");
        }

        public List<Roulette> GetListRoulettes()
        {
            List<Roulette> listFiltered;
            var listRoulettes = this.cachingProvider.GetByPrefix<Roulette>(KEYTABLEREDIS);
            if (listRoulettes.Count != 0)
            {
                listFiltered = new List<Roulette>(listRoulettes.Select(x => x.Value.Value));
            }
            else
            {
                listFiltered = new List<Roulette>();
            }

            return listFiltered;
        }
        public Roulette GetRouletteById(string Id)
        {
            var objRoulette = this.cachingProvider.Get<Roulette>(KEYTABLEREDIS + Id);
            if (objRoulette.HasValue == false)
            {
                return null;
            }
            return objRoulette.Value;
        }        

        public Roulette SaveCacheRoullete(Roulette roulette)
        {
            cachingProvider.Set(KEYTABLEREDIS + roulette.Id, roulette, TimeSpan.FromDays(365));
            return roulette;
        }
              
       
    }
}
