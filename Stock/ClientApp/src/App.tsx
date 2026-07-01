import { Route, Routes } from 'react-router-dom';
import { SummaryDataProvider } from './context/SummaryDataContext';
import HeaderTabs from './components/HeaderTabs';
import YearwiseStockSummaryPage from './pages/YearwiseStockSummaryPage';
import HistoricalTradePage from './pages/HistoricalTradePage';
import FavoritesPage from './pages/FavoritesPage';
import SymbolDetailsPage from './pages/SymbolDetailsPage';

const App = () => (
  <div className="h-screen w-screen bg-slate-950 text-slate-100 overflow-hidden">
    <nav className="border-b border-slate-800 bg-slate-900/90 backdrop-blur-sm">
      <div className="flex h-20 items-center justify-between px-6">
        <div>
          <p className="text-sm font-semibold uppercase tracking-[0.3em] text-sky-400">NSE Stock Desk</p>
          <h1 className="text-xl font-semibold text-white">NSE Dashboard</h1>
        </div>
        <HeaderTabs />
      </div>
    </nav>

    <main className="flex h-[calc(100vh-5rem)] w-full flex-col px-4 pb-4 pt-4">
      <SummaryDataProvider>
        <Routes>
          <Route path="/" element={<YearwiseStockSummaryPage />} />
          <Route path="/historical" element={<HistoricalTradePage />} />
          <Route path="/favorites" element={<FavoritesPage />} />
          <Route path="/details/:symbol/:companySlug" element={<SymbolDetailsPage />} />
        </Routes>
      </SummaryDataProvider>
    </main>
  </div>
);

export default App;
