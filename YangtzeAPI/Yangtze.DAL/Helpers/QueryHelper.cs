using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yangtze.DAL.HelperModels;
using System.Linq.Dynamic.Core;

namespace Yangtze.DAL.Helpers
{
    public static class QueryHelper
    {
        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, QueryStringParameters queryParams)
        {
            if (queryParams.SortParams != null)
            {
                var type = typeof(T);
                var sortOptions = queryParams.SortParams;
                var columnsForSorting = "";
                for (int i = 0; i < sortOptions.Count(); i++)
                {
                    if (type.GetProperty(sortOptions[i].Selector) == null || sortOptions[i].Selector == null)
                    {
                        //if (!string.IsNullOrEmpty(columnsForSorting))
                        //{
                        //    columnsForSorting=columnsForSorting.Remove(columnsForSorting.Length - 1, 1);
                        //}                        
                        //continue;
                        throw new ArgumentException($"Failed sort by \"{sortOptions[i].Selector}\"");
                    };


                    columnsForSorting += sortOptions[i].Selector.ToLower();

                    columnsForSorting += sortOptions[i].Desc ? " Desc" : "";

                    if (i < sortOptions.Count -1)
                    {
                        columnsForSorting += ",";
                    }
                }
                if (!string.IsNullOrEmpty(columnsForSorting))
                    query = query.OrderBy(columnsForSorting);
            }
            return query;
        }

        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, QueryStringParameters queryParams)
        {
            var skip = Convert.ToInt32(queryParams.PageSize * (queryParams.PageIndex - 1));
            var take = Convert.ToInt32(queryParams.PageSize);
            query = query.Skip(skip).Take(take);

            return query;
        }

    }
}
