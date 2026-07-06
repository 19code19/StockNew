export type FormatterType = 'text' | 'number' | 'currency' | 'percent';

const numberFormatter = new Intl.NumberFormat('en-IN', { maximumFractionDigits: 2 });

export const formatDisplayValue = (value: unknown): string => {
  if (value == null || value === '') return '';
  if (typeof value === 'number' && Number.isFinite(value)) return numberFormatter.format(value);
  if (typeof value === 'boolean') return value ? 'true' : 'false';
  return String(value);
};

export const FORMATTERS: Record<FormatterType, (value: unknown) => string> = {
  text: formatDisplayValue,
  number: formatDisplayValue,
  currency: (value) => (value == null || value === '' ? '' : `₹${numberFormatter.format(value as number)}`),
  percent: (value) => (value == null || value === '' ? '' : `${formatDisplayValue(value)}%`),
};

export const formatColumnHeader = (field: string): string =>
  field
    .replace(/([a-z0-9])([A-Z])/g, '$1 $2')
    .replace(/_/g, ' ')
    .replace(/^./, (char) => char.toUpperCase());
