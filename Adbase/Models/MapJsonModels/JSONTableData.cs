using System.Collections.Generic;

namespace Sciencecom.Models.MapJsonModels
{
    public class JSONTableData
    {
        public string Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public List<JSONStructureForJQGrid> Data { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
    }
}