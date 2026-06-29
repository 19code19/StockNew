using AutoMapper;
using Stock.Data;
using Stock.Model;
using Stock.Entity;

namespace Stock
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Model -> Entity
            CreateMap<Stock.Model.EquityListing, EquityListingEntity>().ReverseMap();
            CreateMap<Stock.Model.MetaData, MetaDataEntity>().ReverseMap();
            CreateMap<Stock.Model.OrderBook, OrderBookEntity>().ReverseMap();
            CreateMap<Stock.Model.PriceInfo, PriceInfoEntity>().ReverseMap();
            CreateMap<Stock.Model.SecInfo, SecInfoEntity>().ReverseMap();
            CreateMap<Stock.Model.TradeInfo, TradeInfoEntity>().ReverseMap();
            CreateMap<Stock.Model.YearwiseData, YearwiseDataEntity>().ReverseMap();
            CreateMap<SymbolDataResponse, SymbolDataEntity>().ReverseMap();
            CreateMap<Stock.Model.IndexData, IndexDataEntity>().ReverseMap();
            CreateMap<Stock.Model.HistoricalTradeData, HistoricalTradeDataEntity>().ReverseMap();
            CreateMap<Stock.Model.PeerComparisonData, PeerComparisonDataEntity>().ReverseMap();
            CreateMap<Stock.Model.ShareholdingPatternEntry, ShareholdingPatternEntryEntity>().ReverseMap();
            CreateMap<FinancialStatus, FinancialStatusEntity>().ReverseMap();
            CreateMap<CorpAction, CorpActionEntity>().ReverseMap();
            CreateMap<CorpAnnualReport, CorpAnnualReportEntity>().ReverseMap();
            CreateMap<CorpBoardMeeting, CorpBoardMeetingEntity>().ReverseMap();
            CreateMap<CorpEventCalendar, CorpEventCalendarEntity>().ReverseMap();
            CreateMap<CorporateAnnouncement, CorporateAnnouncementEntity>().ReverseMap();
            // Add more mappings as needed
        }
    }
}
