using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductcommand( string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductcommandValidator : AbstractValidator<CreateProductcommand>
    {
        public CreateProductcommandValidator() 
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Name is Reqired");
            RuleFor(x=>x.Category).NotEmpty().WithMessage("Category is Reqired");
            RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("ImageFile is Reqired");
            RuleFor(x=>x.Price).GreaterThan(0).WithMessage("Price must be > 0");  
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session) :
        ICommandHandler<CreateProductcommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductcommand command, CancellationToken cancellationToken)
        {

      

            //create Product
            var Product = new Product
            {
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Name = command.Name,
            };
            //save to database
            session.Store(Product);
            await session.SaveChangesAsync(cancellationToken);
            //return result
            return new CreateProductResult(Product.Id);

        }
    }
}
