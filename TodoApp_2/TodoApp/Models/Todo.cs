using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class Todo
    {
        [Required]
        [DisplayName("登録No")]
        public int Id { get; set; }

        [Required]
        [DisplayName("名前")]
        public string Title { get; set; }

        [Required]
        [DisplayName("内容")]
        public string Detail { get; set; }

        [Required]
        [DisplayName("パスワード")]
        public string Pass { get; set; }

        [DisplayName("完了")]
        public bool Done { get; set; }

        public virtual User User { get; set; }
    }
}