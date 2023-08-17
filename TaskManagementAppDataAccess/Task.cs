using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementAppDataAccess
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100), Required]
        public string Name { get; set; }
        
        public bool IsCompleted { get; set; }
                
        public DateTime? Created_at { get; set; }
    }
}
