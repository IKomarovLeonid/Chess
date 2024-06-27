using NUnit.Framework;
using Objects.Src;

namespace Tests
{
    internal class LogicTests : BaseTests
    {
        [SetUp]
        public void Setup()
        {
            board = ChessBoard.CreateOnlyBoard();
            moveProcessor = new MoveProcessor(board);
        }

        [Test]
        public void SimpleCheckState()
        {
            board.SetFigure(new Figure(FigureType.Knight, true), "e4");
            board.SetFigure(new Figure(FigureType.King, false), "d6");

            var isCheck = moveProcessor.IsCheckState();

            Assert.IsTrue(isCheck);
        }

        [Test]
        public void CheckState_SameColor_NotACheck()
        {
            board.SetFigure(new Figure(FigureType.Knight, true), "e4");
            board.SetFigure(new Figure(FigureType.King, true), "d6");

            var isCheck = moveProcessor.IsCheckState();

            Assert.False(isCheck);
        }

        [Test]
        public void SimpleMateState_Success()
        {
            // check here
            board.SetFigure(new Figure(FigureType.Rook, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");
            board.SetFigure(new Figure(FigureType.Pawn, false), "g7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "f7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "h7");

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isCheck);
                Assert.IsTrue(isMate);
            });
        }

        [Test]
        public void SimpleDrawState_Success()
        {
            // check here
            board.SetFigure(new Figure(FigureType.King, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();
            var isDraw = moveProcessor.IsDraw();

            Assert.Multiple(() =>
            {
                Assert.IsFalse(isCheck);
                Assert.IsFalse(isMate);
                Assert.IsTrue(isDraw);
            });
        }

        [Test]
        public void NotMate_CanCover()
        {
            // check here
            board.SetFigure(new Figure(FigureType.Rook, true), "b8");
            // black peaces
            board.SetFigure(new Figure(FigureType.King, false), "g8");
            board.SetFigure(new Figure(FigureType.Pawn, false), "g7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "f7");
            board.SetFigure(new Figure(FigureType.Pawn, false), "h7");
            board.SetFigure(new Figure(FigureType.Knight, false), "g6"); // f8 -> not mate

            var isCheck = moveProcessor.IsCheckState();
            var isMate = moveProcessor.IsMateState();

            Assert.Multiple(() =>
            {
                Assert.IsTrue(isCheck);
                Assert.IsFalse(isMate);
            });
        }

        [TestCase("e5", "h1", "0-0")]
        [TestCase("f5", "h1", "0-0")]
        [TestCase("g5", "h1", "0-0")]
        [TestCase("d5", "a1", "0-0-0")]
        [TestCase("c5", "a1", "0-0-0")]
        [TestCase("b5", "a1", "0-0-0")]
        [Description("Unable to castle if opponent's figure checks or attacks fields")]
        public void Can_Not_CastleUnderCheck(string blackPosition, string whiteRockPosition, string move)
        {
            board.SetFigure(new Figure(FigureType.King, true), "e1");
            board.SetFigure(new Figure(FigureType.Rook, true), whiteRockPosition);
            // black makes check
            board.SetFigure(new Figure(FigureType.Rook, false), blackPosition);

            var result = moveProcessor.MakeMove(move);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.False, "Move is not possible");
                Assert.That(board.GetField("e1").HasFigure(), Is.True);
                Assert.That(board.GetField("h1").HasFigure(), Is.True);
                Assert.That(board.GetField("e1").Figure.IsKing(), Is.True);
                Assert.That(board.GetField("h1").Figure.IsRook(), Is.True);
            });
        }

    }
}
