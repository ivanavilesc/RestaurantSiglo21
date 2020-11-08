using CapaAccesoDatos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurantSigloXXI.Validaciones
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {

        public UsuarioValidator()
        {
            RuleFor(p => p.USERID)
                .Cascade(CascadeMode.StopOnFirstFailure) //Este control hará que se vaya mostrando un mensaje a la vez, dependiendo del control que no se esté cumpliendo
                .NotEmpty().WithMessage("El usuario esta vacío")
                .Length(5, 10).WithMessage("El usuario debe tener entre 5 y 10 caracteres")
                .Must(LetrasValidas).WithMessage("El campo usuario contiene caracteres inválidos"); //Aqui se muestran el control de los caracteres no permititos en metodo customer


            RuleFor(p => p.PASSWORD)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("La contraseña está vacía")
                .MinimumLength(4).WithMessage("La contraseña debe tener al menos 4 caracteres");
        }

        protected bool LetrasValidas(string usuario) //Metodo customer, en este caso para reemplazar caracteres especiales no admitidos
        {
            usuario = usuario.Replace(" ", ""); //Si el usuario trae espacios los borra
            usuario = usuario.Replace("!", "!");
            usuario = usuario.Replace("#", "#");
            usuario = usuario.Replace("$", "$");
            usuario = usuario.Replace("%", "%");
            usuario = usuario.Replace("&", "&");
            usuario = usuario.Replace("/", "/");
            usuario = usuario.Replace("'\'", "'\'");
            usuario = usuario.Replace("°", "°");
            usuario = usuario.Replace("¬", "¬");
            usuario = usuario.Replace("(", "(");
            usuario = usuario.Replace(")", ")");
            usuario = usuario.Replace("=", "=");
            usuario = usuario.Replace("?", "?");
            usuario = usuario.Replace("'", "'");
            usuario = usuario.Replace("¿", "¿");
            usuario = usuario.Replace("¡", "¡");
            usuario = usuario.Replace("|", "|");
            usuario = usuario.Replace("{", "{");
            usuario = usuario.Replace("}", "}");
            usuario = usuario.Replace("[", "[");
            usuario = usuario.Replace("]", "]");
            usuario = usuario.Replace("+", "+");
            usuario = usuario.Replace("¨", "¨");
            usuario = usuario.Replace("ñ", "ñ");
            usuario = usuario.Replace("*", "*");
            usuario = usuario.Replace("´", "´");
            usuario = usuario.Replace("<", "<");
            usuario = usuario.Replace(">", ">");
            usuario = usuario.Replace(",", ",");
            usuario = usuario.Replace(";", ";");
            usuario = usuario.Replace(".", ".");
            usuario = usuario.Replace(":", ":");
            usuario = usuario.Replace("-", "-");
            usuario = usuario.Replace("_", "_");
            usuario = usuario.Replace("^", "^");
            usuario = usuario.Replace("`", "`");
            usuario = usuario.Replace("~", "~");

            return usuario.All(Char.IsLetter); //Va a devolver un string que solo sean letras
        }

    }
}
