export interface AiRecommendationView {
  rank: number;
  symbol: string;
  category: string;
  score: number;
  source: string;
  reason: string;
  createdAt: string;
  companyName: string;
  basicIndustry: string;
  issueDesc: string | null;
  macro: string;
  sector: string;
  industryInfo: string;
}
