using CrmHub.Application.Custom;
using CrmHub.Application.Integration.Enuns;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CrmHub.Application.Models.Exact
{
    public abstract class Base<T>
    {
        struct Map
        {
            public string Name;
            public object PropertieValue;
            public string Entity;
            public string Format;
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
                    map.Format = a.Format;
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
                        var maxLoop = Math.Min(propertieValue.Count, map.Fields.Length);
                        for (int index = 0; index < maxLoop; index++)
                        {
                            fields.Add(new MapeamentoCampos()
                            {
                                Id = id,
                                TipoEntidadeCRM = map.Entity,
                                CampoCRM = map.Fields[index],
                                Valor = ConvertToString(propertieValue[index], map.Format)
                            });
                        }
                        continue;
                    }
                }

                fields.Add(new MapeamentoCampos()
                {
                    Id = id,
                    TipoEntidadeCRM = map.Entity,
                    CampoCRM = map.Fields[0],
                    Valor = ConvertToString(map.PropertieValue, map.Format)
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

        private string ConvertToString(object value, string format)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (format != null && value is DateTime)
            {
                return ((DateTime)value).ToString(format);
            }

            return value.ToString();
        }
    }
}
