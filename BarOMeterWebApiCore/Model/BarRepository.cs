using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BarOMeterWebApiCore.Model
{
    public class BarRepository : IRepository
    {
        private List<Bar> _bars;

        // Skal væk langt væk når EFCore kører fint
        public BarRepository()
        {
            if (_bars == null) 
                _bars = new List<Bar>();
            if (_bars.Count ==0)
            {
                var b = new Bar();
                b.BarName = "Katrines Kælder";
                b.Rating = 5;
                _bars.Add(b);
                b = new Bar();
                b.BarName = "Datalogernes Fredagsbar";
                b.Rating = 4;
                _bars.Add(b);
            }
        }
        public async Task<IEnumerable<Bar>> GetBars()
        {
            return _bars;
        }

        public async Task<Bar> GetBar(string barId)
        {
            foreach (var bar in _bars)
            {
                if (bar.BarName == barId)
                    return bar;
            }

            return null;
        }

        public async Task<bool> AddBar(Bar bar)
        {
            foreach (var b in _bars)
            {
                if (b.BarName == bar.BarName)
                    return false;
            }

            _bars.Add(bar);
            return true;
        }

        public async Task<bool> UpdateBar(Bar bar)
        {
            foreach (var b in _bars)
            {
                if (b.BarName == bar.BarName)
                {
                    _bars.Remove(b);
                    _bars.Add(b);
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> RemoveBar(string id)
        {
            Bar b = null;
            bool success = false;

            foreach (var bar in _bars)
            {
                if (bar.BarName == id)
                    b = bar;
            }

            if (b != null)
            {
                success = _bars.Remove(b);
            }

            return success;

            //bool success = _bars.Remove(bar);
            //return success;
        }

        public async Task<int> GetRating(string barId)
        {
            foreach (var bar in _bars)
            {
                if (bar.BarName == barId)
                    return bar.Rating;
            }

            return -1;
        }

        public async Task<int> UpdateRating(string barId, int rating)
        {
            Bar b = null;
            foreach (var bar in _bars)
            {
                if (bar.BarName == barId)
                    b = bar;

            }

            if (b != null)
            {
                b.Rating = rating;
                return rating;
            }
            else return -1;

        }
    }
}