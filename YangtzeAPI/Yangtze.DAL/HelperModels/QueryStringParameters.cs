using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.DAL.HelperModels
{
    public class QueryStringParameters
    {
        private const int MaxPageSize = 96;
        public int PageIndex { get; set; } = 1;
        private string _sortString { get; set; }
        public IList<QueryStringOrderGroupParameter> SortParams { get; set; }
        private int _pageSize = 12;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        [FromQuery(Name = "sort")]
        public string SortString
        {
            get => _sortString;
            set
            {
                try
                {
                    if (!value.ToLower().Equals("null") && !value.ToLower().Equals("undefined"))
                    {
                        SortParams = JsonConvert.DeserializeObject<IList<QueryStringOrderGroupParameter>>(value);
                        _sortString = value;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Incorrect sort parameter!");
                }

            }
        }

    }

    public class QueryStringOrderGroupParameter
    {
        public string Selector{ get; set; }
        public bool Desc { get; set; }
    }
}
