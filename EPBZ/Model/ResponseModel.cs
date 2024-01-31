namespace Model
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
