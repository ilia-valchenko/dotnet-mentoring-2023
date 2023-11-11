if exist .\AppData\cart-data.db (
    del .\AppData\cart-data.db
)

if not exist .\AppData (
    mkdir .\AppData
)

sqlite3 .\AppData\cart-data.db < recreate-database.sql & pause
