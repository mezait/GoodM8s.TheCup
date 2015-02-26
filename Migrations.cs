using System;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace GoodM8s.TheCup {
    public class Migrations : DataMigrationImpl {
        public int Create() {
            // Attendees
            SchemaBuilder.CreateTable("AttendeePartRecord", table => table
                .ContentPartRecord()
                .Column<string>("FirstName", c => c.WithLength(50))
                .Column<string>("LastName", c => c.WithLength(50)));

            ContentDefinitionManager.AlterPartDefinition("AttendeePart", part => part.Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("Attendee", type => type
                .WithPart("AttendeePart")
                .WithPart("UserPart"));

            // Events
            SchemaBuilder.CreateTable("EventPartRecord", table => table
                .ContentPartRecord()
                .Column<string>("Name", c => c.WithLength(50))
                .Column<string>("Description", c => c.WithLength(256)));

            ContentDefinitionManager.AlterPartDefinition("EventPart", part => part.Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("Event", type => type.WithPart("EventPart"));

            // Cups
            SchemaBuilder.CreateTable("CupPartRecord", table => table
                .ContentPartRecord()
                .Column<DateTime>("Date")
                .Column<string>("Title", c => c.WithLength(50)));

            ContentDefinitionManager.AlterPartDefinition("CupPart", part => part.Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("Cup", type => type.WithPart("CupPart"));

            // Event order
            SchemaBuilder.CreateTable("EventOrderRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("CupPartRecord_id")
                .Column<int>("EventPartRecord_id")
                .Column<int>("SortOrder"));

            SchemaBuilder.CreateForeignKey("EventOrder_Cup", "EventOrderRecord", new[] {"CupPartRecord_id"}, "CupPartRecord", new[] {"Id"});
            SchemaBuilder.CreateForeignKey("EventOrder_Event", "EventOrderRecord", new[] {"EventPartRecord_id"}, "EventPartRecord", new[] {"Id"});

            // Cup placement
            SchemaBuilder.CreateTable("CupPlaceRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("CupPartRecord_id")
                .Column<int>("Place")
                .Column<int>("Points"));

            SchemaBuilder.CreateForeignKey("CupPlace_Cup", "CupPlaceRecord", new[] { "CupPartRecord_id" }, "CupPartRecord", new[] { "Id" });

            // Teams
            SchemaBuilder.CreateTable("TeamPartRecord", table => table
                .ContentPartRecord()
                .Column<int>("CupId")
                .Column<string>("Name", c => c.WithLength(50)));

            SchemaBuilder.CreateForeignKey("Team_Cup", "TeamPartRecord", new[] {"CupId"}, "CupPartRecord", new[] {"Id"});

            ContentDefinitionManager.AlterPartDefinition("TeamPart", part => part.Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("Team", type => type.WithPart("TeamPart"));

            // Team attendee
            SchemaBuilder.CreateTable("TeamAttendeeRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("TeamPartRecord_id")
                .Column<int>("AttendeePartRecord_id"));

            SchemaBuilder.CreateForeignKey("TeamAttendee_Team", "TeamAttendeeRecord", new[] {"TeamPartRecord_id"}, "TeamPartRecord", new[] {"Id"});
            SchemaBuilder.CreateForeignKey("TeamAttendee_Attendee", "TeamAttendeeRecord", new[] {"AttendeePartRecord_id"}, "AttendeePartRecord", new[] {"Id"});

            // Scores
            SchemaBuilder.CreateTable("ScorePartRecord", table => table
                .ContentPartRecord()
                .Column<int>("CupId")
                .Column<int>("EventId")
                .Column<string>("Notes"));

            SchemaBuilder.CreateForeignKey("Score_Cup", "ScorePartRecord", new[] {"CupId"}, "CupPartRecord", new[] {"Id"});
            SchemaBuilder.CreateForeignKey("Score_Event", "ScorePartRecord", new[] {"EventId"}, "EventPartRecord", new[] {"Id"});

            ContentDefinitionManager.AlterPartDefinition("ScorePart", part => part.Attachable(false));

            ContentDefinitionManager.AlterTypeDefinition("Score", type => type.WithPart("ScorePart"));

            // Score team
            SchemaBuilder.CreateTable("TeamScoreRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("ScorePartRecord_id")
                .Column<int>("TeamPartRecord_id")
                .Column<int>("Score")
                .Column<int>("Penalties"));

            SchemaBuilder.CreateForeignKey("TeamScore_Score", "TeamScoreRecord", new[] {"ScorePartRecord_id"}, "ScorePartRecord", new[] {"Id"});
            SchemaBuilder.CreateForeignKey("TeamScore_Team", "TeamScoreRecord", new[] {"TeamPartRecord_id"}, "TeamPartRecord", new[] {"Id"});

            return 1;
        }
    }
}