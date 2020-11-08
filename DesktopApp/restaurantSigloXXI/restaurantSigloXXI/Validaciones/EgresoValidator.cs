using CapaAccesoDatos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restaurantSigloXXI.Validaciones
{
    public class EgresoValidator : AbstractValidator<Egreso>
    {
        public EgresoValidator()
        {
            RuleFor(p => p.IDEGRESO)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("El campo ID Egreso está vacío")
                .LessThan(0).WithMessage("El valor de ID Egreso no puede ser menor que cero").WithSeverity(Severity.Error)
                ;//AGREGAR VALIDACIÓN PARA QUE DEJE PASAR SOLO NUMEROS
                 //https://www.google.com/search?sxsrf=ALeKk02zssQjTV-lvL062LZr1Uj6h3Hssw%3A1603262366573&ei=ntePX-3EIvm65OUP2r2R-A8&q=validation+only+numbers+with+fluent+using+must&oq=validation+only+numbers+with+fluent+using+must&gs_lcp=CgZwc3ktYWIQAzoECAAQRzoICCEQFhAdEB46BwghEAoQoAE6BQghEKABOgQIIRAVUKcZWKxOYLdUaAZwAngAgAGvAYgBqQeSAQQxMi4xmAEAoAEBqgEHZ3dzLXdpesgBCMABAQ&sclient=psy-ab&ved=0ahUKEwjti93eicXsAhV5HbkGHdpeBP8Q4dUDCA0&uact=5


            RuleFor(p => p.DESCMOVIMIENTO)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("La descripción esta vacía").WithSeverity(Severity.Warning)
                .Length(5).WithMessage("La descripción debe tener como minimo 5 caracteres")
                ;

            //CONTINUAR CON TODOS LOS DEMAS CAMPOS E IMPLEMENTAR EN Egresos.xaml.cs

        }




    }
}
