using AutoMapper;
using CrmHub.Application.Integration.Models;
using CrmHub.Application.Models.Exact;
using System.Collections.Generic;

namespace CrmHub.Application.Profiles.Exact
{
    public class MappingFieldsProfile : Profile
    {
        private Dictionary<string, string> fields;
        private Dictionary<string, string> entitys;

        public MappingFieldsProfile()
        {
            entitys = new Dictionary<string, string>()
            {
                {"Lead","Lead"},
                {"Contato","Contact"},
                {"Reuniao","Event"},
                {"Endereco", "Address"}
            };

                fields = new Dictionary<string, string>()
            {
                //Schedule
                {"DataFim","End"},
                {"DataIni","Start"},
                {"Endereco","Address"},
                {"TipoReuniao","Type"},
                {"TimeZone","TimeZone"},
                {"Referencia","Subject"},

                //Contact
                {"Nome","Name"},
                {"Cargo","Role"},
                {"Email","Email"},
                {"Telefone","Phones"},
                {"IdMensageiro","MessengerId"},
                {"TipoMensageiro","MessengerType"},

                //Address
                {"Maps","Maps"},
                {"Rua","Street"},
                {"Cidade","City"},
                {"CEP","ZipCode"},
                {"Estado","State"},
                {"Pais","Country"},
                {"Complemento","Complement"},

                //Lead
                //{"Nome","Name"},
                {"Site","Site"},
                {"Origem","Source"},
                {"Mercado","Market"},
                {"LinkExact","Link"},
                {"Observacao","Note"},
                {"Produto","Product"},
                {"DataCadastro","Data"},
                {"PreVendedor","Vendor"},
                //{"Telefone","Phones"},
                {"SubOrigem","SubSource"},
                {"Diagnostico","Diagnosis"}
            };
            this.CreateMap<MapeamentoCampos, MappingFields>()
                .ForMember(s => s.CrmField, i => i.MapFrom(o => o.CampoCRM))
                .ForMember(s => s.CrmEntity, i => i.MapFrom(o => o.TipoEntidadeCRM))
                .ForMember(s => s.ClientField, i => i.MapFrom(o => fields[o.CampoExact]))
                .ForMember(s => s.ClientEntity, i => i.MapFrom(o => entitys[o.TipoEntidadeExact]))
                ;
        }
    }
}
