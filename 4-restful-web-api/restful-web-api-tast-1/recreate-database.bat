if exist .\AppData\catalog-service-database.db (
    del .\AppData\catalog-service-database.db
)

if not exist .\AppData (
    mkdir .\AppData
)

sqlite3 .\AppData\catalog-service-database.db < recreate-database.sql & pause
