namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulateGenresTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");
            Sql("INSERT INTO Genres (Id, Name) VALUES (1, 'Jazz')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (2, 'Blues')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (3, 'Rock')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (4, 'Country')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (5, 'Rap')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (6, 'Pop')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (7, 'Metal')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (8, 'Dance')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (9, 'Alternative')");
            Sql("INSERT INTO Genres (Id, Name) VALUES (10, 'Christian')");
            Sql("SET IDENTITY_INSERT Genres OFF");
        }

        public override void Down()
        {
            Sql("DELETE FROM Genres WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10)");
        }
    }
}
