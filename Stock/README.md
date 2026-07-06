# NSE Stock Application

## Migration Instructions

After the recent refactoring, follow these steps to update your database:

### 1. Remove Old Migrations

Delete the old migration files (keep only `20260629041201_Initial.cs` and related):
```bash
# The following migration files should be manually deleted or archived:
# - 20260701151613_AddHistoricalTradeDateSeriesColumns.cs
# - 20260701154406_RestoreFavoriteSymbolsTable.cs
# - 20260702033435_AddAiRecommendations.cs
# - 20260702052935_AddBatchCreatedAtToHistoricalTradeData_V2.cs
# (and any others beyond the initial setup)
```

### 2. Create New Migration

To create a new migration that incorporates all changes:

```bash
cd e:\NSE Stock App\Stock

# Add a new migration with all the refactoring changes
dotnet ef migrations add "Refactor_RemoveViews_ConvertToLinq" --startup-project .

# Review the generated migration file before applying
```

### 3. Apply Migration to Database

```bash
# Update the database with the new migration
dotnet ef database update --startup-project .
```

### 4. Key Changes in This Migration

- **Removed View Mappings**: The `vw_AiRecommendations` and `vw_YearwiseStockSummary` views are no longer mapped in the DbContext
- **Batch Operations**: Replaced RemoveRange with ExecuteDelete for better performance
- **LINQ Queries**: Added LINQ queries in repository and controllers to replace view functionality
- **Controllers Refactored**:
  - NSEController: Keeps only SaveEquityList, SaveSymbolData, SaveYearwiseData, + yearwise summary endpoints
  - FavoritesController: New controller for favorite management
  - YearwiseStockSummaryController: Functionality moved to NSEController

### 5. Verify Changes

After applying the migration, test the following endpoints:

```bash
# Test favorite operations (now under /api/favorites)
GET    /api/favorites
POST   /api/favorites?symbol=TCS&companyName=TataConsultancyServices
DELETE /api/favorites?symbol=TCS

# Test yearwise summary (now under /api/nse/yearwise-summary)
GET    /api/nse/yearwise-summary
GET    /api/nse/yearwise-summary/download

# Test save operations
POST   /api/nse/save-equity-list
POST   /api/nse/save-symbol-data
POST   /api/nse/save-yearwise-data
```

### 6. Frontend Update

The frontend has been updated to use new API routes:
- `/api/nse/yearwise-summary` (was `/api/yearwisestocksummary`)
- `/api/favorites` (was `/api/nse/favorites`)
- Removed `/historical` route - integrated into symbol details page

## Project Structure Changes

### Removed Components
- Removed "History" navigation tab (previously pointed to `/historical` route)
- Integrated historical trade data into symbol details page

### New/Updated Controllers
- **FavoritesController.cs**: Handles all favorite-related operations
- **NSEController.cs**: Consolidated main data operations with yearwise summary methods

### Refactoring Notes
- All repository methods now use `ExecuteDelete()` for batch operations instead of `RemoveRange()`
- SQL views have been converted to LINQ queries for better maintainability
- Entity relationships have been reviewed and cascade delete behavior verified

## Troubleshooting

If you encounter issues:

1. **View not found error**: Ensure the migration has been applied successfully
2. **API endpoint not found**: Verify the controller names and routing attributes
3. **Data missing**: The migration may need to handle existing data - review migration file before applying
