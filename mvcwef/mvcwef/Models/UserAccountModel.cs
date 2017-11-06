using System;
using System.ComponentModel.DataAnnotations;

namespace mvcwef.Models
{
    public class UserAccountModel
    {
        [Key]
        public long UserId { get; set; }
        [Required(ErrorMessage = "Primer Nombre es requerido")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Segundo Nombre es requerido")]
        public string SecondName { get; set; }
        [Required(ErrorMessage = "Apellidos son requeridos")]
        public string LastName { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Correo Electrónico es requerido")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Nombre de Usuario es requerido")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password de Usuario es requerida")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Por favor confirme su password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string CompleteName { get { return FirstName + " " + SecondName + " " + LastName; } }
    }
}