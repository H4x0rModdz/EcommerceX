using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Models
{
    public class UpdateUserModel
    {
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}