using System.ComponentModel.DataAnnotations;
namespace RedbrowAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese una dirección de correo electrónico válida.")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public string Password { get; set; }
        [Range(18, 99, ErrorMessage = "La edad debe estar entre 18 y 99.")]
        public int Edad { get; set; }
    }
}
