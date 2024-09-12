using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eapproval.Migrations
{
    /// <inheritdoc />
    public partial class a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmpCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Wing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Groups = table.Column<string>(type: "varchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelUserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    Raters = table.Column<int>(type: "int", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numbers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuthorId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headline = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HasServices = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_HeadId",
                        column: x => x.HeadId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Assigned = table.Column<bool>(type: "bit", nullable: true),
                    HasService = table.Column<bool>(type: "bit", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovalRequired = table.Column<bool>(type: "bit", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CloseRequested = table.Column<bool>(type: "bit", nullable: true),
                    AssignedToId = table.Column<int>(type: "int", nullable: true),
                    TicketType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RaisedById = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrevStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ask = table.Column<bool>(type: "bit", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Infos = table.Column<string>(type: "varchar(max)", nullable: true),
                    RequestDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentHandlerId = table.Column<int>(type: "int", nullable: true),
                    TicketingHeadId = table.Column<int>(type: "int", nullable: true),
                    PrevHandlerId = table.Column<int>(type: "int", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MadeCloseRequest = table.Column<bool>(type: "bit", nullable: true),
                    BeenRejected = table.Column<bool>(type: "bit", nullable: true),
                    Accepted = table.Column<bool>(type: "bit", nullable: true),
                    Mentions = table.Column<string>(type: "varchar(max)", nullable: true),
                    Users = table.Column<string>(type: "varchar(max)", nullable: true),
                    TimesRaised = table.Column<int>(type: "int", nullable: false),
                    Genesis = table.Column<bool>(type: "bit", nullable: false),
                    GenesisId = table.Column<int>(type: "int", nullable: true),
                    InitialType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InitialPriorityId = table.Column<int>(type: "int", nullable: true),
                    InitialPriority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Users_AssignedToId",
                        column: x => x.AssignedToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_CurrentHandlerId",
                        column: x => x.CurrentHandlerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_PrevHandlerId",
                        column: x => x.PrevHandlerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_RaisedById",
                        column: x => x.RaisedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tickets_Users_TicketingHeadId",
                        column: x => x.TicketingHeadId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProblemTypesClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subs = table.Column<string>(type: "varchar(max)", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemTypesClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProblemTypesClass_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamLeaders",
                columns: table => new
                {
                    LeadersId = table.Column<int>(type: "int", nullable: false),
                    TeamsLeadedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamLeaders", x => new { x.LeadersId, x.TeamsLeadedId });
                    table.ForeignKey(
                        name: "FK_TeamLeaders_Teams_TeamsLeadedId",
                        column: x => x.TeamsLeadedId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamLeaders_Users_LeadersId",
                        column: x => x.LeadersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamMonitors",
                columns: table => new
                {
                    MonitorsId = table.Column<int>(type: "int", nullable: false),
                    TeamsMonitoredId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMonitors", x => new { x.MonitorsId, x.TeamsMonitoredId });
                    table.ForeignKey(
                        name: "FK_TeamMonitors_Teams_TeamsMonitoredId",
                        column: x => x.TeamsMonitoredId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamMonitors_Users_MonitorsId",
                        column: x => x.MonitorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamSuboridinates",
                columns: table => new
                {
                    SubordinatesId = table.Column<int>(type: "int", nullable: false),
                    TeamMembersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamSuboridinates", x => new { x.SubordinatesId, x.TeamMembersId });
                    table.ForeignKey(
                        name: "FK_TeamSuboridinates_Teams_TeamMembersId",
                        column: x => x.TeamMembersId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamSuboridinates_Users_SubordinatesId",
                        column: x => x.SubordinatesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionObject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serial = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    RaisedById = table.Column<int>(type: "int", nullable: true),
                    ForwardedToId = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionObject_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActionObject_Users_ForwardedToId",
                        column: x => x.ForwardedToId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ActionObject_Users_RaisedById",
                        column: x => x.RaisedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Input = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailsClass_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetailsClass_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TakenBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mentions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notes_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: true),
                    FromId = table.Column<int>(type: "int", nullable: true),
                    ToId = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mentions = table.Column<string>(type: "varchar(max)", nullable: false),
                    TicketsId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Tickets_TicketsId",
                        column: x => x.TicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ConnectionHolderClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionHolderClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionHolderClass_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConversationClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromId = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConversationClass_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationClass_Users_FromId",
                        column: x => x.FromId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mentions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteId = table.Column<int>(type: "int", nullable: true),
                    NotificationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mentions_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mentions_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "File2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConversationId = table.Column<int>(type: "int", nullable: true),
                    NoteId = table.Column<int>(type: "int", nullable: true),
                    ActionId = table.Column<int>(type: "int", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File2", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File2_ActionObject_ActionId",
                        column: x => x.ActionId,
                        principalTable: "ActionObject",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_File2_ConversationClass_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "ConversationClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_File2_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_File2_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionObject_ForwardedToId",
                table: "ActionObject",
                column: "ForwardedToId");

            migrationBuilder.CreateIndex(
                name: "IX_ActionObject_RaisedById",
                table: "ActionObject",
                column: "RaisedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActionObject_TicketId",
                table: "ActionObject",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_AuthorId",
                table: "Blogs",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_TicketId",
                table: "Chats",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionHolderClass_ChatId",
                table: "ConnectionHolderClass",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationClass_ChatId",
                table: "ConversationClass",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ConversationClass_FromId",
                table: "ConversationClass",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsClass_TeamId",
                table: "DetailsClass",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailsClass_TicketId",
                table: "DetailsClass",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_File2_ActionId",
                table: "File2",
                column: "ActionId");

            migrationBuilder.CreateIndex(
                name: "IX_File2_ConversationId",
                table: "File2",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_File2_NoteId",
                table: "File2",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_File2_TicketId",
                table: "File2",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentions_NoteId",
                table: "Mentions",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_Mentions_NotificationId",
                table: "Mentions",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_TicketId",
                table: "Notes",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_FromId",
                table: "Notifications",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_TicketsId",
                table: "Notifications",
                column: "TicketsId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemTypesClass_TeamId",
                table: "ProblemTypesClass",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamLeaders_TeamsLeadedId",
                table: "TeamLeaders",
                column: "TeamsLeadedId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamMonitors_TeamsMonitoredId",
                table: "TeamMonitors",
                column: "TeamsMonitoredId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_HeadId",
                table: "Teams",
                column: "HeadId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamSuboridinates_TeamMembersId",
                table: "TeamSuboridinates",
                column: "TeamMembersId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_AssignedToId",
                table: "Tickets",
                column: "AssignedToId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CurrentHandlerId",
                table: "Tickets",
                column: "CurrentHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_PrevHandlerId",
                table: "Tickets",
                column: "PrevHandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_RaisedById",
                table: "Tickets",
                column: "RaisedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TicketingHeadId",
                table: "Tickets",
                column: "TicketingHeadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "ConnectionHolderClass");

            migrationBuilder.DropTable(
                name: "DetailsClass");

            migrationBuilder.DropTable(
                name: "File2");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Mentions");

            migrationBuilder.DropTable(
                name: "ProblemTypesClass");

            migrationBuilder.DropTable(
                name: "TeamLeaders");

            migrationBuilder.DropTable(
                name: "TeamMonitors");

            migrationBuilder.DropTable(
                name: "TeamSuboridinates");

            migrationBuilder.DropTable(
                name: "ActionObject");

            migrationBuilder.DropTable(
                name: "ConversationClass");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
