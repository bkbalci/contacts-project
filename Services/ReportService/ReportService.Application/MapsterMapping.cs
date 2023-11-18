using System.Text.Json;
using Mapster;
using MongoDB.Bson;
using Newtonsoft.Json;
using ReportService.Domain.Entities;

namespace ReportService.Application;

public class MapsterMapping : IRegister
{
    void IRegister.Register(TypeAdapterConfig config)
    {
        //config.NewConfig<Report, ReportDto>().Map(dest => dest.Content, src => JsonConvert.DeserializeObject<List<object>>(JsonConvert.SerializeObject(src.Content)));
    }
}