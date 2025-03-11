using BusinessEntities.Enums;
using System;

namespace Core.Dtos
{
    public class OrderFilterDto: BaseFilterDto
    {
        public OrderStatus? Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? MinTotalPrice { get; set; }
        public decimal? MaxTotalPrice { get; set; }
    }
}
