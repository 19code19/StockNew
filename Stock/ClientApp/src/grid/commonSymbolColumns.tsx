import { type ColDef, type ICellRendererParams } from '@ag-grid-community/core';
import { Link } from 'react-router-dom';

export const createSlug = (company: string) =>
  company
    .trim()
    .replace(/[^a-zA-Z0-9]+/g, '-')
    .replace(/^-+|-+$/g, '')
    .toLowerCase();

type CommonSymbolColumnOptions = {
  field?: string;
  headerName?: string;
  pinned?: 'left' | 'right';
  includeFavorite?: boolean;
  renderFavorite?: (params: ICellRendererParams) => JSX.Element | null;
  externalLinkUrl?: (symbol: string, company: string, params: ICellRendererParams) => string | null;
  assetType?: 'stock' | 'mutualFund';
  showNse?: boolean;
};

export const buildCommonSymbolColumns = ({
  field,
  headerName,
  pinned = 'left',
  includeFavorite = false,
  renderFavorite,
  externalLinkUrl,
  assetType = 'stock',
  showNse,
}: CommonSymbolColumnOptions = {}): ColDef[] => {
  const defaultField = field ?? (assetType === 'mutualFund' ? 'schemeId' : 'symbol');
  const defaultHeaderName = headerName ?? (assetType === 'mutualFund' ? 'Scheme' : 'Symbol');
  const shouldShowNse = showNse ?? assetType === 'stock';

  return [
    {
      colId: 'symbol',
      field: defaultField,
      headerName: defaultHeaderName,
      minWidth: 140,
      maxWidth: 500,
      width: 140,
      pinned,
      sortable: true,
      filter: true,
      flex: 1,
      cellRenderer: (params: ICellRendererParams) => {
        const symbol = (params.value as string | undefined) ?? '';
        const company = (params.data as { companyName?: string })?.companyName ?? '';
        const slug = createSlug(company);

        const externalLink = externalLinkUrl?.(symbol, company, params);
        const linkAddress = externalLink ?? (shouldShowNse && symbol ? `https://www.nseindia.com/get-quote/equity/${symbol}/${slug}` : null);

        return (
          <div className="flex min-w-0 items-center gap-4 py-1 text-[11px]">
            <div className="flex shrink-0 items-center gap-3">
              {includeFavorite && renderFavorite ? (
                <span className="shrink-0">{renderFavorite(params)}</span>
              ) : null}
              {linkAddress ? (
                <Link
                  className="shrink-0 text-sky-300 hover:text-sky-100 underline"
                  to={linkAddress}
                  target="_blank"
                  rel="noreferrer"
                >
                  {symbol}
                </Link>
              ) : (
                <span>{symbol}</span>
              )}
            </div>
          </div>
        );
      },
    },
  ];
};
