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
  showNse?: boolean;
};

export const buildCommonSymbolColumns = ({
  field = 'symbol',
  headerName = 'Symbol',
  pinned = 'left',
  includeFavorite = false,
  renderFavorite,
  showNse = true,
}: CommonSymbolColumnOptions = {}): ColDef[] => [
  {
    colId: 'symbol',
    field,
    headerName,
    minWidth: 140,
    maxWidth: 250,
    width: 140,
    pinned,
    sortable: true,
    filter: true,
    flex: 1,
    cellRenderer: (params: ICellRendererParams) => {
      const symbol = (params.value as string | undefined) ?? '';
      const company = (params.data as { companyName?: string })?.companyName ?? '';
      const slug = createSlug(company);

      return (
        <div className="flex min-w-0 items-center gap-4 py-1 text-[11px]">
          <div className="flex shrink-0 items-center gap-3">
            {includeFavorite && renderFavorite ? (
              <span className="shrink-0">{renderFavorite(params)}</span>
            ) : null}
            {showNse && symbol ? (
              <Link
                className="shrink-0 text-sky-300 hover:text-sky-100 underline"
                to={`https://www.nseindia.com/get-quote/equity/${symbol}/${slug}`}
                target="_blank"
                rel="noreferrer"
              >
                {symbol}
              </Link>
            ) : null}
          </div>
        </div>
      );
    },
  },
];
