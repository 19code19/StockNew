using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;

namespace Stock.Service;

public class MutualFundService
{
    private const string BaseEndpoint = "v1/api/search/v3/query/filter_derived_data/st_filter";
    private readonly MutualFundRepository _mutualFundRepository;
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private const string MutualFundSchemesKey = "mutual-fund-schemes-rows";

    public MutualFundService(MutualFundRepository mutualFundRepository, HttpClient httpClient, IMemoryCache memoryCache)
    {
        _mutualFundRepository = mutualFundRepository;
        _httpClient = httpClient;
        _memoryCache = memoryCache;
    }

    public async Task<int> RefreshMutualFundSchemesAsync()
    {
        var schemes = await GetAllMutualFundSchemesAsync();
        var entities = schemes.Select(ToEntity).ToList();
        return await _mutualFundRepository.SaveMutualFundSchemesAsync(entities);
    }

    public async Task<IReadOnlyList<MutualFundSchemeEntity>> GetMutualFundSchemesAsync()
    {
        if (_memoryCache.TryGetValue(MutualFundSchemesKey, out List<MutualFundSchemeEntity>? cachedRows) && cachedRows is not null)
        {
            return cachedRows;
        }
        var rows = await _mutualFundRepository.GetMutualFundSchemesAsync();
        _memoryCache.Set(MutualFundSchemesKey, rows, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
            SlidingExpiration = TimeSpan.FromHours(1),
        });
        return rows;
    }

    private async Task<IReadOnlyList<MutualFundScheme>> GetAllMutualFundSchemesAsync()
    {
        const int pageSize = 10;
        var page = 1;
        var result = new List<MutualFundScheme>();

        while (true)
        {
            var pageResponse = await FetchPageAsync(page, pageSize);
            if (pageResponse is null)
            {
                break;
            }

            if (pageResponse.Content.Count == 0)
            {
                break;
            }

            result.AddRange(pageResponse.Content);
            if (pageResponse.Total <= result.Count)
            {
                break;
            }

            page++;
        }

        return result;
    }

    private async Task<MutualFundResponse?> FetchPageAsync(int page, int size)
    {
        var requestUrl = $"{BaseEndpoint}?available_for_investment=true&doc_type=scheme&index=false&page={page}&plan_type=Direct&scheme_type=Growth&size={size}&sort_by=3";
        try
        {
            return await _httpClient.GetFromJsonAsync<MutualFundResponse>(requestUrl);
        }
        catch
        {
            return null;
        }
    }

    private static MutualFundSchemeEntity ToEntity(MutualFundScheme scheme)
    {
        return new MutualFundSchemeEntity
        {
            SchemeId = scheme.SchemeId,
            SchemeName = scheme.SchemeName,
            FundHouse = scheme.FundHouse,
            FundName = scheme.FundName,
            DirectFund = scheme.DirectFund,
            Category = scheme.Category,
            SubCategory = scheme.SubCategory,
            PlanType = scheme.PlanType,
            SchemeCode = scheme.SchemeCode,
            SchemeType = scheme.SchemeType,
            GrowwVerdictScore = scheme.GrowwVerdictScore,
            Nav = scheme.Nav,
            Return1D = scheme.Return1D,
            Return1Y = scheme.Return1Y,
            Return3M = scheme.Return3M,
            Return3Y = scheme.Return3Y,
            Return5Y = scheme.Return5Y,
            Return6M = scheme.Return6M,
            Return7Y = scheme.Return7Y,
            Return10Y = scheme.Return10Y,
            SipReturn1Y = scheme.SipReturn1Y,
            SipReturn3Y = scheme.SipReturn3Y,
            SipReturn5Y = scheme.SipReturn5Y,
            SipReturn10Y = scheme.SipReturn10Y,
            DocRequired = scheme.DocRequired,
            MinInvestmentAmount = scheme.MinInvestmentAmount,
            MinSipInvestment = scheme.MinSipInvestment,
            Amc = scheme.Amc,
            FundManager = scheme.FundManager,
            DirectSchemeName = scheme.DirectSchemeName,
            DocType = scheme.DocType,
            PageView = scheme.PageView,
            SubSubCategory = string.Join(", ", scheme.SubSubCategory),
            Tags = string.Join(", ", scheme.Tags),
            Aum = scheme.Aum,
            ExpenseRatio = scheme.ExpenseRatio,
            Risk = scheme.Risk,
            RiskRating = scheme.RiskRating,
            AvailableForInvestment = scheme.AvailableForInvestment == 1,
            SipAllowed = scheme.SipAllowed,
            LumpsumAllowed = scheme.LumpsumAllowed,
            LaunchDate = scheme.LaunchDate,
            LogoUrl = scheme.LogoUrl,
            ExitLoad = scheme.ExitLoad,
            LastUpdatedAt = DateTime.UtcNow,
        };
    }
}
