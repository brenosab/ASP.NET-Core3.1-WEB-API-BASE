namespace GestaoCompras.Domain.ViewModels
{
    public class MetaData
    {
        public long TotalCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public long PageCount { get; set; }
        public long PageNumber { get; set; }
    }
}