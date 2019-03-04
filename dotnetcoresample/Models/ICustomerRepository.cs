using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcoresample.Models
{
    public interface IRedisRepository
    {
        Task<string> GetTopNameAsync(string id);
    }
}
