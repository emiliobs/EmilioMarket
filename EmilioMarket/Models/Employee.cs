using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmilioMarket.Models
{
    //[Table("Empleado")], puedo cambiar el nnombre de la tabla en el modelo de datos
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        //[Column("Name")], puedo cambiar el nombre de la clumna de la tabla del modelo de datos:
        [Display(Name ="First Name")]
        [Required(ErrorMessage ="You must enter {0})")]
        [StringLength(30,ErrorMessage = "The field {0} must between and {2} characters", MinimumLength =3)]
        public string FirstName { get; set; }

        [Display(Name ="Last Name")]
        [StringLength(30, ErrorMessage = "The field {0} must between and {2} characters", MinimumLength = 3)]
        [Required(ErrorMessage = "You must enter {0})")]
        public string  LastName { get; set; }

        //[DataType(DataType.Currency)] me deja ingresar decimales
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter {0})")]
        [DisplayFormat(DataFormatString ="{0:C2}", ApplyFormatInEditMode =false)]
        public decimal Salary { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name ="Bonus %")]
        [DisplayFormat(DataFormatString = "{0:P}", ApplyFormatInEditMode = false)]
        public float BonusPercent { get; set; }

        [Display(Name = "Date of Birth")]
        [Required(ErrorMessage = "You must enter {0})")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = "Start Time")]
        [Required(ErrorMessage = "You must enter {0})")]
        [DisplayFormat(DataFormatString = "{0:hh/mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        [NotMapped]
        public int Age { get { return DateTime.Now.Year - DateOfBirth.Year; } }

        //Llave foranea de la clase DocumentType:
        [Required(ErrorMessage = "You must enter {0})")]
        [Display(Name = "Document Type")]
        //[ForeignKey("DocumentTypeId"], puedo relacinarlo con el nobre de la otra tabla..
        public  int DocumentTypeId { get; set; }

        //lado varios:
        [Display(Name = "Document Type")]
        public virtual DocumentType DocumentType { get; set; }

    }
}                                                  