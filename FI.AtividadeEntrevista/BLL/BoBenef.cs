using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBenef
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="benef">Objeto de beneficiario</param>
        /// /// <param name="idCliente">ID do cliente</param>
        public long Incluir(DML.Beneficiario benef,long idCliente)
        {
            DAL.DaoBenef ben = new DAL.DaoBenef();
            return ben.Incluir(benef,idCliente);
        }

        /// <summary>
        /// Altera um beneficiario
        /// </summary>
        /// <param name="benef">Objeto de beneficiario</param>
        /// <param name="idCliente">ID do cliente</param>
        public void Alterar(DML.Beneficiario benef, long idCliente)
        {
            DAL.DaoBenef ben = new DAL.DaoBenef();
            ben.Alterar(benef,idCliente);
        }

        /// <summary>
        /// Listar os beneficiario pelo id do cliente
        /// </summary>
        /// <param name="idCliente">id do cliente</param>
        /// <returns></returns>
        public List<DML.Beneficiario> Listar(long idCliente)
        {
            DAL.DaoBenef ben = new DAL.DaoBenef();
            return ben.Listar(idCliente);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DAL.DaoBenef ben = new DAL.DaoBenef();
            ben.Excluir(id);
        }

    }
}
