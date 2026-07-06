type FavoriteToggleProps = {
  symbol: string;
  companyName?: string;
  isFavorite: boolean;
  onToggle: () => void;
};

const FavoriteToggle = ({ symbol, companyName, isFavorite, onToggle }: FavoriteToggleProps) => (
  <button
    type="button"
    onClick={(event) => {
      event.stopPropagation();
      onToggle();
    }}
    aria-label={`${isFavorite ? 'Remove' : 'Add'} ${symbol} from favorites`}
    className={`rounded-full px-3 py-1 text-xs font-semibold transition ${
      isFavorite ? 'bg-amber-500 text-slate-950 hover:bg-amber-400' : 'bg-slate-700 text-slate-100 hover:bg-slate-600'
    }`}
  >
    {isFavorite ? '★' : '☆'}
  </button>
);

export default FavoriteToggle;
