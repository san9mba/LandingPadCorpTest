namespace Core.Dtos
{
    public class ProductFilterDto: BaseFilterDto
    {
        public string Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }
        public bool IncludeDeleted { get; set; } = false;
    }
}
