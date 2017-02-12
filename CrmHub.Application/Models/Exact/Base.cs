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
        struct Map
        {
            public string Name;
            public object PropertieValue;
            public string Entity;
            public string[] Fields;
        };

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

            List<Map> mapping = new List<Map>();
            typeof(T).GetProperties().ToList().ForEach(x =>
            {
                x.GetCustomAttributes(true).OfType<CrmAttribute>().ToList().ForEach(a =>
                {
                    Map map = new Map();
                    map.Name = x.Name;
                    map.PropertieValue = x.GetValue(this);
                    map.Entity = a.HasEntity ? a.Entity : entityCrm;
                    map.Fields = a.Mappings;

                    mapping.Add(map);
                });
            });

            foreach (var map in mapping)
            {
                if (map.Fields is IList)
                {
                    string fieldValue = string.Empty;
                    if (map.PropertieValue is IList)
                    {
                        var propertieValue = map.PropertieValue as IList;
                        fieldValue = propertieValue[0].ToString();
                    }
                    else
                        fieldValue = map.PropertieValue != null ? map.PropertieValue.ToString() : string.Empty;
                        
                    foreach (var value in map.Fields)
                    {
                        fields.Add(new MapeamentoCampos()
                        {
                            Id = id,
                            TipoEntidadeCRM = map.Entity,
                            CampoCRM = value,
                            Valor = fieldValue
                        });
                    }
                    continue;
                }

                fields.Add(new MapeamentoCampos()
                {
                    Id = id,
                    TipoEntidadeCRM = map.Entity,
                    CampoCRM = map.Fields[0],
                    Valor = map.PropertieValue == null ? "" : map.PropertieValue.ToString()
                });
            }

            return fields;
        }
        
        private string[] GetFields(PropertyInfo prop, eCrmName crmName)
        {
            return
                prop.GetCustomAttributes(true).OfType<CrmAttribute>()
                            .Where(x => x.CrmName.Equals(crmName))
                            .Select(x => x.Mappings).ToList().FirstOrDefault();
        }
    }
}
