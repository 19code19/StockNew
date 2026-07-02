CREATE VIEW vw_AiRecommendations AS
	SELECT 
	A.[Rank],
	A.Symbol,
	A.Category,
	A.Score,
	A.Source,
	A.Reason,
	A.CreatedAt,
	M.CompanyName,
	S.BasicIndustry,
	S.IssueDesc,
	S.Macro,
	S.Sector,
	S.IndustryInfo
FROM AiRecommendations A
INNER JOIN MetaDataEntities M ON A.Symbol = M.Symbol
INNER JOIN SecInfoEntities S ON S.Symbol = M.Symbol
