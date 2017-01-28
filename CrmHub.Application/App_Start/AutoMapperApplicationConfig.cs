using AutoMapper;



namespace CrmHub.Application
{
    internal static partial class AutoMapperApplicationConfig
    {
        public static void ConfigurateMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<LeadProfile>();
                cfg.AddProfile<ContactProfile>();
                cfg.AddProfile<ScheduleProfile>();

            });
        }
    }
}
