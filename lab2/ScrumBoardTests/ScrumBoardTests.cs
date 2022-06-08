using System;
using System.Collections.Generic;
using System.Text;
using ScrumBoard.Model;
using Xunit;

namespace ScrumBoardTests
{
    public class ScrumBoardTests
    {
        private readonly Board _sut;

        public ScrumBoardTests()
        {
            _sut = new Board("MyTasks");
        }

        [Fact]
        public void BoardConstructor_NameArgument_AssignsNameToBoard()
        {
            Assert.Equal("MyTasks", _sut.Name);
        }

        [Fact]
        public void AddNewColumn_PassingNameArg_ColumnCreated()
        {
            _sut.AddNewColumn("To do");
            Column column = _sut.GetColumnByName("To do");
            Assert.Equal("To do", column.Name);
        }

        [Fact]
        public void AddNewColumn_WithExistingColumnName_ExceptionThrown()
        {
            _sut.AddNewColumn("To do");
            Assert.Throws<ApplicationException>(() => _sut.AddNewColumn("To do"));
        }

        [Fact]
        public void AddNewColumn_ExceedColumnsLimit_ExceptionThrown()
        {
            _sut.AddNewColumn("1");
            _sut.AddNewColumn("2");
            _sut.AddNewColumn("3");
            _sut.AddNewColumn("4");
            _sut.AddNewColumn("5");
            _sut.AddNewColumn("6");
            _sut.AddNewColumn("7");
            _sut.AddNewColumn("8");
            _sut.AddNewColumn("9");
            _sut.AddNewColumn("10");
            Assert.Equal(10, _sut.GetAllColumns().Count);
            Assert.Throws<ApplicationException>(() => _sut.AddNewColumn("11"));
        }

        [Fact]
        public void AddNewCard_NoColumns_ExceptionThrown()
        {
            Assert.Throws<ApplicationException>(() => _sut.AddNewCard("Hello", "I must not be created", Card.PriorityType.Major));
        }

        [Fact]
        public void AddNewCard_OneColumnPresent_CardCreated()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello to everybody", Card.PriorityType.Normal);
            Card card = _sut.GetCardByName("Hello");
            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello to everybody", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);
        }

        [Fact]
        public void AddNewCard_WithExistingCardName_ExceptionThrown()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello to everybody", Card.PriorityType.Normal);
            Assert.Throws<ApplicationException>(() => _sut.AddNewCard("Hello", "Say hello again", Card.PriorityType.Major));
        }

        [Fact]
        public void MoveCard_CardDoesntExist_ExceptionThrown()
        {
            Assert.Throws<ApplicationException>(() => _sut.MoveCard("Hello", "Open"));
        }

        [Fact]
        public void MoveCard_ColumnDoesntExist_ExceptionThrown()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello to everybody", Card.PriorityType.Normal);
            Assert.Throws<ApplicationException>(() => _sut.MoveCard("Hello", "In progress"));
        }

        [Fact]
        public void MoveCard_ColumnAndCardExists_CardMoved()
        {
            _sut.AddNewColumn("Open");
            Assert.Single(_sut.GetAllColumns());
            Assert.Empty(_sut.GetColumnByName("Open").GetAllCards());
            _sut.AddNewCard("Hello", "Say hello to everybody", Card.PriorityType.Normal);
            Assert.Single(_sut.GetColumnByName("Open").GetAllCards());
            Card card = _sut.GetColumnByName("Open").GetCardByName("Hello");

            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello to everybody", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);

            _sut.AddNewColumn("In progress");
            Assert.Equal(2, _sut.GetAllColumns().Count);
            _sut.MoveCard("Hello", "In progress");
            Assert.Empty(_sut.GetColumnByName("Open").GetAllCards());
            Assert.Single(_sut.GetColumnByName("In progress").GetAllCards());
            card = _sut.GetColumnByName("In progress").GetCardByName("Hello");

            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello to everybody", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);
        }

        [Fact]
        public void GetCardByName_NoColumns_ExceptionThrown()
        {
            Assert.Throws<ApplicationException>(() => _sut.GetCardByName("Hello"));
        }

        [Fact]
        public void GetCardByName_CardDoesntExist_ExceptionThrown()
        {
            _sut.AddNewColumn("Open");
            Assert.Throws<ApplicationException>(() => _sut.GetCardByName("Hello"));
        }

        [Fact]
        public void GetCardByName_CardExists_CardRecieved()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            Assert.Throws<ApplicationException>(() => _sut.GetCardByName("Wrong name"));
            Card card = _sut.GetCardByName("Hello");

            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);
        }

        [Fact]
        public void RenameColumn_ColumnDoesntExist_ExceptionThrown()
        {
            Assert.Throws<ApplicationException>(() => _sut.RenameColumn("Open", "Done"));
        }

        [Fact]
        public void RenameColumn_ColumnWithNewNameExists_ExceptionThrown()
        {
            _sut.AddNewColumn("Done");
            _sut.AddNewColumn("Open");
            Assert.Throws<ApplicationException>(() => _sut.RenameColumn("Open", "Done"));
        }

        [Fact]
        public void RenameColumn_ColumnToBeRenamedExistsAndNewNameIsFree_ColumnRenamed()
        {
            _sut.AddNewColumn("Open");
            Assert.True(_sut.ColumnExists("Open"));
            Assert.False(_sut.ColumnExists("Done"));
            Assert.Single(_sut.GetAllColumns());

            _sut.RenameColumn("Open", "Done");

            Assert.False(_sut.ColumnExists("Open"));
            Assert.True(_sut.ColumnExists("Done"));
            Assert.Single(_sut.GetAllColumns());
        }

        [Fact]
        public void RenameCard_NewCardNameTaken_ExceptionThrown()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            _sut.AddNewCard("Goodbye", "Say goodbye", Card.PriorityType.Normal);
            Assert.Throws<ApplicationException>(() => _sut.RenameCard("Hello", "Goodbye"));
        }

        [Fact]
        public void RenameCard_CardDoesntExist_ExceptionThrown()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            Assert.Throws<ApplicationException>(() => _sut.RenameCard("Acid", "Hello"));
        }

        [Fact]
        public void RenameCard_CardExistsAndNewCardNameIsFree_CardRenamed()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            Assert.Single(_sut.GetColumnByName("Open").GetAllCards());
            Assert.Equal("Hello", _sut.GetCardByName("Hello").Name);

            _sut.RenameCard("Hello", "Privet");

            Assert.Equal("Privet", _sut.GetCardByName("Privet").Name);
            Assert.Single(_sut.GetColumnByName("Open").GetAllCards());
        }

        [Fact]
        public void ChangeCardDescription_CardExists_DescriptionChanged()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            Assert.Equal("Say hello", _sut.GetCardByName("Hello").Description);

            _sut.GetCardByName("Hello").ChangeDescription("Say privet");

            Assert.Equal("Say privet", _sut.GetCardByName("Hello").Description);
        }

        [Fact]
        public void ChangeCardPriority_CardExists_PriorityChanged()
        {
            _sut.AddNewColumn("Open");
            _sut.AddNewCard("Hello", "Say hello", Card.PriorityType.Normal);
            Assert.Equal(Card.PriorityType.Normal, _sut.GetCardByName("Hello").Priority);

            _sut.GetCardByName("Hello").ChangePriority(Card.PriorityType.Major);

            Assert.Equal(Card.PriorityType.Major, _sut.GetCardByName("Hello").Priority);
        }
    }
}
