using Catalog.Application.Responses;
using MediatR;


namespace Catalog.Application.Queries
{
    public partial record GetAllBrandsQuery
    {
        public record GetProductsByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>;
    }
}
