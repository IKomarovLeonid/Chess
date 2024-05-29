using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class BoardTests : BaseTests
    {
        [Test]
        [Description("Check initial chess board state")]
        public void Board_InitializeDefault()
        {
            var board = ChessBoard.CreateInitial();
            
            Assert.Multiple(() =>
            {
                Assert.That(board.GetField("a1").IsWhite, Is.False, "A1 is black field");
                Assert.That(board.GetField("a1").HasFigure, Is.True, "A1 has rock");
                Assert.That(board.GetField("e4").IsWhite, Is.True, "E4 is white field");
                Assert.That(board.GetField("e4").HasFigure, Is.False, "E4 has not any figure");
            });
        }

        [TestCase("e2-e4", FigureType.Pawn)]
        [TestCase("d2-d3", FigureType.Pawn)]
        [TestCase("g1-f3", FigureType.Knight)]
        [TestCase("b1-c3", FigureType.Knight)]
        [Description("Make simple valid move and check fields after")]
        public void Move_Valid_CheckBoard(string move, FigureType figureExpected)
        {
            var moveItems = move.Split('-'); 
            var board = ChessBoard.CreateInitial();

            var isSuccess = board.MakeMove(move);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move was performed successfully");
                Assert.That(board.GetField(moveItems[0]).HasFigure, Is.False, "Pawn moved, 'field from' is free now");
                Assert.That(board.GetField(moveItems[0]).Figure, Is.Null);
                Assert.That(board.GetField(moveItems[1]).HasFigure, Is.True, "Pawn moved, 'field to' is set");
                Assert.That(board.GetField(moveItems[1]).Figure, Is.EqualTo(figureExpected), "Figure");
            });
        }

        [TestCase("e2-d3")]
        [TestCase("e2-e5")]
        [TestCase("e2-e1")]
        [Description("Validate pawns moves")]
        public void Move_InvalidPawnMove(string move)
        {
            var moveItems = move.Split('-');
            var board = ChessBoard.CreateInitial();

            var fieldInitial = board.GetField(moveItems[0]);
            var fieldAfter = board.GetField(moveItems[1]);

            var isSuccess = board.MakeMove(move);

            var fieldNow = board.GetField(moveItems[0]);
            var fieldAfterNow = board.GetField(moveItems[1]);

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.False, "Move failed");
                Assert.That(fieldInitial.Figure, Is.EqualTo(fieldNow.Figure), "No affection");
                Assert.That(fieldAfterNow.Figure, Is.EqualTo(fieldAfter.Figure), "No affection");
            });
        }

        [Test]
        [Description("Simple valid pawn capture")]
        public void Move_PawnCapture_Success()
        {
            var board = ChessBoard.CreateInitial();

            var figure = board.GetField("e2").Figure;
            board.MakeMove("e2-e4");
            board.MakeMove("d7-d5");
            var isSuccess = board.MakeMove("e4xd5");
            var figureNow = board.GetField("d5").Figure;

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move processed");
                Assert.That(figureNow, Is.EqualTo(figure));
            });
        }
    }
}
