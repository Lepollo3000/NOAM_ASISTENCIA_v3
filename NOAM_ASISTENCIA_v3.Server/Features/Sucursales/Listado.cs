using Ardalis.Result;
using Carter;
using MediatR;
using NOAM_ASISTENCIA_v3.Server.Helpers;
using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Paging;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Features.Sucursales;

public static class Listado
{
    public sealed class Request(SucursalOrder order, SucursalSearch search, PageParameters pageParameters) : SearchParameters<SucursalOrder, SucursalSearch>(order, search, pageParameters), IRequest<Result<PagedList<SucursalTableModel>>> { }

    internal sealed class Handler(IApplicationServices services) : IRequestHandler<Request, Result<PagedList<SucursalTableModel>>>
    {
        public async Task<Result<PagedList<SucursalTableModel>>> Handle(Request request, CancellationToken cancellationToken)
        {
            var tableModelList = await services
                .Sucursales.GetSucursalesAsync(request);

            return Result.Success(tableModelList);
        }
    }
}

public class ListadoEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.GetSucursalesRouteGroup()
            .MapGet("", async ([AsParameters] SucursalOrder order, [AsParameters] SucursalSearch search, [AsParameters] PageParameters pageParameters, IMediator mediator) =>
            {
                var query = new Listado.Request(order, search, pageParameters);
                var result = await mediator.Send(query);

                return result.ToEndpointResult();
            });
    }
}
