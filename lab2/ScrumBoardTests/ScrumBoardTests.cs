using System;
using System.Collections.Generic;
using System.Text;
using ScrumBoard;
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
            Assert.Equal("To do", _sut.GetAllColumns()[0].Name);
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
            Card card = _sut.GetAllColumns()[0].GetCardByName("Hello");
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
            Assert.Empty(_sut.GetAllColumns()[0].GetAllCards());
            _sut.AddNewCard("Hello", "Say hello to everybody", Card.PriorityType.Normal);
            Assert.Single(_sut.GetAllColumns()[0].GetAllCards());
            Card card = _sut.GetAllColumns()[0].GetCardByName("Hello");

            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello to everybody", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);

            _sut.AddNewColumn("In progress");
            Assert.Equal(2, _sut.GetAllColumns().Count);
            _sut.MoveCard("Hello", "In progress");
            Assert.Empty(_sut.GetAllColumns()[0].GetAllCards());
            Assert.Single(_sut.GetAllColumns()[1].GetAllCards());
            card = _sut.GetAllColumns()[1].GetCardByName("Hello");

            Assert.Equal("Hello", card.Name);
            Assert.Equal("Say hello to everybody", card.Description);
            Assert.Equal(Card.PriorityType.Normal, card.Priority);
        }
    }
}
