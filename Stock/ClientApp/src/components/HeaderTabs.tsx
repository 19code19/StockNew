import { NavLink } from 'react-router-dom';

const tabs = [
  { label: 'Yearwise Summary', to: '/' },
  { label: 'Historical Trade', to: '/historical' },
  { label: 'Favorites', to: '/favorites' },
];

const HeaderTabs = () => (
  <div className="flex flex-wrap items-center gap-2">
    {tabs.map((tab) => (
      <NavLink
        key={tab.to}
        to={tab.to}
        end={tab.to === '/'}
        className={({ isActive }) =>
          `rounded-full px-4 py-2 text-sm font-semibold transition ${
            isActive ? 'bg-slate-800 text-white' : 'text-slate-400 hover:bg-slate-900 hover:text-white'
          }`
        }
      >
        {tab.label}
      </NavLink>
    ))}
  </div>
);

export default HeaderTabs;
