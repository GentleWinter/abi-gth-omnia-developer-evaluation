using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleProfile : Profile
    {
        public GetSaleProfile()
        {
            CreateMap<Guid, Application.Users.GetUser.GetUserCommand>()
                .ConstructUsing(id => new Application.Users.GetUser.GetUserCommand(id));
        }
    }
}
