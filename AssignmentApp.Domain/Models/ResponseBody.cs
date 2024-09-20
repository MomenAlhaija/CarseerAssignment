namespace Assignment.Domain.Models;

public class ResponseBody<T> where T : class
{ 
    public int Count { get; set; }
    public string Message { get; set; }
    public string SearchCriteria { get; set; }
    public IEnumerable<T> Results { get; set; }
}

