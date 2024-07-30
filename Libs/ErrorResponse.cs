namespace hotel_clone_api.Libs
{
    public class ErrorResponse
    {
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
