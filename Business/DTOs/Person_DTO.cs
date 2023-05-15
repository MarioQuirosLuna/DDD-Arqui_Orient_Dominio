using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class Person_DTO
    {
        public int IdPerson { get; set; }
        [Required(ErrorMessage = "The field FirtsName is required")]
        [StringLength(50)]
        public string FirtsName { get; set; }
        [Required(ErrorMessage = "The field LastName is required")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "The field Age is required")]
        public int Age { get; set; }
    }
}
