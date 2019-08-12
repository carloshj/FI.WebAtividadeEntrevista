using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL
{
    internal class DaoBenef: AcessoDados
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="benef">Objeto de beneficiario</param>
        internal long Incluir(DML.Beneficiario benef,long idCliente)
        {
            if (VerificarExistencia(benef.CPF, idCliente))
            {
                return 0;
            }

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", benef.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", benef.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_IncBenef", parametros);
            long ret = 0;
            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
            return ret;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Beneficiario> Listar(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", idCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<DML.Beneficiario> benef = Converter(ds);

            return benef;
        }

        /// <summary>
        /// Inclui um novo beneficario
        /// </summary>
        /// <param name="benef">Objeto de beneficiario</param>
        internal void Alterar(DML.Beneficiario benef,long idCliente)
        {
            if (VerificarExistencia(benef.CPF, idCliente,benef.Id))
            {
                return;
            }

            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", benef.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cpf", benef.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", benef.Id));

            base.Executar("FI_SP_AltBenef", parametros);
        }

        internal bool VerificarExistencia(string CPF, long IdCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", IdCliente));

            DataSet ds = base.Consultar("FI_SP_VerificaBenef", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal bool VerificarExistencia(string CPF, long IdCliente,long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IdCliente", IdCliente));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_VerificaBenef2", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        /// <summary>
        /// Excluir Beneficiario
        /// </summary>
        /// <param name="cliente">Objeto de Beneficiario</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelBenef", parametros);
        }

        private List<DML.Beneficiario> Converter(DataSet ds)
        {
            List<DML.Beneficiario> lista = new List<DML.Beneficiario>();
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DML.Beneficiario benef = new DML.Beneficiario();
                    benef.Id = row.Field<long>("Id");
                    benef.IdCliente = row.Field<long>("IdCliente");
                    benef.Nome = row.Field<string>("Nome");
                    benef.CPF = row.Field<string>("CPF");
                    lista.Add(benef);
                }
            }

            return lista;
        }
    }
}
