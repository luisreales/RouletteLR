using RouletteLR.Models;
using RouletteLR.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteLR.Services
{
    public class RouletteService : IRouletteService
    {
        private IRouletteRepository rouletteRepository;

        public RouletteService(IRouletteRepository rouletteRepository)
        {
            this.rouletteRepository = rouletteRepository;
        }

        public Roulette create()
        {
            Roulette roulette = new Roulette()
            {
                Id = Guid.NewGuid().ToString(),
                IsOpenBoard = false,
                OpenedAt = null,
                ClosedAt = null
            };
            rouletteRepository.SaveCacheRoullete(roulette);
            return roulette;
        }

        public Roulette Find(string Id)
        {
            return rouletteRepository.GetRouletteById(Id);
        }

        public Roulette Open(string Id)
        {
            Roulette roulette = rouletteRepository.GetRouletteById(Id);
            if (roulette == null)
            {
                throw new Exception();
            }

            if (roulette.OpenedAt != null)
            {
                throw new Exception();
            }
            roulette.OpenedAt = DateTime.Now;
            roulette.IsOpenBoard = true;
            roulette.Id = Id;
            return rouletteRepository.SaveCacheRoullete(roulette);
        }

        public Roulette Close(string Id)
        {
            Roulette roulette = rouletteRepository.GetRouletteById(Id);
            if (roulette == null)
            {
                throw new Exception();
            }
            if (roulette.ClosedAt != null)
            {
                throw new Exception();
            }
            roulette.ClosedAt = DateTime.Now;
            roulette.IsOpenBoard = false;
            return rouletteRepository.SaveCacheRoullete(roulette);
        }

        public Roulette Bet(string Id, string UserId, int position, double moneyBet)
        {
            if (moneyBet > 10000 || moneyBet < 1)
            {
                throw new Exception();
            }
            Roulette roulette = rouletteRepository.GetRouletteById(Id);
            if (roulette == null)
            {
                throw new Exception();
            }

            if (roulette.IsOpenBoard == false)
            {
                throw new Exception();
            }

            double value = 0d;
            roulette.BoardList[position].TryGetValue(UserId, out value);
            roulette.BoardList[position].Remove(UserId + "");
            roulette.BoardList[position].TryAdd(UserId + "", value + moneyBet);
            roulette.Id = Id;

            return rouletteRepository.SaveCacheRoullete(roulette);
        }

        public List<Roulette> GetAll()
        {
            return rouletteRepository.GetListRoulettes();
        }
    }
}
