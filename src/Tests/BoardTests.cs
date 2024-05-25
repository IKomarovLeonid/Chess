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

        [Test]
        [Description("Make simple valid move and check fields after")]
        public void Move_Valid_CheckBoard()
        {
            var board = ChessBoard.CreateInitial();

            var isSuccess = board.MakeMove("e2-e4");

            Assert.Multiple(() =>
            {
                Assert.That(isSuccess, Is.True, "Move was performed successfully");
                Assert.That(board.GetField("e2").HasFigure, Is.False, "Pawn moved, e2 is free now");
                Assert.That(board.GetField("e2").Figure, Is.Null);
                Assert.That(board.GetField("e4").HasFigure, Is.True, "Pawn moved, e4 is set");
                Assert.That(board.GetField("e4").Figure, Is.EqualTo(FigureType.Pawn), "Figure is pawn");
            });
        }
    }
}
