using System.ComponentModel.DataAnnotations;

namespace BookingApp.Api.Dtos;

public class HotelCreateDto
{
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Name { get; set; }
    [Required]
    [Range(1,5)]
    public int Stars { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Address { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string City { get; set; }
    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Country { get; set; }
    [MaxLength(1000)]
    [MinLength(3)]
    public string Description { get; set; }
}
