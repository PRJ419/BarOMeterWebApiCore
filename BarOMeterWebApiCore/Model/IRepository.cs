using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BarOMeterWebApiCore.Model
{
    public interface IRepository
    {
        Task<IEnumerable<Bar>> GetBars();

        //Specific bars
        Task<Bar> GetBar(string barId);
        Task<bool> AddBar(Bar bar);
        Task<bool> UpdateBar(Bar bar);
        Task<bool> RemoveBar(string id);

        //Ratings
        Task<int> GetRating(string barid);
        //Task<bool> UpdateRating(string barId, int rating);
        Task<int> UpdateRating(string id, int rating);
    }
}