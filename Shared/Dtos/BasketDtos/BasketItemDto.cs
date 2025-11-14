using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos.BasketDtos
{
    public record BasketItemDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        [Range(1,double.MaxValue)]
        public decimal Price { get; set; }
        [Range(1,99)]
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
    }
}