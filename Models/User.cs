using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ShopDbLib.Models
{
    public enum Role
    {
        User, Admin
    }

    public interface IUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }

    }

    public class User : IUser
    {


        public int Id { get; set; }

        [Required(ErrorMessage = "������� ��� ������������")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "������� ������")]
        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }
        [Required(ErrorMessage = "�������  ������� ")]
        [MaxLength(20)]
        public string Phone { get; set; }
        public string Address { get; set; }

        [MaxLength(10)]
        [Required]
        public Role Role { get; set; }
    }
}