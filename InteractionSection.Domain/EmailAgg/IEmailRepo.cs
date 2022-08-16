using Common.Domain;
using InteractionSection.Application.Contracts.EmailApp;

namespace InteractionSection.Domain.EmailAgg
{
    public interface IEmailRepo : IBaseEfRepo<SaveEmail, SearchEmail, ViewEmail, Email>
    {
    }
}
