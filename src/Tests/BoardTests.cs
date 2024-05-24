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
                Assert.That(board.GetField(0, 0).IsWhite, Is.False, "A1 is black field");
                Assert.That(board.GetField(0, 0).HasFigure, Is.True, "A1 has rock");
                Assert.That(board.GetField(3, 4).IsWhite, Is.True, "E4 is white field");
                Assert.That(board.GetField(3, 4).HasFigure, Is.False, "E4 has not any figure");
            });
             
        }
    }
}
