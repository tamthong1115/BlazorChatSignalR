using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatModels;

public class Chat
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? Message { get; set; }

    public DateTime TimeStamp { get; set; }
}