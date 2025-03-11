using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace ConnectLogger.Connections.Api.Infrastructure.Filters;

public class EnableQueryFilter : IOperationFilter
{
    static List<OpenApiParameter> s_Parameters = new List<(string Name, string Description)>()
            {
                ( "$select", "Выбор определенных полей. Используйте запятую для списка."),
                ( "$filter", "Фильтрация выборки."),
                ( "$orderby", "Сортировка результата."),
                ( "$top", "Максимальное количество выбираемых записей."),
                ( "$skip", "Количество записей, которые нужно пропустить."),
                ( "$apply", "Apply метод."),
                ( "$expand", "Expand метод.")
            }.Select(pair => new OpenApiParameter
            {
                Name = pair.Name,
                Required = false,
                Schema = new OpenApiSchema { Type = "String" },
                In = ParameterLocation.Query,
                Description = pair.Description


            }).ToList();

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(em => em is Microsoft.AspNetCore.OData.Query.EnableQueryAttribute))
        {

            operation.Parameters ??= new List<OpenApiParameter>();
            foreach (var item in s_Parameters)
                operation.Parameters.Add(item);
        }
    }
}

