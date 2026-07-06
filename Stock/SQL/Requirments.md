Delete the History page and related all files components etc.
After deleet the details page remove every where the in the grid deatils navigation. Replace NSE text with symbols
Create new contoller only Keep Favorites thing  thre favorites controller update ui router and all
NSEController only keep SaveEquityList, SaveSymbolData, SaveYearwiseData
YearwiseStockSummaryController Move to NSE contoller
vw_AiRecommendations.sql & vw_YearwiseStockSummary.sql convert them to linq and use no view need
SaveEquityList, SaveSymbolData, SaveYearwiseData make this all result establish the relationship in entity so one deleet will delted related no need one by one
wgere ever poble use execite delte or ecciteupdate new ef core method
remove all unused anction method service method every thing 
remove old migration craete new 
migratuon command save in read me

