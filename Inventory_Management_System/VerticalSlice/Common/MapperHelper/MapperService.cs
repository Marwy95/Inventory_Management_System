using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Inventory_Management_System.VerticalSlice.Common.MapperHelper
{
    public static class MapperService
    {
        public static IMapper Mapper { get; set; }
        public static TResult MapOne<TResult>(this object source)
        {
            return Mapper.Map<TResult>(source);
        }
        public static IQueryable<TResult> Map<TResult>(this IQueryable<object> source)
        {
            return source.ProjectTo<TResult>(Mapper.ConfigurationProvider);
        }

    }
}

