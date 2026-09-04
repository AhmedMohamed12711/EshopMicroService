using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string category):IQuery<GetProductByCategoryByResult>;
    public record GetProductByCategoryByResult(IEnumerable<Product>  Products);
    public  class  GetProductQueryByCategoryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryByResult>
    {
        public async Task<GetProductByCategoryByResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var product = await session.Query<Product>()
                .Where(p=>p.Category.Contains(query.category)).ToListAsync();
       
            return new GetProductByCategoryByResult(product);
        }
    }
}
