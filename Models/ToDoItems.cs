using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiTest.Models
{
    [Table("todo_items")] // точное имя таблицы в БД
    public class ToDoItems
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("iscompleted")]
        public bool IsCompleted { get; set; }

        [Column("imagefilename")]
        public string? ImageFileName { get; set; }
    }
}
