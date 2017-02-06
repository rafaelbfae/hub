using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CrmHub.Application.Models.Exact
{
    public abstract class Base<T>
    {
        public string GetEntityNameByAttribute(eCrmName crmName)
        {
            var crmEntity = typeof(T).GetTypeInfo().GetCustomAttributes<CrmAttribute>()
               .Where(x => x.CrmName.Equals(crmName)).FirstOrDefault();
            return crmEntity == null ? "" : crmEntity.Mappings[0];
        }

        public List<MapeamentoCampos> GetFieldsByAttribute(int id, eCrmName crmName)
        {
            List<MapeamentoCampos> fields = new List<MapeamentoCampos>();
            string entityCrm = this.GetEntityNameByAttribute(crmName);

            var mapping = (typeof(T).GetProperties()
                .Select(prop => new
                {
                    Name = prop.Name,
                    PropertieValue = prop.GetValue(this),
                    Fields = prop.GetCustomAttributes(true).OfType<CrmAttribute>()
                            .Where(x => x.CrmName.Equals(crmName))
                            .Select(x => x.Mappings).ToList().FirstOrDefault()
                })).Where(x => x.Fields != null).ToList();

            foreach (var map in mapping)
            {
                if (map.PropertieValue is IList)
                {
                    var propertieValue = map.PropertieValue as IList;
                    var maxLoop = Math.Min(propertieValue.Count, map.Fields.Length);
                    for (int index = 0; index < maxLoop; index++)
                    {
                        fields.Add(new MapeamentoCampos()
                        {
                            Id = id,
                            TipoEntidadeCRM = entityCrm,
                            CampoCRM = map.Fields[index],
                            Valor = propertieValue[index].ToString()
                        });
                    }
                    continue;
                }

                fields.Add(new MapeamentoCampos()
                {
                    Id = id,
                    TipoEntidadeCRM = entityCrm,
                    CampoCRM = map.Fields[0],
                    Valor = map.PropertieValue == null ? "" : map.PropertieValue.ToString()
                });
            }

            return fields;
        }
    }
}
