using Api.Contracts;
using Domain;
using Mapster;

namespace Api.Mappings;

public static class MapsterConfig
{
    public static void Register(TypeAdapterConfig config)
    {
        // Order -> CreateOrderResponse
        config.NewConfig<Order, CreateOrderResponse>()
            .Map(dest => dest.OrderId, src => src.Id.Value)
            .Map(dest => dest.Status, src => (int)src.Status)
            .Map(dest => dest.ItemsCount, src => src.Items.Count)
            .Map(dest => dest.TotalPrice, src => src.TotalPrice)
            .Map(dest => dest.CancellationReason, src => src.CancellationReason);

        // OrderItemDto -> OrderItemInput (Application)
        // Property names match; default config is sufficient.
    }
}
