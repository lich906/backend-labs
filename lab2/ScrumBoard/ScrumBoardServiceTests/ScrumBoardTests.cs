using System;
using Xunit;

namespace ScrumBoardService.Tests
{
    public class ScrumBoardTests
    {
        [Fact]
        public void CreatingNewBoard()
        {
            var board = new Board("Project");

            Assert.Equal("Project", board.Name);
        }

        [Fact]
        public void GetAllColumns_NoColumns_EmptyList()
        {
            var board = new Board("Project");

            Assert.Empty(board.GetAllColumns());
        }

        [Fact]
        public void GetColumn_NoColumns_ThrowsException()
        {
            var board = new Board("Project");

            Assert.Throws<ArgumentOutOfRangeException>(() => board.GetColumn(0));
        }

        [Fact]
        public void AddNewColumn_OnEmptyBoard_ColumnsCreatedSuccessfully()
        {
            var board = new Board("Project");

            board.AddNewColumn("Open");

            Assert.NotEmpty(board.GetAllColumns());
            Assert.Equal("Open", board.GetColumn(0).Name);

            board.AddNewColumn("Reopened");

            Assert.Equal("Open", board.GetColumn(0).Name);
            Assert.Equal("Reopened", board.GetColumn(1).Name);

            board.AddNewColumn("In Progress");

            Assert.Equal("In Progress", board.GetColumn(2).Name);
        }

        [Fact]
        public void GetColumn_WrongIndex_ThrowsException()
        {
            var board = new Board("Project");

            board.AddNewColumn("Open");
            board.AddNewColumn("In Progress");
            board.AddNewColumn("Done");

            Assert.NotNull(board.GetColumn(0));
            Assert.NotNull(board.GetColumn(1));
            Assert.NotNull(board.GetColumn(2));

            Assert.Throws<ArgumentOutOfRangeException>(() => board.GetColumn(5));
        }

        [Fact]
        public void AddNewColumn_ExceedColumnsLimit_ThrowsException()
        {
            var board = new Board("Project");

            board.AddNewColumn("Submitted");
            board.AddNewColumn("Duplicated");
            board.AddNewColumn("Open");
            board.AddNewColumn("Reopened");
            board.AddNewColumn("In Progress");
            board.AddNewColumn("To be discussed");
            board.AddNewColumn("Code Review");
            board.AddNewColumn("Fixed");
            board.AddNewColumn("Verified");
            board.AddNewColumn("Won't fix");

            Assert.Throws<Exception>(() => board.AddNewColumn("Yet another column that won't be created"));
        }

        [Fact]
        public void RenameColumn_NameOpen_NameReopened()
        {
            var board = new Board("Project");

            board.AddNewColumn("Open");

            board.GetColumn(0).Rename("Reopened");

            Assert.Equal("Reopened", board.GetColumn(0).Name);
        }

            [Fact]
        public void AddNewCard_NoColumns_ThrowsException()
        {
            var board = new Board("Project");

            Assert.Throws<Exception>(() => board.AddNewCard("Pet the cat"));
        }

        [Fact]
        public void AddNewCard_HasOneColumn_CardCreated()
        {
            var board = new Board("Project");

            board.AddNewColumn("To do");

            board.AddNewCard("Pet the cat");

            Assert.Equal("Pet the cat", board.GetColumn(0).GetCard(0).Name);
        }
    }
}
