namespace WilmerFlorez.Common.Transverses
{
    public class CommonResult<T>
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
