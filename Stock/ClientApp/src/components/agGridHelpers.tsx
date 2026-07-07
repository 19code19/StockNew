import { type ICellRendererParams } from '@ag-grid-community/core';
import { useCallback, useEffect, useState } from 'react';
import FavoriteToggle from './FavoriteToggle';

type FavoriteSymbolRow = { symbol?: string | null; assetType?: string | null };

import { apiUrl } from '../api/api';

const normalizeSymbol = (symbol?: string | null) => symbol?.trim().toUpperCase() ?? '';
const normalizeAssetType = (assetType?: string | null) => (assetType?.trim().toLowerCase() || 'stock');

export const useFavoriteGridState = (defaultAssetType: string = 'stock') => {
  const [favoriteSymbols, setFavoriteSymbols] = useState<Set<string>>(new Set());

  const fetchFavorites = useCallback(async () => {
    try {
      const response = await fetch(apiUrl('/api/favorites'));
      if (!response.ok) {
        setFavoriteSymbols(new Set());
        return;
      }

      const favorites = (await response.json()) as FavoriteSymbolRow[];
      setFavoriteSymbols(new Set(
        favorites
          .map((fav) => `${normalizeAssetType(fav.assetType)}:${normalizeSymbol(fav.symbol)}`)
          .filter((key) => key !== 'stock:')
      ));
    } catch {
      setFavoriteSymbols(new Set());
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const toggleFavorite = useCallback(
    async (symbol: string, companyName: string, isFavorite: boolean, assetType: string = defaultAssetType) => {
      const normalizedAssetType = normalizeAssetType(assetType);
      const url = `/api/favorites?symbol=${encodeURIComponent(symbol)}&assetType=${encodeURIComponent(normalizedAssetType)}${
        isFavorite ? '' : `&companyName=${encodeURIComponent(companyName)}`
      }`;
      const method = isFavorite ? 'DELETE' : 'POST';

      try {
        const response = await fetch(url, { method });
        if (!response.ok) {
          return;
        }

        await fetchFavorites();
      } catch {
        // Swallow errors here; the parent grid can decide how to surface them if needed.
      }
    },
    [defaultAssetType, fetchFavorites],
  );

  const renderFavoriteButton = useCallback(
    (params: ICellRendererParams): JSX.Element | null => {
      const assetType = normalizeAssetType(params.data?.assetType as string | null);
      const symbol = assetType === 'mutualfund'
        ? (params.data as { schemeId?: string }).schemeId
        : params.data?.symbol as string | undefined;
      const rawCompanyName = params.data?.companyName as string | undefined;
      const fallbackName = assetType === 'mutualfund'
        ? ((params.data as { schemeName?: string; fundName?: string }).schemeName || (params.data as { schemeName?: string; fundName?: string }).fundName)
        : undefined;
      const companyName = rawCompanyName || fallbackName || symbol;
      if (!symbol) return null;

      const normalizedSymbol = normalizeSymbol(symbol);
      const favoriteKey = `${assetType}:${normalizedSymbol}`;
      const isFavorite = favoriteSymbols.has(favoriteKey);

      return (
        <FavoriteToggle
          symbol={symbol}
          companyName={companyName}
          isFavorite={isFavorite}
          onToggle={() => void toggleFavorite(symbol, companyName ?? '', isFavorite, assetType)}
        />
      );
    },
    [favoriteSymbols, toggleFavorite],
  );

  return { favoriteSymbols, renderFavoriteButton };
};