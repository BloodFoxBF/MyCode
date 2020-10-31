using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Extensions
{
    public static class MapCollectionTo<Dst> where Dst : class
    {
        public static IEnumerable<Dst> From<Src>(IEnumerable<Src> obj) where Src : class
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Src, Dst>()).CreateMapper();
            return mapper.Map<IEnumerable<Dst>>(obj);
        }
    }
}
