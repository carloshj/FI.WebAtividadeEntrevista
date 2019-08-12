using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Beneficiario
    /// </summary>
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        public long IdCliente { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [CpfValidationAttribute(ErrorMessage = "Digite um CPF válido")]
        [RegularExpression(@"^((\d{3}).(\d{3}).(\d{3})-(\d{2}))*$", ErrorMessage = "Digite um CPF no formato 999.999.999-99")]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        public bool IsDeleted { get; set; }

    }
}
