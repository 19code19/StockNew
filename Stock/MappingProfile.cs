using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Stock.Model.EquityListing, EquityListingEntity>().ReverseMap();
        CreateMap<Stock.Model.MetaData, MetaDataEntity>().ReverseMap();
        CreateMap<Stock.Model.OrderBook, OrderBookEntity>().ReverseMap();
        CreateMap<Stock.Model.PriceInfo, PriceInfoEntity>().ReverseMap();
        CreateMap<Stock.Model.SecInfo, SecInfoEntity>().ReverseMap();
        CreateMap<Stock.Model.TradeInfo, TradeInfoEntity>().ReverseMap();
        CreateMap<Stock.Model.YearwiseData, YearwiseDataEntity>().ReverseMap();

        CreateMap<SymbolDataResponse, SymbolDataEntity>().ReverseMap();
        CreateMap<EquitySymbolData, EquitySymbolDataEntity>().ReverseMap(); // ← add this
    }
}