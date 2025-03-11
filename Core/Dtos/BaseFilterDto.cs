namespace Core.Dtos
{
    public class BaseFilterDto
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
    }
}
