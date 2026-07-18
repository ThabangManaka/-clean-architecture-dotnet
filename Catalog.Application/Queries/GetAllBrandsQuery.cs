using Catalog.Application.Responses;
using MediatR;


namespace Catalog.Application.Queries
{
    public partial record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
    {
    }
}
