using System.Collections.Generic;

namespace Core.Model
{
    public class ExpressionInfo
    {
        public KeyValuePair<string, string>? Expression { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
