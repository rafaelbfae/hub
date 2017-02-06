using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrmHub.Application.Models.Exact.Roots.Base
{
    public abstract class BaseExact<T>
    {

        protected BaseExact()
        {
            EntidadeCampoValor = new List<Exact.MapeamentoCampos>();
        }

        public Autenticacao Autenticacao { get; set; }
        public List<MapeamentoCampos> MapeamentoCampos { get; set; }
        public List<MapeamentoCampos> EntidadeCampoValor { get; set; }
        public List<CamposPersonalizados> CamposPersonalizados { get; set; }

        public List<MapeamentoCampos> GetFieldsByMapping()
        {
            var campos = new List<MapeamentoCampos>();
            PropertyInfo[] props = typeof(T).GetProperties();
            foreach (var mapeamento in MapeamentoCampos)
            {
                var propInfo = props.Where(w => w.Name.Equals(mapeamento.TipoEntidadeExact)).FirstOrDefault();
                if (propInfo == null) continue;

                var source = propInfo.GetValue(this);
                if (source is IList)
                {
                    var propertieValue = source as IList;
                    for (int index = 0; index < propertieValue.Count; index++)
                    {
                        var tppl = propertieValue[index].GetType().GetProperties().Where(w => w.Name.Equals(mapeamento.CampoExact)).FirstOrDefault();
                        campos.Add(new MapeamentoCampos()
                        {
                            CampoCRM = mapeamento.CampoCRM,
                            TipoEntidadeCRM = mapeamento.TipoEntidadeCRM,
                            Valor = tppl.GetValue(propertieValue[index]).ToString()
                        });
                    }
                    continue;
                }
                var type = source.GetType();
                var tpp = type.GetProperties();
                propInfo = type.GetProperties().Where(w => w.Name.Equals(mapeamento.CampoExact)).FirstOrDefault();
                if (propInfo == null) continue;

                var value = propInfo.GetValue(source);
                campos.Add(new MapeamentoCampos()
                {
                    CampoCRM = mapeamento.CampoCRM,
                    TipoEntidadeCRM = mapeamento.TipoEntidadeCRM,
                    Valor = value == null ? "" : value.ToString()
                });
            }

            return campos;
        }
    }
}
