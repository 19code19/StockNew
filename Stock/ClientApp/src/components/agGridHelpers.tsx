import { type ICellRendererParams } from '@ag-grid-community/core';
import { useCallback, useEffect, useState } from 'react';
import FavoriteToggle from './FavoriteToggle';

type FavoriteSymbolRow = { symbol?: string | null };

import { apiUrl } from '../api/api';

const normalizeSymbol = (symbol?: string | null) => symbol?.trim().toUpperCase() ?? '';

export const useFavoriteGridState = () => {
  const [favoriteSymbols, setFavoriteSymbols] = useState<Set<string>>(new Set());

  const fetchFavorites = useCallback(async () => {
    try {
      const response = await fetch(apiUrl('/api/favorites'));
      if (!response.ok) {
        setFavoriteSymbols(new Set());
        return;
      }

      const favorites = (await response.json()) as FavoriteSymbolRow[];
      setFavoriteSymbols(new Set(favorites.map((fav) => normalizeSymbol(fav.symbol)).filter(Boolean)));
    } catch {
      setFavoriteSymbols(new Set());
    }
  }, []);

  useEffect(() => {
    void fetchFavorites();
  }, [fetchFavorites]);

  const toggleFavorite = useCallback(
    async (symbol: string, companyName: string, isFavorite: boolean) => {
      const url = `/api/favorites?symbol=${encodeURIComponent(symbol)}${
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
    [fetchFavorites],
  );

  const renderFavoriteButton = useCallback(
    (params: ICellRendererParams): JSX.Element | null => {
      const symbol = params.data?.symbol as string | undefined;
      const companyName = params.data?.companyName as string | undefined;
      if (!symbol) return null;

      const normalizedSymbol = normalizeSymbol(symbol);
      const isFavorite = favoriteSymbols.has(normalizedSymbol);

      return (
        <FavoriteToggle
          symbol={symbol}
          companyName={companyName}
          isFavorite={isFavorite}
          onToggle={() => void toggleFavorite(symbol, companyName ?? '', isFavorite)}
        />
      );
    },
    [favoriteSymbols, toggleFavorite],
  );

  return { favoriteSymbols, renderFavoriteButton };
};